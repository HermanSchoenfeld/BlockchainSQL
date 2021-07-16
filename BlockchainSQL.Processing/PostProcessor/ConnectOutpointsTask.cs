namespace BlockchainSQL.Processing {

	public class ConnectOutpointsTask : BizComponent, IPostProcessingTask {
        public void ExecuteAll() {
            DAC.ConnectOutpoints();
        }

        public void ExecutePartial(PersistResult newPersistSet) {
            DAC.ConnectOutpoints(newPersistSet.TransactionInput.From, newPersistSet.TransactionInput.To);
        }

        public int Priority { get; set; }
    }
}
