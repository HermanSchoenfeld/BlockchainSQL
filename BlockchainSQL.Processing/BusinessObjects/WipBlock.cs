// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
	public class WipBlock {
        public Block Block;
        public DateTime StartScanTime;
        public DateTime FinishScanTime;
        public TimeSpan OrganizeDuration;
        public DateTime StartParallelizedProcessingTime;
        public DateTime EndParallelizedProcessingTime;
        public int BlockchainSourcePercentage;

        public WipBlock(Block block, DateTime startScanTime, DateTime endScanTime, int percentageScanned ) {
            Block = block;
            StartScanTime = startScanTime;
            FinishScanTime = endScanTime;
            BlockchainSourcePercentage = percentageScanned;
        }

        public static long Estimate(WipBlock result) {
            if (result == null)
                return 0;
            var size = 5 * 64L;
            size += SizeEstimator.Estimate(result.Block);
            return size;
        }
    }

}
