using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing.Scanning;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
    public static class IBlockchainOrganizerExtensions {
        public static WipBlock[] Organize(this IBlockOrganizer organizer, IEnumerable<WipBlock> blocks) {
            WipBlock[] danglingBlocks;
            return organizer.Organize(blocks, out danglingBlocks);
        }
    }
}
