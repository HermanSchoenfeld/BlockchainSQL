using System;
using System.Collections;
using System.Collections.Generic;

namespace BlockchainSQL.DataObjects {

    public partial class TransactionOutput : TransactionItem {

        public TransactionOutput() {
            RowState = 1;
        }

        public virtual long ID { get; set; }

        public virtual long Value { get; set; }

        public virtual AddressType ToAddressType { get; set; }

        public virtual string ToAddress { get; set; }

        public virtual byte RowState { get; set; }

    }
}
