namespace BlockchainSQL.Processing
{
    public class LeftOverBlocksException : BlockchainSQLException {
        public LeftOverBlocksException(int numBlocksOutstanding ) 
            : base("Unable to organize a total of {0} blocks, resulting data may be corrupt", numBlocksOutstanding) {            
        }
    }
}
