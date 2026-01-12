// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
