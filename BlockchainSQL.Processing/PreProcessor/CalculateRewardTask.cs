// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

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
