using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public interface IPreProcessor : IBizComponent {
        WipBlock[] PreProcess(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken);
    }
}
