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