using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
	public class FetchResult {
        public long NodeHeight { get; set; }
        public Block[] Blocks { get; set; }

    }
}
