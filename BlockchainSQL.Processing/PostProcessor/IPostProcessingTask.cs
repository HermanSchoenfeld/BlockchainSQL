namespace BlockchainSQL.Processing
{
    public interface IPostProcessingTask : IBizComponent {

        void ExecuteAll();
        void ExecutePartial(PersistResult newPersistSet);

        int Priority { get; set; }
    }
}
