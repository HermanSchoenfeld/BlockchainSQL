namespace BlockchainSQL.Processing
{


    public class TotalizeTransactionsTask : BizComponent, IPostProcessingTask {
        public void ExecuteAll() {
            DAC.TotalizeTransactions();
        }

        public void ExecutePartial(PersistResult newPersistSet) {
            DAC.TotalizeTransactions(newPersistSet.Transaction.From, newPersistSet.Transaction.To);
        }

        public int Priority { get; set; }
    }
}
