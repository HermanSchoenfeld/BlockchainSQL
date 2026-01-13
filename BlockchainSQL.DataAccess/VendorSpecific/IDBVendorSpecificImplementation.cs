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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {

	public interface IDBVendorSpecificImplementation {

		string GenerateConnectOutpointsQuery();

		string GenerateConnectOutpointsQuery(long fromTransactionInputID, long toTransactionInputID);

		string GenerateTotalizeTransactionsQuery();

		string GenerateTotalizeTransactionsQuery(long fromTransactionInputID, long toTransactionInputID);

		string GenerateTotalizeBlocksQuery();

		string GenerateTotalizeBlocksQuery(long fromBlockID, long toBlockID);

		bool HasDisabledApplicationIndexes(IDAC dac);

        void EnableAllApplicationIndexes(IDAC dac);

        void DisableAllApplicationIndexes(IDAC dac);

        void CleanupDatabase(IDAC dac);

		DataTable ExecuteUserSQL(IDAC dac, string userSql, int page, int pageSize, string orderByHint, out int pageCount);

        IEnumerable<StatementLine> GetStatementLines(IDAC dac, string address);

        bool IsValidSchema(IDAC dac);

    }
}
