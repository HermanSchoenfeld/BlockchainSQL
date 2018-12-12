using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
