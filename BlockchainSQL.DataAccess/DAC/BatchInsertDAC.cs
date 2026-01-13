// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
	public partial class ApplicationDAC {
		public virtual IEnumerable<BulkInsertResult> BatchInsertBlocks(IEnumerable<Block> blocks, bool saveScriptData, bool enforceFK) {
			using (var scope = this.BeginScope()) {
				var blockTable = CreateBlockTable(this.GetMaxIDFast("Block", "ID"));
				var blockArr = blocks as Block[] ?? blocks.ToArray();
				foreach (var block in blockArr) {
					var blockRow = blockTable.NewRow();
					block.ID = int.Parse(blockRow["ID"].ToString());
					blockRow["Height"] = (object)block.Height ?? DBNull.Value;
					blockRow["Size"] = block.Size;
					blockRow["Nonce"] = block.Nonce;
					blockRow["PreviousBlockHash"] = block.PreviousBlockHash;
					blockRow["Hash"] = block.Hash;
					blockRow["Difficulty"] = block.Difficulty;
					blockRow["TimeStampUnix"] = block.TimeStampUnix;
					blockRow["TimeStampUtc"] = block.TimeStampUtc;
					blockRow["MerkleRoot"] = block.MerkleRoot;
					blockRow["Bits"] = block.Bits;
					blockRow["Version"] = block.Version;
					blockRow["TransactionCount"] = block.TransactionCount;
					blockRow["OutputsBTC"] = block.OutputsBTC ?? (object)DBNull.Value;
					blockRow["RewardBTC"] = block.RewardBTC ?? (object)DBNull.Value;
					blockRow["FeesBTC"] = block.FeesBTC ?? (object)DBNull.Value;
					blockRow["BranchID"] = block.Branch != null ? (object)block.Branch.ID : DBNull.Value;
					blockRow["RowState"] = block.RowState;
					blockTable.Rows.Add(blockRow);
				}
				BulkInsert(blockTable, enforceFK);
				var range = BatchInsertTransactions(blockArr.SelectMany(b => b.Transactions ?? new Transaction[0]),
					saveScriptData,
					enforceFK);

				scope.Commit();
				return range.Concat(new BulkInsertResult(BulkInsertResult.BulkInsertTable.Block, blockTable, blockTable.Columns["ID"]));
			}
		}

		public virtual IEnumerable<BulkInsertResult> BatchInsertTransactions(IEnumerable<Transaction> transactions, bool saveScriptData,
		                                                                     bool enforceFK) {
			var txTable = CreateTransactionTable(this.GetMaxIDFast("Transaction", "ID"));

			var transactionsArr = transactions as Transaction[] ?? transactions.ToArray();
			foreach (var tx in transactionsArr) {
				var txRow = txTable.NewRow();
				tx.ID = long.Parse(txRow["ID"].ToString());
				txRow["TXID"] = tx.TXID;
				txRow["WTXID"] = tx.WTXID;
				txRow["Version"] = tx.Version;
				txRow["Size"] = tx.Size;
				txRow["InputCount"] = tx.InputCount;
				txRow["Outputcount"] = tx.OutputCount;
				txRow["InputsBTC"] = tx.InputsBTC ?? (object)DBNull.Value;
				txRow["OutputsBTC"] = tx.OutputsBTC ?? (object)DBNull.Value;
				txRow["FeeBTC"] = tx.FeeBTC ?? (object)DBNull.Value;
				txRow["LockTime"] = tx.LockTime;
				txRow["Index"] = tx.Index;
				txRow["BlockID"] = tx.Block.ID;
				txRow["RowState"] = tx.RowState;
				txTable.Rows.Add(txRow);
			}
			BulkInsert(txTable, enforceFK);
			return
				(saveScriptData ? BatchInsertTransactionScripts(transactionsArr, enforceFK) : Enumerable.Empty<BulkInsertResult>())
				.Concat(BatchInsertInputs(transactionsArr.SelectMany(b => b.Inputs ?? new TransactionInput[0]),
					saveScriptData,
					enforceFK))
				.Concat(BatchInsertOutputs(transactionsArr.SelectMany(b => b.Outputs ?? new TransactionOutput[0]),
					saveScriptData,
					enforceFK))
				.Concat(new BulkInsertResult(BulkInsertResult.BulkInsertTable.Transaction, txTable, txTable.Columns["ID"]));
		}

		public virtual IEnumerable<BulkInsertResult>
			BatchInsertTransactionScripts(IEnumerable<Transaction> transactions, bool enforceFK) {
			var transactionsArr = transactions as Transaction[] ?? transactions.ToArray();
			var transactionScripts = transactionsArr
				.SelectMany(t => t.Inputs)
				.Concat(transactionsArr.SelectMany(t => t.Outputs).Cast<TransactionItem>())
				.Where(ti => ti.Script != null)
				.Select(ti => ti.Script);

			transactionScripts = transactionScripts.Concat(
					transactionsArr.SelectMany(x => x.Inputs)
						.Where(x => x.WitScript != null)
						.Select(x => x.WitScript))
				.ToArray();

			return BatchInsertScripts(transactionScripts, enforceFK);
		}

		public virtual IEnumerable<BulkInsertResult> BatchInsertScripts(IEnumerable<Script> scripts, bool enforceFK) {
			var scriptTable = CreateScriptTable(this.GetMaxIDFast("Script", "ID"));

			var scriptsArr = scripts as Script[] ?? scripts.ToArray();
			foreach (var script in scriptsArr) {
				var scriptRow = scriptTable.NewRow();
				script.ID = long.Parse(scriptRow["ID"].ToString());
				scriptRow["ScriptType"] = (int)script.ScriptType;
				scriptRow["ScriptClass"] = (int)script.ScriptClass;
				scriptRow["ScriptByteLength"] = script.ScriptByteLength;
				scriptRow["InstructionCount"] = script.InstructionCount;
				scriptRow["RowState"] = script.RowState;
				scriptTable.Rows.Add(scriptRow);
			}
			BulkInsert(scriptTable, enforceFK);
			var insertRanges =
				BatchInsertScriptInstructions(scriptsArr.SelectMany(s => s.Instructions ?? new ScriptInstruction[0]), enforceFK);
			return insertRanges.Concat(new BulkInsertResult(BulkInsertResult.BulkInsertTable.Script,
				scriptTable,
				scriptTable.Columns["ID"]));
		}

		public virtual IEnumerable<BulkInsertResult> BatchInsertScriptInstructions(
			IEnumerable<ScriptInstruction> scriptInstructions, bool enforceFK) {
			var scriptInstructionTable = CreateScriptInstructionTable(this.GetMaxIDFast("ScriptInstruction", "ID"));
			var scriptInstructionArr = scriptInstructions as ScriptInstruction[] ?? scriptInstructions.ToArray();
			foreach (var scriptInstruction in scriptInstructionArr) {
				var scriptInstructionRow = scriptInstructionTable.NewRow();
				scriptInstruction.ID = long.Parse(scriptInstructionRow["ID"].ToString());
				scriptInstructionRow["OpCode"] = (int)scriptInstruction.OpCode;
				scriptInstructionRow["Index"] = scriptInstruction.Index;
				scriptInstructionRow["Valid"] = scriptInstruction.Valid;
				scriptInstructionRow["DataLE"] = (object)scriptInstruction.DataLE ?? DBNull.Value;
				scriptInstructionRow["ScriptID"] = scriptInstruction.Script.ID;
				scriptInstructionRow["RowState"] = scriptInstruction.RowState;
				scriptInstructionTable.Rows.Add(scriptInstructionRow);
			}
			BulkInsert(scriptInstructionTable, enforceFK);
			return new[] {
				new BulkInsertResult(BulkInsertResult.BulkInsertTable.ScriptInstruction,
					scriptInstructionTable,
					scriptInstructionTable.Columns["ID"])
			};
		}

		public virtual IEnumerable<BulkInsertResult> BatchInsertInputs(IEnumerable<TransactionInput> inputs, bool saveScriptData,
		                                                               bool enforceFK) {
			var transactionInputTable = CreateTransactionInputsTable(this.GetMaxIDFast("TransactionInput", "ID"));
			var inputsArr = inputs as TransactionInput[] ?? inputs.ToArray();
			foreach (var input in inputsArr) {
				var txinRow = transactionInputTable.NewRow();
				input.ID = long.Parse(txinRow["ID"].ToString());
				txinRow["Index"] = input.Index;
				txinRow["RowState"] = input.RowState;
				txinRow["TransactionID"] = input.Transaction.ID;
				txinRow["ScriptID"] = saveScriptData && input.Script != null ? (object)input.Script.ID : DBNull.Value;
				txinRow["WitScriptId"] = saveScriptData && input.WitScript != null ? (object)input.WitScript.ID : DBNull.Value;
				txinRow["OutpointTXID"] = input.Outpoint.TXID;
				txinRow["OutpointIndex"] = input.Outpoint.OutputIndex;
				txinRow["TransactionOutputID"] = DBNull.Value;
				txinRow["Value"] = input.Value ?? (object)DBNull.Value;
				txinRow["Sequence"] = input.Sequence;
				transactionInputTable.Rows.Add(txinRow);
			}
			BulkInsert(transactionInputTable, enforceFK);
			return new[] {
				new BulkInsertResult(BulkInsertResult.BulkInsertTable.TransactionInput,
					transactionInputTable,
					transactionInputTable.Columns["ID"])
			};
		}

		public virtual IEnumerable<BulkInsertResult> BatchInsertOutputs(IEnumerable<TransactionOutput> outputs, bool saveScriptData,
		                                                                bool enforceFK) {
			var transactionOutputTable = CreateOutputsTable(this.GetMaxIDFast("TransactionOutput", "ID"));
			var outputArr = outputs as TransactionOutput[] ?? outputs.ToArray();
			foreach (var output in outputArr) {
				var txRow = transactionOutputTable.NewRow();
				output.ID = long.Parse(txRow["ID"].ToString());
				txRow["Index"] = output.Index;
				txRow["Value"] = output.Value;
				txRow["ToAddressType"] = output.ToAddressType;
				txRow["ToAddress"] = output.ToAddress != null ? (object)output.ToAddress : DBNull.Value;
				txRow["RowState"] = output.RowState;
				txRow["TransactionID"] = output.Transaction.ID;
				txRow["ScriptID"] = saveScriptData && output.Script != null ? (object)output.Script.ID : DBNull.Value;
				transactionOutputTable.Rows.Add(txRow);
			}
			BulkInsert(transactionOutputTable, enforceFK);
			return new[] {
				new BulkInsertResult(BulkInsertResult.BulkInsertTable.TransactionOutput,
					transactionOutputTable,
					transactionOutputTable.Columns["ID"])
			};
		}

		protected virtual DataTable CreateBlockTable(long currentMaxID) {
			DataTable blockTable = new DataTable("[Block]");
			DataColumn BlockId = new DataColumn("ID");
			BlockId.DataType = Type.GetType("System.Int64");
			BlockId.AutoIncrement = true;
			BlockId.AutoIncrementSeed = currentMaxID + 1;
			BlockId.AutoIncrementStep = 1;
			blockTable.Columns.Add(BlockId);
			blockTable.Columns.Add("Height", typeof(Int32));
			blockTable.Columns.Add("PreviousBlockHash", typeof(byte[]));
			blockTable.Columns.Add("Hash", typeof(byte[]));
			blockTable.Columns.Add("BranchID", typeof(Int32));
			blockTable.Columns.Add("Size", typeof(Int64));
			blockTable.Columns.Add("Nonce", typeof(Int64));
			blockTable.Columns.Add("TimeStampUnix", typeof(Int64));
			blockTable.Columns.Add("TimeStampUtc", typeof(DateTime));
			blockTable.Columns.Add("MerkleRoot", typeof(Byte[]));
			blockTable.Columns.Add("Bits", typeof(Int64));
			blockTable.Columns.Add("Difficulty", typeof(float));
			blockTable.Columns.Add("Version", typeof(Int32));
			blockTable.Columns.Add("TransactionCount", typeof(Int64));
			blockTable.Columns.Add("OutputsBTC", typeof(decimal));
			blockTable.Columns.Add("RewardBTC", typeof(decimal));
			blockTable.Columns.Add("FeesBTC", typeof(decimal));
			blockTable.Columns.Add("RowState", typeof(Int32));
			return blockTable;
		}

		protected virtual DataTable CreateTransactionTable(long currentMaxID) {
			DataTable txTable = new DataTable("[Transaction]");
			DataColumn txID = new DataColumn("ID");
			txID.DataType = Type.GetType("System.Int64");
			txID.AutoIncrement = true;
			txID.AutoIncrementSeed = currentMaxID + 1;
			txID.AutoIncrementStep = 1;
			txTable.Columns.Add(txID);
			txTable.Columns.Add("BlockID", typeof(Int64));
			txTable.Columns.Add("Index", typeof(Int64));
			txTable.Columns.Add("TXID", typeof(byte[]));
			txTable.Columns.Add("WTXID", typeof(byte[]));
			txTable.Columns.Add("Size", typeof(Int64));
			txTable.Columns.Add("InputCount", typeof(Int64));
			txTable.Columns.Add("OutputCount", typeof(Int64));
			txTable.Columns.Add("InputsBTC", typeof(decimal));
			txTable.Columns.Add("OutputsBTC", typeof(decimal));
			txTable.Columns.Add("FeeBTC", typeof(decimal));
			txTable.Columns.Add("LockTime", typeof(Int64));
			txTable.Columns.Add("Version", typeof(Int64));
			txTable.Columns.Add("RowState", typeof(Int32));
		
			return txTable;
		}

		protected virtual DataTable CreateScriptTable(long currentMaxID) {
			var scriptTable = new DataTable("[Script]");
			var ID = new DataColumn("ID");
			ID.DataType = Type.GetType("System.Int64");
			ID.AutoIncrement = true;
			ID.AutoIncrementSeed = currentMaxID + 1;
			ID.AutoIncrementStep = 1;
			scriptTable.Columns.Add(ID);
			scriptTable.Columns.Add("ScriptType", typeof(Int32));
			scriptTable.Columns.Add("ScriptClass", typeof(Int32));
			scriptTable.Columns.Add("ScriptByteLength", typeof(Int64));
			scriptTable.Columns.Add("InstructionCount", typeof(int));
			scriptTable.Columns.Add("RowState", typeof(Int32));
			return scriptTable;
		}

		protected virtual DataTable CreateScriptInstructionTable(long currentMaxID) {
			var scriptTable = new DataTable("[ScriptInstruction]");
			var ID = new DataColumn("ID");
			ID.DataType = Type.GetType("System.Int64");
			ID.AutoIncrement = true;
			ID.AutoIncrementSeed = currentMaxID + 1;
			ID.AutoIncrementStep = 1;
			scriptTable.Columns.Add(ID);
			scriptTable.Columns.Add("ScriptID", typeof(UInt64));
			scriptTable.Columns.Add("OpCode", typeof(Int32));
			scriptTable.Columns.Add("Index", typeof(Int32));
			scriptTable.Columns.Add("Valid", typeof(bool));
			scriptTable.Columns.Add("DataLE", typeof(byte[]));
			scriptTable.Columns.Add("RowState", typeof(Int32));

			return scriptTable;
		}

		protected virtual DataTable CreateTransactionInputsTable(long currentMaxID) {
			var inputsTable = new DataTable("[TransactionInput]");
			var ID = new DataColumn("ID");
			ID.DataType = Type.GetType("System.Int64");
			ID.AutoIncrement = true;
			ID.AutoIncrementSeed = currentMaxID + 1;
			ID.AutoIncrementStep = 1;
			inputsTable.Columns.Add(ID);
			inputsTable.Columns.Add("TransactionID", typeof(Int64));
			inputsTable.Columns.Add("Index", typeof(Int64));
			inputsTable.Columns.Add("ScriptID", typeof(Int64));
			inputsTable.Columns.Add("WitScriptID", typeof(Int64));
			inputsTable.Columns.Add("OutpointTXID", typeof(byte[]));
			inputsTable.Columns.Add("OutpointIndex", typeof(Int64));
			inputsTable.Columns.Add("TransactionOutputID", typeof(Int64));
			inputsTable.Columns.Add("Value", typeof(Int64));
			inputsTable.Columns.Add("Sequence", typeof(Int64));
			inputsTable.Columns.Add("RowState", typeof(Int32));

			return inputsTable;
		}

		protected virtual DataTable CreateOutputsTable(long currentMaxID) {
			DataTable outputsTable = new DataTable("[TransactionOutput]");
			DataColumn ID = new DataColumn("ID");
			ID.DataType = Type.GetType("System.Int64");
			ID.AutoIncrement = true;
			ID.AutoIncrementSeed = currentMaxID + 1;
			ID.AutoIncrementStep = 1;
			outputsTable.Columns.Add(ID);
			outputsTable.Columns.Add("TransactionID", typeof(Int64));
			outputsTable.Columns.Add("Index", typeof(Int64));
			outputsTable.Columns.Add("ToAddressType", typeof(byte));
			outputsTable.Columns.Add("ToAddress", typeof(string));
			outputsTable.Columns.Add("Value", typeof(Int64));
			outputsTable.Columns.Add("ScriptID", typeof(Int64));
			outputsTable.Columns.Add("RowState", typeof(Int32));
			return outputsTable;
		}

		private long GetMaxIDFast(string tableName, string idCol) {
			if (DecoratedDAC is MSSQLDAC) {
				return this.ExecuteScalar<long>("select IDENT_CURRENT('{0}')".FormatWith(tableName));
			}
			return this.GetMaxID(tableName, idCol);
		}

		private void BulkInsert(DataTable table, bool enforceFK) {
			var options = BulkInsertOptions.KeepIdentity;
			if (enforceFK)
				options |= BulkInsertOptions.MaintainForeignKeys;

			this.BulkInsert(table, options, TimeSpan.FromMinutes(5));

		}

	}
}
