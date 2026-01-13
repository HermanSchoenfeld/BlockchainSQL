// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
