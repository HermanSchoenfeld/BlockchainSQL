using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    
   
    public class TotalizeTransactionsTask : BizComponent, IPostProcessingTask {
        public void ExecuteAll() {
            DAC.TotalizeTransactions();
        }

        public void ExecutePartial(PersistResult newPersistSet) {
            DAC.TotalizeTransactions(newPersistSet.Transaction.From, newPersistSet.Transaction.To);
        }

        public int Priority { get; set; }
    }
}
