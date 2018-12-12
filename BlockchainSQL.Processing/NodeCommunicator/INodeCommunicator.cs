using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
    public interface INodeCommunicator {

        Task<long> GetPeerChainHeight();

        Task<FetchResult> FetchNextBlocks(BlockLocators blockLocators, int batchSize, bool skipTransactions);

    }
}
