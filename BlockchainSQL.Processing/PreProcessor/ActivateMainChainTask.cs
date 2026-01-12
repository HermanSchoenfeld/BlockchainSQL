// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public class ActivateMainChainTask : BizComponent, IPreProcessingTask {
        private readonly List<WipBlock> _leftOverBlocks;
        public ActivateMainChainTask(IBlockOrganizer blockOrganizer) {
            Organizer = blockOrganizer;
            _leftOverBlocks = new List<WipBlock>();
        }

        public IBlockOrganizer Organizer { get; private set; }

        public IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {
            var start = DateTime.Now;
            WipBlock[] danglingBlocks;
            var outputBlocks = Organizer.Organize(_leftOverBlocks.Concat(blocks), out danglingBlocks);
            _leftOverBlocks.Clear();
            _leftOverBlocks.AddRange(danglingBlocks);
            var organizeDuration = DateTime.Now.Subtract(start);
            outputBlocks.Update(o => o.OrganizeDuration = organizeDuration);
            return outputBlocks;
        }

        public ExecutionContext Context => ExecutionContext.Sequential;

        public int Priority => 0;
    }
}
