// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.ComponentModel;

namespace BlockchainSQL.DataObjects
{
    public enum ScriptType : byte {
        [Description("Coinbase script used in Transaction Inputs that pays newly minted coins")]
        Coinbase   = 1,

        [Description("Lock script (or Signature script) used in Transaction Outputs")]
        Lock       = 2,

        [Description("Unlock script (or Pubkey script) used in Transaction Inputs")]
        Unlock     = 3
    }

}
