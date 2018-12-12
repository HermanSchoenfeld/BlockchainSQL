using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.Code {
    public interface IBlockchainRepository {

        Task<IEnumerable<Block>> GetBlocks(int page, int pageSize, SortOption[] sortOptions);

        Task<Block> GetBlock(string hash);

		Task<Block> GetBlockByHeight(int height);

		Task<int> GetBlockCount();

        Task<IEnumerable<Transaction>> GetBlockTransactions(string blockHash, int page, int pageSize, SortOption[] sortOptions);

        Task<Transaction> GetTransaction(string txid);

        Task<IEnumerable<TransactionInput>> GetTransactionInputs(string txid);

        Task<IEnumerable<TransactionOutput>> GetTransactionOutputs(string txid);

        Task<IEnumerable<StatementLine>> GetStatementLines(string address);

        Task<ScriptSummary> GetScriptSummary(string txid, int index0, TransactionItemType itemType);

        Task<SearchResult> SearchHash(string text);

        Task<QueryResult> Execute(string sql, int page, int pageSize);

    }
}