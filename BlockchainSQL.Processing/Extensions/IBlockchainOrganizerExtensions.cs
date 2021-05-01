using System.Collections.Generic;

namespace BlockchainSQL.Processing
{
    public static class IBlockchainOrganizerExtensions {
        public static WipBlock[] Organize(this IBlockOrganizer organizer, IEnumerable<WipBlock> blocks) {
            WipBlock[] danglingBlocks;
            return organizer.Organize(blocks, out danglingBlocks);
        }
    }
}
