// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.DataObjects
{

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
