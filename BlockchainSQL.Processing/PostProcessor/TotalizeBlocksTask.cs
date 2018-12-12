using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    
    public class TotalizeBlocksTask : BizComponent, IPostProcessingTask {
        public void ExecuteAll() {
            DAC.TotalizeBlocks();
        }

        public void ExecutePartial(PersistResult newPersistSet) {
            DAC.TotalizeBlocks(newPersistSet.Block.From, newPersistSet.Block.To);
        }

        public int Priority { get; set; }
    }
}
