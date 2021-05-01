using System.Collections.Generic;
using System.Threading;

namespace BlockchainSQL.Processing
{
    public interface IPreProcessingTask : IBizComponent {
        IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken);

        ExecutionContext Context { get; }
        int Priority { get; }
    }
}
