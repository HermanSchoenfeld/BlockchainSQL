// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using BlockchainSQL.DataAccess;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public class BlockStreamPersistor : BizComponent, IBlockStreamPersistor {
        public PersistResult Persist(IEnumerable<WipBlock> processedBlocks, bool saveScriptData, bool enforceFK, CancellationToken cancellationToken) {
            var blockArr = processedBlocks as WipBlock[] ?? processedBlocks.ToArray();
            using (WipPipelineScope.Current?.PipelineBlocks.EnterWriteScope()) {
                using (var scope = DAC.BeginScope()) {
                    scope.BeginTransaction();                    
                    var bulkInsertResults = DAC.BatchInsertBlocks(blockArr.Select(i => i.Block), saveScriptData, enforceFK); // Cancellation token not passed to Wait since it can stuff up connnections
                    scope.Commit();
                    WipPipelineScope.Current?.RemoveBlocksFromPipeline(blockArr.Select(b => b.Block));
                    return ToPersistResult(bulkInsertResults);
                }
            }
        }

        private PersistResult ToPersistResult(IEnumerable<BulkInsertResult> bulkInsertResults) {
            var result = new PersistResult();
            foreach (var r in bulkInsertResults) {
                PersistResult.Range range;
                switch (r.Table) {
                    case BulkInsertResult.BulkInsertTable.Block:
                        range = result.Block;
                        break;
                    case BulkInsertResult.BulkInsertTable.Transaction:
                        range = result.Transaction;
                        break;
                    case BulkInsertResult.BulkInsertTable.TransactionInput:
                        range = result.TransactionInput;
                        break;
                    case BulkInsertResult.BulkInsertTable.TransactionOutput:
                        range = result.TransactionOutput;
                        break;
                    case BulkInsertResult.BulkInsertTable.Script:
                        range = result.Script;
                        break;
                    case BulkInsertResult.BulkInsertTable.ScriptInstruction:
                        range = result.ScriptInstruction;
                        break;
                    default:
                        throw new NotSupportedException(r.Table.ToString());
                }
                range.From = r.FromID;
                range.To = r.ToID;
                range.Total = r.ToID - r.FromID + 1;                
            }
            return result;
        }
    }
}
