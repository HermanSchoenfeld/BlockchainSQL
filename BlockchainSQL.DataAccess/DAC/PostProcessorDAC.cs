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

        public void ConnectOutpoints() {
            this.ExecuteNonQuery(ConnectOutpointsQuery);
        }

        public void ConnectOutpoints(long fromTransactionInputID, long toTransactionInputID) {
            var query = ConnectOutpointsQuery.Replace("---", string.Empty).FormatWith(fromTransactionInputID, toTransactionInputID);
            this.ExecuteNonQuery(query);
        }

        public void TotalizeTransactions() {
            this.ExecuteNonQuery(TotalizeTransactionsQuery);
        }

        public void TotalizeTransactions(long fromTransactionInputID, long toTransactionInputID) {
            var query = TotalizeTransactionsQuery.Replace("---", string.Empty).FormatWith(fromTransactionInputID, toTransactionInputID);
            this.ExecuteNonQuery(query);
        }

        public void TotalizeBlocks() {
            this.ExecuteNonQuery(TotalizeBlocksQuery);
        }

        public void TotalizeBlocks(long fromBlockID, long toBlockID) {
            var query = TotalizeBlocksQuery.Replace("---", string.Empty).FormatWith(fromBlockID, toBlockID);
            this.ExecuteNonQuery(query);
        }

    }
}