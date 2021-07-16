using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
	public interface ITransactionFeeCalculator : IBizComponent {

        Task Evaluate(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);
    }
}
