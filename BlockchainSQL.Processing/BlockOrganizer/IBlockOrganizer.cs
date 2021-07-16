using System.Collections.Generic;

namespace BlockchainSQL.Processing {
	public interface IBlockOrganizer : IBizComponent {
        WipBlock[] Organize(IEnumerable<WipBlock> blocks, out WipBlock[] danglingBlocks);
    }
}
