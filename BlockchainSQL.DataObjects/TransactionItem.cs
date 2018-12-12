using System;
using System.Collections.Generic;

namespace BlockchainSQL.DataObjects {

    public abstract class TransactionItem {

        public virtual Transaction Transaction { get; set; }

        public virtual uint Index { get; set; }

        public virtual Script Script { get; set; }

    }
}
