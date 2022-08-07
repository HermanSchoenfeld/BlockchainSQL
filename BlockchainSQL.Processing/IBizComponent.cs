using BlockchainSQL.DataAccess;
using Hydrogen;

namespace BlockchainSQL.Processing {
	public interface IBizComponent {
        ILogger Log { get; }
        ApplicationDAC CustomDAC { get; set; }
        ApplicationDAC CreateDAC();
    }
}
