using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public interface IBlockLocator {

        BlockLocators GetBlockLocators();
    }
}
