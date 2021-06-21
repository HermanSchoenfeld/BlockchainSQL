using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing
{
    public class ExpandScriptsTask : BizComponent, IPreProcessingTask {
        public IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {
            var blocksArr = blocks as WipBlock[] ?? blocks.ToArray();
            Parallel.ForEach(
                blocks
                    .SelectMany(b => b.Block.Transactions),
            BitcoinProtocolParser.ExpandTransactionItemScript
            );
            return blocksArr;
        }
        public ExecutionContext Context => ExecutionContext.Parallel;

        public int Priority => 0;
    }
}
