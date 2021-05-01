using System.Linq;

namespace BlockchainSQL.DataObjects
{
    public static class BlockExtensions {

        public static bool IsGenesisBlock(this Block block) {
            return block.PreviousBlockHash != null &&  block.PreviousBlockHash.Length == 32 && block.PreviousBlockHash.All(c => c == 0);
        }
    }
}
