using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
    public class BlockFile {
        public int FileNumber;
        public long FileSize;
        public long AccumulatedFileSize;
        public string Path;
    }
}
