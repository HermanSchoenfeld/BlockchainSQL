namespace BlockchainSQL.Processing {
	public interface IPostProcessor : IBizComponent {

        void PostProcessAll();
        void PostProcessPartial(PersistResult newPersistSet);
        IPostProcessingTask[] Tasks { get; }
        
    }
}
