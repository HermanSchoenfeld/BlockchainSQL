using System.Collections.Generic;
using System.Threading;

namespace BlockchainSQL.Processing
{
    public interface IBlockStreamPersistor : IBizComponent {
        PersistResult Persist(IEnumerable<WipBlock> processedBlocks, bool saveScriptData, bool enforceFK, CancellationToken cancellationToken);

    }
}
