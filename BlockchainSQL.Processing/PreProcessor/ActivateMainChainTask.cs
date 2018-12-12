using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public class ActivateMainChainTask : BizComponent, IPreProcessingTask {
        private readonly List<WipBlock> _leftOverBlocks;
        public ActivateMainChainTask(IBlockOrganizer blockOrganizer) {
            Organizer = blockOrganizer;
            _leftOverBlocks = new List<WipBlock>();
        }

        public IBlockOrganizer Organizer { get; private set; }

        public IEnumerable<WipBlock> Process(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {
            var start = DateTime.Now;
            WipBlock[] danglingBlocks;
            var outputBlocks = Organizer.Organize(_leftOverBlocks.Concat(blocks), out danglingBlocks);
            _leftOverBlocks.Clear();
            _leftOverBlocks.AddRange(danglingBlocks);
            var organizeDuration = DateTime.Now.Subtract(start);
            outputBlocks.Update(o => o.OrganizeDuration = organizeDuration);
            return outputBlocks;
        }

        public ExecutionContext Context => ExecutionContext.Sequential;

        public int Priority => 0;
    }
}
