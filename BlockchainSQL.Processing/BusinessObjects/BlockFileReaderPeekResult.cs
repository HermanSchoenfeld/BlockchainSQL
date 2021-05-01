namespace BlockchainSQL.Processing
{
    public class BlockFileReaderPeekResult {
        public byte[] NextBlockPrevBlockHash { get; set; }
        public long NextBlockSize { get; set; }

        public long AdditionalOffset { get; set; }
    }

}
