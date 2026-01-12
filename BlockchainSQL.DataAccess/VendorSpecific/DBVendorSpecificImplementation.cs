// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Data;
using System.Linq;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
	public abstract class DBVendorSpecificImplementationBase : IDBVendorSpecificImplementation {
		const string ConnectOutpointsQuery =
			#region Query

			@"UPDATE 
	TransactionInput
SET
	TransactionOutputID = TXO.ID,
	Value = TXO.Value,
	RowState = 2
FROM
	TransactionInput TXI INNER JOIN
	[Transaction] TXO_TX ON TXI.OutpointTXID = TXO_TX.TXID INNER JOIN
	Block TXO_B ON TXO_TX.BlockID = TXO_B.ID INNER JOIN
	TransactionOutput TXO ON TXO_TX.ID = TXO.TransactionID AND TXO.[Index] = TXI.OutpointIndex
WHERE
	---TXI.ID >= {0} AND TXI.ID <= {1} AND
	TXI.RowState = 1 AND
	TXO_B.BranchID = 1";
		#endregion

		const string TotalizeTransactionsQuery =
			#region
			@"UPDATE
	[Transaction] 
SET
	InputsBTC = I.TOT,
	OutputsBTC = O.TOT,
	FeeBTC = I.TOT - O.TOT,
	RowState = 2
FROM
	[Transaction] T INNER JOIN (
		SELECT TransactionID, SUM(CAST( ISNULL(Value,0) AS DECIMAL(38,8)) / 100000000 ) AS TOT
		FROM TransactionInput  
		GROUP BY TransactionID
	) I ON T.ID = I.TransactionID INNER JOIN (
		SELECT TransactionID, SUM(CAST( ISNULL(Value,0) AS DECIMAL(38,8)) / 100000000 ) AS TOT
		FROM TransactionOutput  
		GROUP BY TransactionID
	) O ON T.ID = O.TransactionID
WHERE
	---T.ID >= {0} AND T.ID <= {1} AND
	RowState = 1";

		#endregion

		const string TotalizeBlocksQuery =
			#region

			@"UPDATE
	Block 
SET
	OutputsBTC = BS.O,
	FeesBTC = BS.F,
	RowState = 2
FROM
	Block B INNER JOIN (
		SELECT TX.BlockID, SUM(TX.InputsBTC) AS I, SUM(TX.OutputsBTC) AS O, SUM(TX.FeeBTC) AS F
		FROM [Transaction] TX
		---WHERE TX.BlockID >= {0} AND TX.BlockID <= {1}
		GROUP BY TX.BlockID
	) BS ON B.ID = BS.BlockID 
WHERE
	RowState = 1";

		#endregion

		public virtual string GenerateConnectOutpointsQuery() {
			return ConnectOutpointsQuery;
		}

		public virtual string GenerateConnectOutpointsQuery(long fromTransactionInputID, long toTransactionInputID) {
			return ConnectOutpointsQuery.Replace("---", string.Empty).FormatWith(fromTransactionInputID, toTransactionInputID);
		}

		public virtual string GenerateTotalizeTransactionsQuery() 
			=> TotalizeTransactionsQuery;

		public virtual string GenerateTotalizeTransactionsQuery(long fromTransactionInputID, long toTransactionInputID) 
			=> TotalizeTransactionsQuery.Replace("---", string.Empty).FormatWith(fromTransactionInputID, toTransactionInputID);

		public virtual string GenerateTotalizeBlocksQuery() 
			=> TotalizeBlocksQuery;

		public virtual string GenerateTotalizeBlocksQuery(long fromBlockID, long toBlockID) 
			=> TotalizeBlocksQuery.Replace("---", string.Empty).FormatWith(fromBlockID, toBlockID);

		public abstract bool HasDisabledApplicationIndexes(IDAC dac);

		public abstract void EnableAllApplicationIndexes(IDAC dac);

		public abstract void DisableAllApplicationIndexes(IDAC dac);

		public abstract void CleanupDatabase(IDAC dac);

		public abstract DataTable ExecuteUserSQL(IDAC dac, string userSql, int page, int pageSize, string orderByHint,
		                                         out int pageCount);
		public abstract IEnumerable<StatementLine> GetStatementLines(IDAC dac, string address);

		public virtual bool IsValidSchema(IDAC dac) 
			=> dac.GetSchema()
				.Tables
				.Select(t => t.Name)
				.ContainsAll(
					"Branch",
					"Block",
					"Transaction",
					"TransactionInput",
					"TransactionOutput",
					"Script",
					"ScriptInstruction",
					"Text"
				);
	}
}

/*


Branch

Block

Transaction

TransactionInput

TransactionOutput

Script

ScriptInstruction

Text

*/