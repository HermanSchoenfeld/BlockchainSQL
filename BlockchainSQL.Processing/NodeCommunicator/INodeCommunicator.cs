using System.Threading.Tasks;

namespace BlockchainSQL.Processing
{
    public interface INodeCommunicator {

        Task<long> GetPeerChainHeight();

        Task<FetchResult> FetchNextBlocks(BlockLocators blockLocators, int batchSize, bool skipTransactions);

    }
}
