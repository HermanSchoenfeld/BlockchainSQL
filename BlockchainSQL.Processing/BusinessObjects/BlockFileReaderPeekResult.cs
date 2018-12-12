using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public class BlockFileReaderPeekResult {
        public byte[] NextBlockPrevBlockHash { get; set; }
        public long NextBlockSize { get; set; }

        public long AdditionalOffset { get; set; }
    }

}
