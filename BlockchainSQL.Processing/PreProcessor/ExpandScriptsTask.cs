using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public class ExpandScriptsTask : BizComponent, IPreProcessingTask {
        public IDifficultyCalculator Calculator { get; private set; }
        public IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {
            var blocksArr = blocks as WipBlock[] ?? blocks.ToArray();
            Parallel.ForEach(
                blocks
                    .SelectMany(b => b.Block.Transactions.SelectMany(t => t.Inputs))
                    .Cast<TransactionItem>()
                    .Concat(
                        blocks.SelectMany(b => b.Block.Transactions.SelectMany(t => t.Outputs))
                    ).ToArray(),
                   BitcoinProtocolParser.ExpandTransactionItemScript
            );
            return blocksArr;
        }
        public ExecutionContext Context => ExecutionContext.Parallel;

        public int Priority => 0;



    }
}
