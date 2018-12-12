using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
    public class BlockAlreadySequencedException : BlockchainSQLException {
        public BlockAlreadySequencedException(byte[] blockHash) 
            : base("Block '{0}' has already been sequenced", BitcoinProtocolHelper.BytesToString(blockHash)) {            
        }
    }
}
