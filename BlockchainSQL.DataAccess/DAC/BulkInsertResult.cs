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
