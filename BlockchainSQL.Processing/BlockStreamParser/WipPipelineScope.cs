using System;
using System.Collections.Generic;
using System.Linq;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public sealed class WipPipelineScope : ScopeContext<WipPipelineScope> {
        private const string ContextID = "FDDBCCE3-37C0-422F-8FCA-1AD316D4F51B";
        public readonly SyncronizedDictionary<byte[], WipBlock> PipelineBlocks;
        public readonly SyncronizedDictionary<byte[], Transaction> PipelineTransactions;

        public WipPipelineScope()
            : base(ContextID, ScopeContextPolicy.MustBeRoot) {
            PipelineBlocks = new SyncronizedDictionary<byte[], WipBlock>(ByteArrayEqualityComparer.Instance);
            PipelineTransactions = new SyncronizedDictionary<byte[], Transaction>(ByteArrayEqualityComparer.Instance);
            ProcessingTaskShouldExpandScripts = false;
        }

        public static WipPipelineScope Current => GetCurrent(ContextID);

        public bool ProcessingTaskShouldExpandScripts { get; set; }

        public void AddBlocksToPipeline(WipBlock block) {
            AddBlocksToPipeline(new [] { block });
        }
        
        public void AddBlocksToPipeline(IEnumerable<WipBlock> blocks) {
            using (PipelineBlocks.EnterWriteScope()) {
                using (PipelineTransactions.EnterWriteScope()) {
                    foreach (var wipBlock in blocks) {
                        PipelineBlocks.Add(wipBlock.Block.Hash, wipBlock);
                        if (wipBlock.Block.Transactions != null) {
                            foreach (var txn in wipBlock.Block.Transactions) {
                                // Blocks version 1 allowed duplicated TXID's (https://bitcointalk.org/index.php?topic=216938.0)
                                // TXID: D5D27987D2A3DFC724E359870C6644B40E497BDC0589A033220FE15429D88599
                                // TXID: E3BF3D07D4B0375638D5F1DB5255FE07BA2C4CB067CD81B84EE974B6585FB468
                                // So we simply ignore them here
                                if (PipelineTransactions.ContainsKey(txn.TXID)) {
                                    continue;
                                }
                                PipelineTransactions.Add(txn.TXID, txn);
                            }
                        }
                    }
                }
            }
        }

        public void RemoveBlockFromPipeline(Block block) {
            RemoveBlocksFromPipeline(new [] { block });
        }

        public void RemoveBlocksFromPipeline(IEnumerable<Block> blocks) {
            using (PipelineBlocks.EnterWriteScope())
            using (PipelineTransactions.EnterWriteScope()) {
                foreach (var block in blocks) {
                    PipelineBlocks.Remove(block.Hash);
                    if (block.Transactions != null) {
                        foreach (var txn in block.Transactions) {
                            PipelineTransactions.Remove(txn.TXID);
                        }
                    }
                }
            }
        }

        public void ClearPipelineData() {
            PipelineBlocks.Clear();
            PipelineTransactions.Clear();
        }

        protected override void OnScopeEnd(WipPipelineScope rootScope, bool inException) {
            PipelineTransactions.Clear();
            PipelineBlocks.Clear();
        }
 
    }
}
