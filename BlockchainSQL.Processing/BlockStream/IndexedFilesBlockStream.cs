using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BlockchainSQL.Processing.BusinessObjects;
using Sphere10.Framework;
using Sphere10.Framework.Windows.LevelDB;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.Processing
{

    public class IndexedFilesBlockStream : FilesBlockStreamBase {
        private readonly string _indexDBPath;
        private IDictionary<byte[], BlockIndex> _blockIndexByHash;
        private IDictionary<DiskPos, BlockIndex> _blockIndexByDiskPos; 
        private BlockIndex[] _blockIndexBySequence;
        private Queue<BlockIndex> _readQueue;

        public IndexedFilesBlockStream(string directoryPath, string indexDBPath) : base(directoryPath) {
            if (string.IsNullOrWhiteSpace(indexDBPath)) throw new ArgumentException("Null or whitespace", nameof(indexDBPath));
            var indexDBValidation = ProcessingTierHelper.ValidateLevelDBDirectory(indexDBPath);
            if (indexDBValidation.Failure) throw new ArgumentException(indexDBValidation.ToString(), nameof(indexDBValidation));
            _indexDBPath = indexDBPath;
        }

        public override bool HasMore => _readQueue.Count > 0;

        protected override void OpenInternal() {
            Log.Info("Examining block index");
            // load the block locations from indexDB
            _blockIndexBySequence = GetAllBlockLocations(_indexDBPath).Where(b => b.DataLocation != null).OrderBy(b => b.Height).ToArray();
            _blockIndexByHash = _blockIndexBySequence.ToDictionary(b => b.Hash, ByteArrayEqualityComparer.Instance);
            _blockIndexByDiskPos = _blockIndexBySequence.ToDictionary(b => b.DataLocation);
            _readQueue = new Queue<BlockIndex>(_blockIndexBySequence); // default read all

            base.OpenInternal();
        }

        protected override void CloseInternal() {
            base.CloseInternal();
            _blockIndexByDiskPos.Clear();
            _blockIndexByDiskPos.Clear();
            _readQueue.Clear();
            _blockIndexBySequence = null;
        }


        protected override IBlockFileReader NewBlockFileReader(string blockFilePath) {
            var reader = base.NewBlockFileReader(blockFilePath);
            return reader;
        }

        protected override DiskPos SearchTip(BlockFile[] blockFiles, BlockLocators blockLocators, CancellationToken cancellationToken) {
            foreach (var location in blockLocators.Locations) {
                BlockIndex blockIndex;
                if (ByteArrayEqualityComparer.Instance.Equals(location.Hash,BitcoinProtocolHelper.EmptyHash)) {
                    // tip is genesis block                    
                    if (_blockIndexByHash.TryGetValue(BitcoinProtocolHelper.GenesisHash, out blockIndex))
                        return blockIndex.DataLocation;
                    continue;
                }
                if (!_blockIndexByHash.TryGetValue(location.Hash, out blockIndex))
                    continue;
                // the tip is the next block after blockIndex, search by height
                var tip = _blockIndexByHash.Values.FirstOrDefault(b => b.Height == blockIndex.Height + 1 );
                if (tip != null)
                    return tip.DataLocation;
                break;
            }
            return DiskPos.None;
        }

        protected override void SeekToDiskPos(DiskPos pos) {
            if (!pos.Equals(DiskPos.None)) {
                BlockIndex startBlock;
                if (!_blockIndexByDiskPos.TryGetValue(pos, out startBlock))
                    throw new BlockchainSQLException("No block is available at disk position '{0}'", pos);

                if (_readQueue.Peek().DataLocation != pos) {
                    _readQueue = new Queue<BlockIndex>(
                        _blockIndexBySequence
                            .SkipWhile(b => b.DataLocation != pos)
                        );
                }
            }
            base.SeekToDiskPos(pos);
        }


        protected override BlockStreamReadResult ReadNextBlocksInternal(bool skipTransactions, CancellationToken cancellationToken) {
            if (_readQueue.Count == 0) {
                return BlockStreamReadResult.EndOfStream;
            }
            var startTime = DateTime.Now;
            var nextBlockIndex = _readQueue.Peek();
            SeekToDiskPos(nextBlockIndex.DataLocation);
            _readQueue.Dequeue(); // SeekToDiskPos override needs to know nextBlockIndex so we dequeue after seek
            var nextBlock = CurrentBlockFileReader.ReadNextBlock();
            var pc = CalculatePercentageProcessed();
            return new BlockStreamReadResult {
                BlocksRead = new[] { new WipBlock(nextBlock, startTime, DateTime.Now, pc) },
                HasMoreBlocks = _readQueue.Count > 0,
                PercentageSourceRead = pc
            };
        }

        /// <summary>
        /// Returns the locations of all the blocks
        /// </summary>
        /// <param name="indexDB"></param>
        /// <returns></returns>
        private IEnumerable<BlockIndex> GetAllBlockLocations(string indexDB) {
            // The LevelDB is never openend directly in order to avoid corrupting it. A copy is made and discarded.
            var clonedIndexDB = Tools.FileSystem.GetTempEmptyDirectory(true);
            using (new ActionScope(
                () => Tools.FileSystem.CopyDirectory(indexDB, clonedIndexDB, true, false, true), 
                () => Tools.FileSystem.DeleteDirectory(clonedIndexDB, true)))  
            using (var db = new DB(clonedIndexDB, new Options {CreateIfMissing = false})) {
                using (var iter = db.CreateIterator(new ReadOptions {FillCache = false, VerifyCheckSums = false})) {
                    iter.SeekToFirst();
                    while (iter.IsValid()) {
                        var key = iter.GetKey();
                        byte[] blockHashBytes;
                        if (IsBlockKey(key, out blockHashBytes)) {
                            yield return ParseBlockFileLocation(blockHashBytes, iter.GetValue());
                        }
                        iter.Next();
                    }
                }
            }
        }


        private static bool IsBlockKey(IReadOnlyList<byte> bytes, out byte[] hashBytes) {
            if (bytes.Count == 33 && bytes[0] == 'b') {
                hashBytes = bytes.Skip(1).ToArray();
                return true;
            }
            hashBytes = null;
            return false;
        }

        private BlockIndex ParseBlockFileLocation(byte[] blockHashBytes, byte[] blockIndexData) {
            #region Bitcoin Core snippet reference

            ////! pointer to the hash of the block, if any. Memory is owned by this CBlockIndex
            //const uint256* phashBlock;

            ////! pointer to the index of the predecessor of this block
            //CBlockIndex* pprev;

            ////! pointer to the index of some further predecessor of this block
            //CBlockIndex* pskip;

            ////! height of the entry in the chain. The genesis block has height 0
            //int nHeight;

            ////! Which # file this block is stored in (blk?????.dat)
            //int nFile;

            ////! Byte offset within blk?????.dat where this block's data is stored
            //unsigned int nDataPos;

            //if (!(nType & SER_GETHASH))
            //    READWRITE(VARINT(nVersion));

            //READWRITE(VARINT(nHeight));
            //READWRITE(VARINT(nStatus));
            //READWRITE(VARINT(nTx));
            //if (nStatus & (BLOCK_HAVE_DATA | BLOCK_HAVE_UNDO))
            //    READWRITE(VARINT(nFile));
            //if (nStatus & BLOCK_HAVE_DATA)
            //    READWRITE(VARINT(nDataPos));
            //if (nStatus & BLOCK_HAVE_UNDO)
            //    READWRITE(VARINT(nUndoPos));

            //// block header
            //READWRITE(this->nVersion);
            //READWRITE(hashPrev);
            //READWRITE(hashMerkleRoot);
            //READWRITE(nTime);
            //READWRITE(nBits);
            //READWRITE(nNonce);

            #endregion

            var loc = new BlockIndex();
            using (var reader = blockHashBytes.AsEndianReader(EndianBitConverter.Little)) {
                loc.Hash = BitcoinProtocolParser.ParseHashBytes(reader);
            }
            using (var reader = blockIndexData.AsEndianReader(EndianBitConverter.Little)) {
                loc.Version = (int)reader.ReadCompactVarInt();
                loc.Height = (uint)reader.ReadCompactVarInt();
                loc.Status = (BlockStatus) (uint)reader.ReadCompactVarInt();
                loc.TransactionCount = (uint)reader.ReadCompactVarInt();
                var hasData = loc.Status.HasFlag(BlockStatus.BLOCK_HAVE_DATA);
                var hasUndo = loc.Status.HasFlag(BlockStatus.BLOCK_HAVE_UNDO);
                if (hasData || hasUndo) {
                    var fileNo = (int) reader.ReadCompactVarInt();
                    if (hasData)
                        loc.DataLocation = new DiskPos(
                            fileNo,
                            ((uint) reader.ReadCompactVarInt() - 8).ClipTo(0, uint.MaxValue) // -8 offset to include magicid & blocksize
                            );
                    if (hasUndo)
                        loc.UndoLocation = new DiskPos(
                            fileNo,
                            ((uint) reader.ReadCompactVarInt() - 8).ClipTo(0, uint.MaxValue) // -8 offset to include magicid & blocksize
                            );

                } else {
                    var x = 1;
                }
            }
            return loc;
        }

    }
}
