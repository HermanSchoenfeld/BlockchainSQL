using System;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public interface IBlockStreamParser : IBizComponent {
        Task Parse(CancellationToken cancellationToken, Action<int> progressCallback = null, bool deferPostProcessing = false, TimeSpan? pollSleepDuration = null);
    }
}
