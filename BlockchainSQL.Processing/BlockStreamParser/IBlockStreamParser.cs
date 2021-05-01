using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing
{
    public interface IBlockStreamParser : IBizComponent {
        Task Parse(CancellationToken cancellationToken, Action<int> progressCallback = null, bool deferPostProcessing = false, TimeSpan? pollSleepDuration = null);
    }
}
