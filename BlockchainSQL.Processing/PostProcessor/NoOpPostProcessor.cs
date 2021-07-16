namespace BlockchainSQL.Processing {
	public class NoOpPostProcessor : BizComponent, IPostProcessor {

        public NoOpPostProcessor() {
            Tasks = new IPostProcessingTask[0];
        }

        public void PostProcessAll() {
        }

        public void PostProcessPartial(PersistResult newPersistSet) {            
        }

        public IPostProcessingTask[] Tasks { get; private set; }
    }
}
