using System;

namespace BlockchainSQL.DataObjects
{
    public class StatementLine {
        public virtual DateTime TXDate { get; set; }

        public virtual string TXType { get; set; }

        public virtual byte[] TXID { get; set; }

        public virtual uint TXID_IX { get; set; }

        public virtual decimal TXAmount { get; set; }

        public virtual uint BlockHeight { get; set; }

    }


}
