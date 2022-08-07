using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen;
using Hydrogen.Data;

namespace BlockchainSQL.Processing {
	public class TransactionFeeCalculator : BizComponent, ITransactionFeeCalculator {
        private readonly TransactionCache _transactionCache;

        public TransactionFeeCalculator() : this(new TransactionCache(100000)) {
        }

        public TransactionFeeCalculator(TransactionCache transactionCache) {
            _transactionCache = transactionCache;
        }

        public virtual async Task Evaluate(IEnumerable<Transaction> transactions, CancellationToken cancellationToken) {
            var transArr = transactions as Transaction[] ?? transactions.ToArray();
            _transactionCache.BulkLoad(transArr.Select(t => new KeyValuePair<byte[], Transaction>(t.TXID, t)));
            await Task.Run(() => Parallel.ForEach(transArr.Where(t => t.Index > 0), EvaluateTransaction), cancellationToken);
        }

        public virtual void EvaluateTransaction(Transaction transaction) {
            throw new NotImplementedException();
            // Calculate fee ( fee = sum of outpoints - sum of outputs)

            transaction.FeeBTC = -1;//(decimal) (CalculateInputSum(transaction) - CalculateOutputSum(transaction)) / 100000000D;
        }

        private BigInteger CalculateOutputSum(Transaction transaction) {
            return transaction.Outputs.Select(o => (BigInteger) o.Value).Aggregate(BigInteger.Add);

        }

        private BigInteger CalculateInputSum(Transaction transaction) {
            return transaction
                .Inputs
                .Select(i => {
                    try {
                        var outpoint = i.Outpoint;

                        var outpointTx = _transactionCache[outpoint.TXID];
                        if (outpoint.OutputIndex < outpointTx.OutputCount) {
                            return (BigInteger) outpointTx.Outputs[(int) outpoint.OutputIndex].Value;
                        }
                        throw new SoftwareException("Transaction input ({0}, {1}) had invalid outpoint ({2}, {3})", transaction.TXID, i.Index, outpoint.TXID, outpoint.OutputIndex);
                    } catch (Exception error) {
                        var xx = error.ToDiagnosticString();
                        var y = xx;
                        throw;

                    }
                })
                .Aggregate(BigInteger.Add);
        }
    }
}
