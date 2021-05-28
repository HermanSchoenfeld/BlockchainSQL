using System.Collections.Generic;
using System.Linq;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing
{
    public class BlockLocator : BizComponent, IBlockLocator {
        public virtual BlockLocators GetBlockLocators() {
            var indices = DetermineBlockLocatorIndices(DAC.GetMaxBlockHeight());
            if (!indices.Any())
                return BlockLocators.Empty;
            return new BlockLocators {
                 Locations = base.DAC.GetBlockLocators(indices).ToArray()
            };
        }
		// 1000    0
		//  999    1
		//  998    2
		/// <summary>
		/// 
		/// </summary>
		/// <param name="topDepth"></param>
		/// <returns></returns>

        protected virtual long[] DetermineBlockLocatorIndices(long topDepth) {
            // Start at max_depth
            var indices = new List<long>();
            if (topDepth >= 0) {
                // Push last 10 indices first
                long step = 1, start = 0;
                for (var i = topDepth; i > 0; i -= step, ++start) {
                    if (start >= 10)
                        step *= 2;
                    indices.Add(i);
                }
                indices.Add(0);
            }
            return indices.ToArray();
        }
    }
}
