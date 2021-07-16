using System;

namespace BlockchainSQL.Processing {
	public class NoOpBlockLocator : IBlockLocator {
        public virtual BlockLocators GetBlockLocators() {
            throw new NotSupportedException();
        }

    }
}
