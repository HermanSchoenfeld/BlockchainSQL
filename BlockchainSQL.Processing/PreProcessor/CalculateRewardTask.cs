using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen.Data;

namespace BlockchainSQL.Processing {
	public class CalculateRewardTask : BizComponent, IPreProcessingTask {
        public IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {
            var blocksArr = blocks as WipBlock[] ?? blocks.ToArray();
            Parallel.ForEach(blocksArr.Select(b => b.Block), SetBlockReward);
            return blocksArr;
        }
        public ExecutionContext Context => ExecutionContext.Parallel;

        public int Priority => 0;


        private void SetBlockReward(Block block) {
            // Calculate expected reward for height (protocol rule)
            var blockReward = BitcoinProtocolHelper.CalculateReward((uint)block.Height);
            // Set block reward property
            block.RewardBTC = blockReward / 100000000.0M;
        }

    }
}
