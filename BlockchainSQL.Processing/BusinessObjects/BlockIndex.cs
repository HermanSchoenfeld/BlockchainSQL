using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing.BusinessObjects {
    
    public class BlockIndex {
        public byte[] Hash;
        public int Version;
        public uint Height;
        public BlockStatus Status;
        public uint TransactionCount;
        public DiskPos DataLocation;
        public DiskPos UndoLocation;
    }
}
