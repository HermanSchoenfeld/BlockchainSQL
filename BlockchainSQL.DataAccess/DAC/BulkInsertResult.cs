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
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.DataAccess {
    public class BulkInsertResult {

        public BulkInsertResult(BulkInsertTable tableName, DataTable newRows, DataColumn idColumn)
            : this(
                  tableName,
                  newRows.Rows.Count > 0 ? Tools.Object.ChangeType<long>(newRows.Rows[0][idColumn]) : 0,
                  newRows.Rows.Count > 0 ? Tools.Object.ChangeType<long>(newRows.Rows[newRows.Rows.Count - 1][idColumn]) : 0) {
        }


        public BulkInsertResult(BulkInsertTable table, long from, long to) {
            Table = table;
            FromID = from;
            ToID = to;
        }
        public readonly BulkInsertTable Table;

        public readonly long FromID;

        public readonly long ToID;

        public enum BulkInsertTable {
            Block,
            Transaction,
            TransactionInput,
            TransactionOutput,
            Script,
            ScriptInstruction
        }
    }
}
