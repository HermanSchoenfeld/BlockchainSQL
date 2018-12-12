using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public class OptimizedBlockOrganizer : BizComponent, IBlockOrganizer {
        private const int OutOfSequenceTolerance = 2000000;
        private readonly BulkFetchActionCache<byte[], BlockPtr> _bulkLoadedBlockDbCache; 
        private readonly ICache<byte[], BlockPtr> _blockCache;
        private readonly ICache<int, int> _branchHeightCache;
        private readonly SyncronizedDictionary<byte[], BlockPtr> _processedBlocks;
        private readonly SyncronizedDictionary<int, Branch> _generatedBranches;
        private readonly Branch _mainChain;
        private readonly Branch _invalidBranch;        
        
        public OptimizedBlockOrganizer(bool optimizeForBulkLoading) {
            _mainChain = new Branch { ID = (int) KnownBranches.MainChain, ForkHeight = 0 };
            _invalidBranch = new Branch { ID = (int)KnownBranches.Invalid };
            _processedBlocks = new SyncronizedDictionary<byte[], BlockPtr>(ByteArrayEqualityComparer.Instance);
            _generatedBranches = new SyncronizedDictionary<int, Branch>();
            _bulkLoadedBlockDbCache = new BulkFetchActionCache<byte[], BlockPtr>(
                () => optimizeForBulkLoading ? LoadAllDatabaseBlocks() : new Dictionary<byte[], BlockPtr>(),
                fetchOnceOnly: true,
                keyComparer: ByteArrayEqualityComparer.Instance
            );
            _blockCache = new ActionCache<byte[], BlockPtr>(
                hash =>
                (optimizeForBulkLoading ? _bulkLoadedBlockDbCache[hash] : null) ??
                TryFindProcessedBlock(hash) ?? 
                TryFindOrganizedBlockInPipelineStream(hash) ??
                (optimizeForBulkLoading ? null : TryFindBlockInDatabase(hash)),
                null,
                CacheReapPolicy.Oldest,
                ExpirationPolicy.SinceFetchedTime,
                5000,
                null,
                NullValuePolicy.ReturnButDontCache,
                keyComparer: ByteArrayEqualityComparer.Instance
            );
            _branchHeightCache = new ActionCache<int, int>(
                branchID =>
                    MaxBranchHeightFromProcessedBlocks(branchID) ?? 
                    MaxBranchHeightFromPipelineStreamBlocks(branchID) ?? 
                    MaxBranchHeightFromDB(branchID) ?? 
                    0
            );
        }

        public virtual WipBlock[] Organize(IEnumerable<WipBlock> blocks, out WipBlock[] unorganizedBlocks) {
            var blocksToSequence = new LinkedList<WipBlock>(blocks);
            var results = new List<WipBlock>();
            var outOfSequenceBlockCount = 0;
            var danglingBlocks = new List<WipBlock>();
            var invalidBlockCount = 0;
            var byteArrComparer = ByteArrayEqualityComparer.Instance;
            while (blocksToSequence.Count > 0) {
                var wipBlock = blocksToSequence.First();
                // skip block if already processed
                if (_blockCache[wipBlock.Block.Hash] != null) {
                    throw new BlockAlreadySequencedException(wipBlock.Block.Hash);
                    // CANNOT RE-PROCESS BLOCK AS BELOW!! ERROR!!! Need to figure out proper way to deal with this
                    //blocksToSequence.RemoveFirst();
                    //continue;
                }

                if (wipBlock.Block.IsGenesisBlock()) {
                    #region Block is genesis block of mainchain

                    wipBlock.Block.Height = 0;
                    wipBlock.Block.Branch = _mainChain;

                    #endregion
                } else {

                    #region Determine previous block

                    var prevBlock = _blockCache[wipBlock.Block.PreviousBlockHash];
                    if (prevBlock == null) {
                        // previous block has not been encountered, possibilities include:
                        // 1. previous block is stored after this block (need to shuffle array)
                        // 2. previous block does not exist (invalid)
                        //      if prevBlock not atlast 200 blocks ahead, then invalid
                        outOfSequenceBlockCount++;
                        var unsequencedPrevBlock = blocksToSequence.Find(b => byteArrComparer.Equals(b.Block.Hash, wipBlock.Block.PreviousBlockHash)); // Use HashList collection
                        if (unsequencedPrevBlock != null) {
                            // Previous block ahead in array, so shuffle current block ahead and try again
                            blocksToSequence.RemoveFirst();
                            blocksToSequence.AddAfter(unsequencedPrevBlock, wipBlock);
                            continue;
                        }

                        if (blocksToSequence.Count <= OutOfSequenceTolerance) {
                            // Previous block might be passed by client in subsequent call, so 
                            // move it to end and keep processing rest                       
                            blocksToSequence.RemoveFirst();
                            danglingBlocks.Add(wipBlock);
                            continue;
                        }
                        // Previous block is determined not to exist, previous block just dummy invalid block
                        invalidBlockCount++;
                        prevBlock = new BlockPtr(new Block {Branch = _invalidBranch, Height = -1});
                        break;
                    }

                    #endregion

                    #region Place block in appropriate branch

                    switch ((KnownBranches) prevBlock.Branch.ID) {
                        case KnownBranches.Invalid:
                            // Joining an invalid block, all part of the invalid block set
                            wipBlock.Block.Height = -1;
                            wipBlock.Block.Branch = _invalidBranch;
                            break;
                        case KnownBranches.MainChain:
                            // Joining root block
                            wipBlock.Block.Height = prevBlock.Height.Value + 1;
                            var currBlockHeight = _branchHeightCache[_mainChain.ID];
                            if (currBlockHeight == prevBlock.Height) {
                                // Joined at the top of root, so it's still part of root
                                wipBlock.Block.Branch = _mainChain;
                            } else {
                                // Joined below top of root, so it's an orphan
                                var newBranch = NewBranch(wipBlock.Block.Height, wipBlock.Block.TimeStampUnix, wipBlock.Block.TimeStampUtc, _mainChain.ID);
                                wipBlock.Block.Branch = newBranch;
                            }
                            break;
                        default:
                            // joining to an orphan, three things could happen
                            //  1. orphan branch grows by one and stay's orphaned
                            //  2. orphan branch grows by one and becomes root branch    
                            //  3. orphan branch splits into two orphan branches    

                            wipBlock.Block.Height = prevBlock.Height.Value + 1;
                            var orphanBranchHeight = _branchHeightCache[prevBlock.Branch.ID];
                            if (orphanBranchHeight == prevBlock.Height) {
                                var rootHeight = _branchHeightCache[_mainChain.ID];
                                if (wipBlock.Block.Height > rootHeight) {
                                    #region  (2) Appends end of orphan branch, and orphan branch becomes root branch

                                    // avoid race conditions with parallel perist task (if running)
                                    using (WipPipelineScope.Current?.PipelineBlocks.EnterWriteScope()) {

                                        // 2.1 Get path from root branch to orphan branch
                                        var pathFromRoot = new Stack<Branch>(GetPathFromRootToOrphanBranch(prevBlock.Branch.ID));
                                        Debug.Assert(pathFromRoot.Count > 0);

                                        // 2.2 Split the root branch from where the path starts
                                        var _ignore = SplitBranch(
                                            _mainChain.ID,
                                            pathFromRoot.Peek().ForkHeight,
                                            pathFromRoot.Peek().TimeStampUnix,
                                            results
                                            );

                                        // 2.3 Migrate the orphan path blocks & sub-paths to the root branch
                                        while (pathFromRoot.Count > 0) {
                                            var orphanSegmentBranch = pathFromRoot.Pop();
                                            var orphanSegmanBranchHeight = _branchHeightCache[orphanSegmentBranch.ID];
                                            var migrateFromHeight = orphanSegmentBranch.ForkHeight;
                                            var migrateToHeight = pathFromRoot.Count > 0 ? pathFromRoot.Peek().ForkHeight - 1 : int.MaxValue;

                                            // Split the path segment from past the "toHeight" into a new branch (if applicable)
                                            if (orphanSegmanBranchHeight > migrateToHeight) {
                                                SplitBranch(orphanSegmentBranch.ID, migrateToHeight + 1, orphanSegmentBranch.TimeStampUnix, results);
                                            }
                                            // Migrate the path segment to the root branch
                                            MigrateBranch(orphanSegmentBranch.ID, migrateFromHeight, _mainChain, results);
                                            DeleteBranch(orphanSegmentBranch.ID);

                                        }
                                    }

                                    // current block joins end of root branch
                                    wipBlock.Block.Branch = _mainChain;

                                    #endregion
                                } else {
                                    #region (1) Appends the end of the orphan branch without changing anything

                                    wipBlock.Block.Branch = prevBlock.Branch;

                                    #endregion
                                }
                            } else {
                                #region (3) block forks the previous block branch without affecting root branch (since root is still larger)

                                wipBlock.Block.Branch = NewBranch(wipBlock.Block.Height, wipBlock.Block.TimeStampUnix, wipBlock.Block.TimeStampUtc, prevBlock.Branch.ID);

                                #endregion
                            }
                            break;
                    }
                    _branchHeightCache.Set(wipBlock.Block.Branch.ID, wipBlock.Block.Height);

                    #endregion
                }
                blocksToSequence.RemoveFirst();
                _processedBlocks.Add(wipBlock.Block.Hash, new BlockPtr(wipBlock.Block));
                results.Add(wipBlock);
            }

            //if (outOfSequenceBlockCount > 0) {
            //    Log.Warning("Encountered {0} out of sequence blocks", outOfSequenceBlockCount);
            //}
            //if (invalidBlockCount > 0) {
            //    Log.Warning("Encountered {0} invalid blocks", invalidBlockCount);
            //}
            unorganizedBlocks = danglingBlocks.ToArray();
            return results.ToArray();
        }

        protected virtual Branch NewBranch(int branchHeight, uint timeStampUnix, DateTime timeStampUtc, int parentBranch) {
            var branch = new Branch {
                ID = (int)(DAC.GetMaxID("Branch", "ID")) + 1,
                ForkHeight = branchHeight,
                TimeStampUnix = timeStampUnix,
                TimeStampUtc = timeStampUtc,
                ParentBranch = new Branch {ID = parentBranch}
            };
            DAC.InsertBranch(branch);
            _generatedBranches.Add(branch.ID, branch);
            return branch;
        }

        /// <summary>
        /// Splits a branch at a height. Blocks & sub-branches above split-point are moved to a new branch.
        /// </summary>
        /// <param name="branchID"></param>
        /// <param name="atHeight"></param>
        /// <returns></returns>
        /// <remarks>Operation performed on both in-memory, unpersisted objects as well as database</remarks>
        protected virtual Branch SplitBranch(int branchID, int atHeight, long unixTime, IEnumerable<WipBlock> justProcessedBlocks) {
            var currBranchHeight = _branchHeightCache[branchID];
            if (currBranchHeight < atHeight)
                return null;
            var newBranch =  NewBranch(atHeight, (uint)unixTime, Tools.Time.FromUnixTime((uint)unixTime), branchID);
            MigrateBranch(branchID, atHeight, newBranch, justProcessedBlocks);
            _branchHeightCache.Set(branchID, atHeight - 1);           
            return newBranch;
        }

        /// <summary>
        /// Moves blocks & sub-branches from the specified height and above to a new branch.
        /// </summary>
        /// <param name="fromBranchID"></param>
        /// <param name="fromBlockHeight"></param>
        /// <param name="toBranch"></param>
        /// <returns></returns>
        /// <remarks>Operation performed on both in-memory, unpersisted objects as well as database</remarks>
        protected virtual void MigrateBranch(int fromBranchID, int fromBlockHeight, Branch toBranch, IEnumerable<WipBlock> justProcessedBlocks) {

            // Mem - Migrate unpersisted blocks from branch and from block height to new branch
            foreach (var block in justProcessedBlocks.Where(b => b.Block.Branch?.ID == fromBranchID && b.Block.Height >= fromBlockHeight)) {
                block.Block.Branch = toBranch;
            }
            

            // Mem - Migrate unpersisted blocks from branch and from block height to new branch
            using (_processedBlocks.EnterWriteScope()) {
                foreach (var block in _processedBlocks.Values.Where(b => b.Branch.ID == fromBranchID && b.Height >= fromBlockHeight)) {
                    block.Branch = toBranch;
                }
            }

            // Pipeline blocks - migrate unpersisted blocks from branch and from block height to new branch
            var pipelineBlocks = WipPipelineScope.Current?.PipelineBlocks;
            if (pipelineBlocks != null) {
                using (pipelineBlocks.EnterWriteScope()) {
                    pipelineBlocks
                        .Where(i => i.Value.Block.Branch != null)
                        .Where(i => i.Value.Block.Branch.ID == fromBranchID && i.Value.Block.Height >= fromBlockHeight)
                        .ForEach(block => block.Value.Block.Branch = toBranch);
                }
            }


            // Cache - Migrate unpersisted blocks from branch and from block height to new branch
            using (_blockCache.EnterWriteScope()) {
                _blockCache
                    .GetCachedItems()
                    .Values
                    .Where(c => !c.Invalidated)
                    .Where(i => i.Value.Branch.ID == fromBranchID && i.Value.Height >= fromBlockHeight)
                    .ForEach(block => block.Value.Branch = toBranch);
            }
            using (_bulkLoadedBlockDbCache.EnterWriteScope()) {
                _bulkLoadedBlockDbCache
                    .GetCachedItems()
                    .Values
                    .Where(c => !c.Invalidated && c.Value != null)
                    .Where(i => i.Value.Branch.ID == fromBranchID && i.Value.Height >= fromBlockHeight)
                    .ForEach(block => block.Value.Branch = toBranch);

            }
			using (_processedBlocks.EnterWriteScope()) {
				_processedBlocks
					.Values
					.Where(v => v.Branch.ID == fromBranchID && v.Height >= fromBlockHeight)
					.ForEach(v => v.Branch = toBranch);
			}

			// Mem - Migrate sub-branches as well 
			using (_generatedBranches.EnterWriteScope()) {
                _generatedBranches
                    .Values
                    .Where(b => b.ParentBranch.ID == fromBranchID && b.ForkHeight > fromBlockHeight)
                    .ForEach(branch => branch.ParentBranch = toBranch);                
            }

            // DB - Migrate blocks & sub-branches from specified branch starting at height to a new branch
            using (_branchHeightCache.EnterWriteScope()) {
                if (_branchHeightCache[fromBranchID] >= fromBlockHeight) {
                    DAC.MigrateBranch(fromBranchID, fromBlockHeight, toBranch.ID);
                }
            }

            _branchHeightCache.Set(fromBranchID, fromBlockHeight - 1);
            _branchHeightCache.Invalidate(toBranch.ID);
        }

        protected virtual void DeleteBranch(int branchID) {
            DAC.DeleteBranch(branchID);
            if (_generatedBranches.ContainsKey(branchID)) {
                _generatedBranches.Remove(branchID);
            }
            _branchHeightCache.Invalidate(branchID);
        }

        protected virtual IEnumerable<Branch> GetPathFromRootToOrphanBranch(int endOrphanBranchID, HashSet<long> recursionPreventor = null) {
            if (recursionPreventor == null)
                recursionPreventor = new HashSet<long>();

            if (recursionPreventor.Contains(endOrphanBranchID))
                throw new InvalidDataException("Data corruption detected. Branch {0} has a cyclic dependency".FormatWith(endOrphanBranchID));

            var path = new List<Branch>();
            var segment = 
                _generatedBranches.ContainsKey(endOrphanBranchID) ? 
                _generatedBranches[endOrphanBranchID] : 
                DAC.GetBranchByID(endOrphanBranchID);
            path.Add(segment);
            if (segment.ParentBranch.ID != (long)KnownBranches.MainChain)
                path.AddRange(GetPathFromRootToOrphanBranch(segment.ParentBranch.ID, recursionPreventor));

           // path = path.OrderBy(b => b.ForkHeight).ThenBy(b => b.ID).ToList();
            return path;
        }

        protected int? MaxBranchHeightFromProcessedBlocks(int branchID) {
            // If a processed block refers to branch, use processed blocks for max height
            if (_processedBlocks.Values.Any(b => b.Branch.ID == branchID)) {
                return _processedBlocks.Values.Where(b => b.Branch.ID == branchID).Max(b => b.Height);
            }
            return null;
        }

        protected int? MaxBranchHeightFromPipelineStreamBlocks(int branchID) {
            var pipelineScope = WipPipelineScope.Current;
            if (pipelineScope != null) {
                using (pipelineScope.PipelineBlocks.EnterReadScope()) {
                    var relevantPipelineBlocks = pipelineScope.PipelineBlocks.Values.Where(b => b.Block.Branch != null && b.Block.Branch.ID == branchID);
                    if (relevantPipelineBlocks.Any()) {
                        return relevantPipelineBlocks.Max(b => b.Block.Height);
                    }
                }
            }
            return null;
        }

        protected int? MaxBranchHeightFromDB(int branchID) {
            using (var scope = DAC.BeginDirtyReadScope()) {
                return DAC.ExecuteScalar<int?>("SELECT MAX(Height) FROM Block WHERE BranchID = {0}".FormatWith(branchID));
            }
        }

        protected BlockPtr TryFindBlockInDatabase(byte[] hash) {
            using (BizLogicScope.Current.DAC.BeginDirtyReadScope()) {
                var results = BizLogicScope.Current.DAC.GetBlocksByHash(new[] {hash});
                if (results.Any()) {
                    return new BlockPtr(results.Single());
                }
                return null;
            }
        }

        protected BlockPtr TryFindProcessedBlock(byte[] hash) {
            // Try processed blocks list
            if (_processedBlocks.ContainsKey(hash))
                return _processedBlocks[hash];
            return null;
        }

        protected BlockPtr TryFindOrganizedBlockInPipelineStream(byte[] hash) {
            // Try pipeline blocks list
            var pipelineScope = WipPipelineScope.Current;
            if (pipelineScope != null) {
                using (pipelineScope.PipelineBlocks.EnterReadScope()) {
                    if (pipelineScope.PipelineBlocks.ContainsKey(hash)) {
                        var piplineBlock = pipelineScope.PipelineBlocks[hash];
                        if (piplineBlock.Block.Branch != null) // if null, means just scanned not processed
                            return new BlockPtr(piplineBlock.Block);
                    }
                }
            }
            return null;
        }



        protected IDictionary<byte[], BlockPtr> LoadAllDatabaseBlocks() {
            using (BizLogicScope.Current.DAC.BeginDirtyReadScope()) {
                return
                    BizLogicScope
                        .Current
                        .DAC
                        .Select("Block", new[] {"Hash", "BranchID", "Height"})
                        .Rows
                        .Cast<DataRow>()
                        .Distinct(new ActionEqualityComparer<DataRow>((r1, r2) => ByteArrayEqualityComparer.Instance.Equals(r1.Get<byte[]>("Hash"), r2.Get<byte[]>("Hash"))))
                        .ToDictionary(
                            r => r.Get<byte[]>("Hash"),
                            r => new BlockPtr(r.Get<int>("Height"), new Branch {ID = r.Get<int>("BranchID")})
                        );
            }
        }

        #region Inner Classes
        protected class BlockPtr {

            public BlockPtr(Block block) : this(block.Height, block.Branch) {
            }
            public BlockPtr(int? height, Branch branch) {
                Height = height;
                Branch = branch;
            }

            public int? Height;
            public Branch Branch;
        }

        #endregion
    }
}
