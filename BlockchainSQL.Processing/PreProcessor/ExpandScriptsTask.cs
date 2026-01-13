// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
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
