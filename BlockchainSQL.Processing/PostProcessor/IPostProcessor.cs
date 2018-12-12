using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
    public interface IPostProcessor : IBizComponent {

        void PostProcessAll();
        void PostProcessPartial(PersistResult newPersistSet);
        IPostProcessingTask[] Tasks { get; }
        
    }
}
