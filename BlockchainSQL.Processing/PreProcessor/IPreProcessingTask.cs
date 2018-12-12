using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public interface IPreProcessingTask : IBizComponent {
        IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken);

        ExecutionContext Context { get; }
        int Priority { get; }
    }
}
