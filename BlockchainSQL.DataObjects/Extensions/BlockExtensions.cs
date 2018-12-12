using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.DataObjects {
    public static class BlockExtensions {

        public static bool IsGenesisBlock(this Block block) {
            return block.PreviousBlockHash != null &&  block.PreviousBlockHash.Length == 32 && block.PreviousBlockHash.All(c => c == 0);
        }
    }
}
