using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
    public interface ITransactionFeeCalculator : IBizComponent {

        Task Evaluate(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);
    }
}
