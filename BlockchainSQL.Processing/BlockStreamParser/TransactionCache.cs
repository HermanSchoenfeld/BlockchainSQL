using System.Data;
using System.Linq;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using Hydrogen;
using Hydrogen.Data;

namespace BlockchainSQL.Processing {
	public sealed class TransactionCache : CacheBase<byte[], Transaction> {
        private readonly ApplicationDAC _dac;
        private readonly DACScope _scope;
        public TransactionCache(uint capacity)
            : base(CacheReapPolicy.ASAP, ExpirationPolicy.SinceLastAccessedTime, capacity, keyComparer: ByteArrayEqualityComparer.Instance) {
            _dac = BizLogicScope.Current.CreateDAC();
            _dac.UseScopeOsmosis = false;
            _dac.DefaultIsolationLevel = IsolationLevel.ReadUncommitted;
            _scope = _dac.BeginScope();            
        }

        public override void Dispose() {            
            _scope.Dispose();
        }

        public override CachedItem Get(object key) {
            var size = this.CurrentSize;
            return base.Get(key);
        }

        protected override sealed long EstimateSize(Transaction value) {
            return (uint) SizeEstimator.Estimate(value);
        }

        protected override sealed Transaction Fetch(byte[] key) {
            using (SystemLog.Logger.LogDuration("TransactionCache::Fetch")) {
                // Search the pipeline first (since sought after txn might not be persisted yet)
                var pipelineScope = WipPipelineScope.Current;
                if (pipelineScope != null) {
                    using (pipelineScope.PipelineTransactions.EnterReadScope()) {
                        if (pipelineScope.PipelineTransactions.ContainsKey(key)) {
                            return pipelineScope.PipelineTransactions[key];
                        }
                    }
                }
                _scope.BeginTransaction(IsolationLevel.ReadUncommitted);
                var txn = _dac.GetTransactionByTXID(key);
                txn.Outputs = _dac.GetTransactionOutputs(txn.ID).ToArray();
                _scope.Commit();
                return txn;
            }
        }

        protected override bool CheckStaleness(byte[] key, CachedItem<Transaction> item) => false;
	}
}
