namespace BlockchainSQL.Processing.BusinessObjects
{

    public class BlockIndex {
        public byte[] Hash;
        public int Version;
        public uint Height;
        public BlockStatus Status;
        public uint TransactionCount;
        public DiskPos DataLocation;
        public DiskPos UndoLocation;
    }
}
