using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
    public interface IPostProcessingTask : IBizComponent {

        void ExecuteAll();
        void ExecutePartial(PersistResult newPersistSet);

        int Priority { get; set; }
    }
}
