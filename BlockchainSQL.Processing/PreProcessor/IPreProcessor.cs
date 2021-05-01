using System.Collections.Generic;
using System.Threading;

namespace BlockchainSQL.Processing
{
    public interface IPreProcessor : IBizComponent {
        WipBlock[] PreProcess(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken);
    }
}
