using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen.Data;

namespace BlockchainSQL.DataAccess {
    public class NoOpVendorSpecificImplementation : DBVendorSpecificImplementationBase {

        public override bool HasDisabledApplicationIndexes(IDAC dac) {
            return false;
        }

        public override void EnableAllApplicationIndexes(IDAC dac) {            
            // do nothing
        }


        public override void DisableAllApplicationIndexes(IDAC dac) {
            // do nothing
        }

        public override void CleanupDatabase(IDAC dac) {
            // do nothing
        }

        public override DataTable ExecuteUserSQL(IDAC dac, string userSql, int page, int pageSize, string orderByHint, out int pageCount) {
           throw new NotSupportedException(); 
        }

        public override IEnumerable<StatementLine> GetStatementLines(IDAC dac, string address) {
            throw new NotSupportedException();
        }
    }
}
