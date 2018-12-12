using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using NBitcoin.Crypto;
using NBitcoin.Protocol;

namespace BlockchainSQL.Processing {
    public class BlockStreamReadResult {

        public static BlockStreamReadResult EndOfStream => new BlockStreamReadResult {
            BlocksRead = new WipBlock[0],
            HasMoreBlocks = false,
            PercentageSourceRead = 100
        };

        public WipBlock[] BlocksRead { get; set; }

        public int PercentageSourceRead { get; set; }

        public bool HasMoreBlocks { get; set; }
    }
}
