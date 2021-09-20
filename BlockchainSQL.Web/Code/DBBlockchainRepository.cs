using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.Code {
	public class DBBlockchainRepository : IBlockchainRepository {
		private readonly ApplicationDAC _dac;


		public DBBlockchainRepository(string connString) {
			_dac = BlockchainDatabase.NewDAC(DBMSType.SQLServer, connString);
		}

		public Task<IEnumerable<Block>> GetBlocks(int page, int pageSize, SortOption[] sortOptions) {
			return WithDirtyScope(() => _dac.GetBlocks(pageSize, page * pageSize, sortOptions));
		}

		public Task<Block> GetBlock(string hash) {
			return WithDirtyScope(() => _dac.GetBlockByHash(BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(hash)));
		}

		public Task<Block> GetBlockByHeight(int height) {
			return WithDirtyScope(() => _dac.GetActiveBlockByHeight(height));
		}

		public Task<int> GetBlockCount() {
			return WithDirtyScope(() => _dac.CountBlocks((int)KnownBranches.MainChain));
		}

		public Task<IEnumerable<Transaction>> GetBlockTransactions(string blockHash, int page, int pageSize, SortOption[] sortOptions) {
			return WithDirtyScope(() => {
				var block = _dac.GetBlockByHash(BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(blockHash));
				return _dac.GetTransactionsByBlockID(block.ID, pageSize, page * pageSize, sortOptions);
			});
		}

		public Task<Transaction> GetTransaction(string txid) {
			return WithDirtyScope(() => _dac.GetTransactionByTXID(BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(txid), true));
		}

		public Task<IEnumerable<TransactionInput>> GetTransactionInputs(string txid) {
			return WithDirtyScope(() => _dac.GetTransactionInputsByTXID(BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(txid), false, true));
		}

		public Task<TransactionInput> GetTransactionInputById(long id) {
			return WithDirtyScope(() => _dac.GetTransactionInputById((uint)id));
		}

		public Task<IEnumerable<TransactionOutput>> GetTransactionOutputs(string txid) {
			return WithDirtyScope(() => _dac.GetTransactionOutputsByTXID(BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(txid), false));
		}

		public async Task<IEnumerable<StatementLine>> GetStatementLines(string address) {
			if (string.IsNullOrWhiteSpace(address) || !BitcoinProtocolHelper.IsValidAddress(address))
				throw new ArgumentException("'{0}' is not a valid address".FormatWith(address ?? "NULL"), nameof(address));

			using (var scope = await Task.Run(() => _dac.BeginDirtyReadScope())) {
				return await Task.Run(() => _dac.GetStatementLines(address));
			}
		}

		public Task<ScriptSummary> GetScriptSummary(long scriptId) {
			return WithDirtyScope(() => _dac.GetScriptSummary(scriptId));
		}

		public async Task<SearchResult> SearchHash(string text) {
			if (BitcoinProtocolHelper.IsValidHashString(text)) {
				var hashBytes = BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(text);
				using (await Task.Run(() => _dac.BeginDirtyReadScope())) {
					if (await Task.Run(() => _dac.BlockExists(hashBytes))) {
						return new SearchResult {
							Key = text.ToUpperInvariant(),
							ResultType = SearchResultType.Block
						};
					}
					if (await Task.Run(() => _dac.TransactionExists(hashBytes))) {
						return new SearchResult {
							Key = text.ToUpperInvariant(),
							ResultType = SearchResultType.Transaction
						};
					}
				}
			}
			return new SearchResult {
				ResultType = SearchResultType.NotFound,
				Key = null
			};
		}


		public async Task<QueryResult> Execute(string sql, int page, int pageSize) {
			int newPageCount = 0;

			string orderByHint = null;
			#region Extract order by clause for mssql CTE
			var queryXml =
				Microsoft.SqlServer.Management.SqlParser.Parser.Parser.Parse(sql)
					.Script
					.Xml
					.ToString();

			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(queryXml);
			var orderBys = xmlDoc.SelectNodes("/SqlScript/SqlBatch/SqlSelectStatement/SqlSelectSpecification/SqlOrderByClause");

			if (orderBys.Count > 0) {
				var last = orderBys.Item(orderBys.Count - 1);
				orderByHint = last.FirstChild?.Value;
				if (!string.IsNullOrWhiteSpace(orderByHint)) {
					orderByHint = orderByHint.Replace("\r", string.Empty);
					sql = sql.Replace(orderByHint, "/*" + orderByHint + "*/");
					orderByHint = orderByHint.Replace("\n", string.Empty).Replace("\r", string.Empty);
				}

			}
			#endregion

			var result = await WithDirtyScope(() => _dac.ExecuteUserSQL(sql, page, pageSize, orderByHint, out newPageCount));

			return new QueryResult {
				Page = page,
				PageSize = pageSize,
				PageCount = newPageCount,
				Data = result
			};
		}

		private async Task<T> WithDirtyScope<T>(Func<T> func) {
			using (_dac.BeginDirtyReadScope(true)) {
				return await Task.Run(func);
			}
		}
	}
}