using BlockchainSQL.DataAccess;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
	public interface IBizComponent {
        ILogger Log { get; }
        ApplicationDAC CustomDAC { get; set; }
        ApplicationDAC CreateDAC();
    }
}
