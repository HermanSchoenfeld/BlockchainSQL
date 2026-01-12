// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;

namespace BlockchainSQL.Web.Code {
	public interface IBlockchainRepository {

		Task<IEnumerable<Block>> GetBlocks(int page, int pageSize, SortOption[] sortOptions);

		Task<Block> GetBlock(string hash);

		Task<Block> GetBlockByHeight(long height);

		Task<int> GetBlockCount();

		Task<IEnumerable<Transaction>> GetBlockTransactions(string blockHash, int page, int pageSize, SortOption[] sortOptions);

		Task<Transaction> GetTransaction(string txid);

		Task<IEnumerable<TransactionInput>> GetTransactionInputs(string txid);

		Task<IEnumerable<TransactionOutput>> GetTransactionOutputs(string txid);

		Task<IEnumerable<StatementLine>> GetStatementLines(string address);

		Task<ScriptSummary> GetScriptSummary(long scriptId);

		Task<SearchResult> SearchHash(string text);

		Task<QueryResult> Execute(string sql, int page, int pageSize);

	}
}