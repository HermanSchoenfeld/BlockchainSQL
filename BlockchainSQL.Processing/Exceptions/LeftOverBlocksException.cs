using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
    public class LeftOverBlocksException : BlockchainSQLException {
        public LeftOverBlocksException(int numBlocksOutstanding ) 
            : base("Unable to organize a total of {0} blocks, resulting data may be corrupt", numBlocksOutstanding) {            
        }
    }
}
