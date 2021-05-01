namespace BlockchainSQL.Processing
{
    public class BlockAlreadySequencedException : BlockchainSQLException {
        public BlockAlreadySequencedException(byte[] blockHash) 
            : base("Block '{0}' has already been sequenced", BitcoinProtocolHelper.BytesToString(blockHash)) {            
        }
    }
}
