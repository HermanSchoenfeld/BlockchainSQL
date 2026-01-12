// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
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
	public class CalculateDifficultyTask : BizComponent, IPreProcessingTask {
        public CalculateDifficultyTask(IDifficultyCalculator difficultyCalculator) {
            Calculator = difficultyCalculator;
        }

        public IDifficultyCalculator Calculator { get; private set; }
        public IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {
            var blocksArr = blocks as WipBlock[] ?? blocks.ToArray();
            Parallel.ForEach(blocksArr.Select(b => b.Block), SetBlockDifficulty);
            return blocksArr;
        }
        public ExecutionContext Context => ExecutionContext.Parallel;

        public int Priority => 0;


        private void SetBlockDifficulty(Block block) {
            block.Difficulty = Calculator.CalculateDifficulty(block.Bits);
        }

    }
}
