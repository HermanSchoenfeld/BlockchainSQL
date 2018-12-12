using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public interface IBlockStreamPersistor : IBizComponent {
        PersistResult Persist(IEnumerable<WipBlock> processedBlocks, bool saveScriptData, bool enforceFK, CancellationToken cancellationToken);

    }
}
