namespace BlockchainSQL.Processing
{

    public class TotalizeBlocksTask : BizComponent, IPostProcessingTask {
        public void ExecuteAll() {
            DAC.TotalizeBlocks();
        }

        public void ExecutePartial(PersistResult newPersistSet) {
            DAC.TotalizeBlocks(newPersistSet.Block.From, newPersistSet.Block.To);
        }

        public int Priority { get; set; }
    }
}
