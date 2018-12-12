using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public class NoOpVendorSpecificImplementation : IVendorSpecificImplementation {

        public bool HasDisabledApplicationIndexes(IDAC dac) {
            return false;
        }

        public void EnableAllApplicationIndexes(IDAC dac) {            
            // do nothing
        }


        public void DisableAllApplicationIndexes(IDAC dac) {
            // do nothing
        }

        public void CleanupDatabase(IDAC dac) {
            // do nothing
        }

        public DataTable ExecuteUserSQL(IDAC dac, string userSql, int page, int pageSize, string orderByHint, out int pageCount) {
           throw new NotSupportedException(); 
        }

        public IEnumerable<StatementLine> GetStatementLines(IDAC dac, string address) {
            throw new NotSupportedException();
        }
    }
}
