// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Linq;
using System.Threading;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
	public class NodeBlockStream : BlockStreamBase {
        public const int DefaultFetchBatchSize = 10;
        private readonly INodeCommunicator _nodeCommunicator;
        private long _blocksReceived;

        public NodeBlockStream( INodeCommunicator communicator, IBlockLocator locator, int fetchBatchSize = DefaultFetchBatchSize) {
            _blocksReceived = 0L;
            BatchFetchBlockCount = fetchBatchSize;
            _nodeCommunicator = communicator;
            Locator = locator;
            Communicator = communicator;
        }

        public override bool HasMore {
            get {
                CheckOpened();
	            var locators = Locator.GetBlockLocators();
				return Communicator.GetPeerChainHeight().ResultSafe() > (locators.Locations.Length > 0 ? locators.Locations[0].Height : 0);                
            }
        }

        public INodeCommunicator Communicator { get; private set; }
        public IBlockLocator Locator { get; private set; }

        public int BatchFetchBlockCount { get; set; }

        protected override void OpenInternal() {
            _nodeCommunicator.GetPeerChainHeight().ResultSafe();
        }

        protected override void CloseInternal() {
            // do nothing
        }

        protected override int SeekToTipInternal(BlockLocators blockLocators, CancellationToken cancellationToken) {
            // do nothing
            return 0;
        }

        protected override BlockStreamReadResult ReadNextBlocksInternal(bool skipTransactions, CancellationToken cancellationToken) {
            var locators = Locator.GetBlockLocators();
            var localChainHeight = (locators.Locations.Any() ? locators.Locations[0].Height : 0).ClipTo(0, int.MaxValue);
            var startTime = DateTime.Now;
            var nextBlocks = _nodeCommunicator.FetchNextBlocks(locators, BatchFetchBlockCount, skipTransactions).ResultSafe(cancellationToken);
            var endTime = DateTime.Now;
            return new BlockStreamReadResult {
                BlocksRead = nextBlocks.Blocks.Select(b => new WipBlock(b, startTime, endTime, 0)).ToArray(),
                HasMoreBlocks = localChainHeight + nextBlocks.Blocks.Length < nextBlocks.NodeHeight,
                PercentageSourceRead = nextBlocks.NodeHeight > 0 ?  (int) Math.Floor((localChainHeight / (double)nextBlocks.NodeHeight)*100.0D) : 0
            };
        }
    }
}
