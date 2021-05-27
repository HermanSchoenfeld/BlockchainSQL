namespace BlockchainSQL.DataObjects
{

    public class TransactionInput : TransactionItem {

        public TransactionInput() {
            RowState = 1;
        }

        public virtual long ID { get; set; }

        public virtual Outpoint Outpoint { get; set; }

        public virtual TransactionOutput TransactionOutput { get; set; }

        public virtual long? Value { get; set; }

        public virtual uint Sequence { get; set; }

        public virtual byte RowState { get; set; }
        
        public virtual byte[][] WitnessStackBytes { get; set; }

        public virtual Script WitScript { get; set; }
    }
}
