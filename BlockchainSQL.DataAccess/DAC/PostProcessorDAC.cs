using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen;
using Hydrogen.Data;
using Hydrogen.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC {

        public void ConnectOutpoints() {
	        var query = _vendorSpecificImplementation.GenerateConnectOutpointsQuery();
            this.ExecuteNonQuery(query);
        }

        public void ConnectOutpoints(long fromTransactionInputID, long toTransactionInputID) {
            var query = _vendorSpecificImplementation.GenerateConnectOutpointsQuery(fromTransactionInputID, toTransactionInputID);
			this.ExecuteNonQuery(query);
        }

        public void TotalizeTransactions() {
	        var query = _vendorSpecificImplementation.GenerateTotalizeTransactionsQuery();
			this.ExecuteNonQuery(query);
        }

        public void TotalizeTransactions(long fromTransactionInputID, long toTransactionInputID) {
	        var query = _vendorSpecificImplementation.GenerateTotalizeTransactionsQuery(fromTransactionInputID, toTransactionInputID);
            this.ExecuteNonQuery(query);
        }

        public void TotalizeBlocks() {
	        var query = _vendorSpecificImplementation.GenerateTotalizeBlocksQuery();
			this.ExecuteNonQuery(query);
        }

        public void TotalizeBlocks(long fromBlockID, long toBlockID) {
			var query = _vendorSpecificImplementation.GenerateTotalizeBlocksQuery(fromBlockID, toBlockID);
			this.ExecuteNonQuery(query);
        }

    }
}