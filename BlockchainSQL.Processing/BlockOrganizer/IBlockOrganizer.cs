using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public interface IBlockOrganizer : IBizComponent {
        WipBlock[] Organize(IEnumerable<WipBlock> blocks, out WipBlock[] danglingBlocks);
    }
}
