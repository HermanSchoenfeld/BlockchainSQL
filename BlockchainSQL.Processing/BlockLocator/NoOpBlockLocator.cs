using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public class NoOpBlockLocator : IBlockLocator {
        public virtual BlockLocators GetBlockLocators() {
            throw new NotSupportedException();
        }

    }
}
