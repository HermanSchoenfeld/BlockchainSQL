namespace BlockchainSQL.DataObjects
{

    public abstract class TransactionItem {

        public virtual Transaction Transaction { get; set; }

        public virtual uint Index { get; set; }

        public virtual long? ScriptId => Script.ID;

        public virtual Script Script { get; set; }

    }
}
