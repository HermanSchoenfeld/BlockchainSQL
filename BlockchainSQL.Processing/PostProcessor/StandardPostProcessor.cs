using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing
{

    public class PostProcessor : BizComponent, IPostProcessor {

        public PostProcessor() {
            Tasks = new IPostProcessingTask[] {
                new ConnectOutpointsTask() { Priority = 1 }, 
                new TotalizeTransactionsTask() { Priority = 2 }, 
                new TotalizeBlocksTask() { Priority = 3 }
            };
        }
        public void PostProcessAll() {
            using (var scope = DAC.BeginScope()) {
                scope.BeginTransaction();
                Tasks.ForEach(t => t.ExecuteAll());
                scope.Commit();
            }
        }

        public void PostProcessPartial(PersistResult newPersistSet) {
            using (var scope = DAC.BeginScope()) {
                scope.BeginTransaction();
                Tasks.ForEach(t => t.ExecutePartial(newPersistSet));
                scope.Commit();
            }
        }

        public IPostProcessingTask[] Tasks { get; private set; }
    }
}
