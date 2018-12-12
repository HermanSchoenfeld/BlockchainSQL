using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.DataObjects {
    public class BlockLocation {
        public byte[] Hash { get; set; }

        public int Height { get; set; }
    }


}
