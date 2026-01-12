// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.ComponentModel;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataObjects
{
    public enum ScriptClass : byte {
        [Description("Null Data scripts are used to store data on the blockchain and cannot be spent")]
        NullData = 1,

        [Description("Coinbase scripts are used to reward the miner newly minted coins")]
        CoinBase = 2,

        [Description("Pay to Public Key (P2PK) payments can be redeemed by the bearer of the public key (public key is known at payment time)")]
        P2PK = 3,

        [Description("Pay to Public Key Hash (P2PKH) payments can be redeemed by the bearer of the public key (public key known at redeem time)")]
        P2PKH = 4,

        [Description("Pay to Script payments (P2S) can be redeemed by the bearer of a script that solves the signature script (redeem script known at payment time)")]
        P2S = 5,

        [Description("Pay to Script Hash (P2SH) can be redeemed by the bearer of a script that solves the signature script (redeem script known at redeem time)")]
        P2SH = 6,
        
        [Description("Multisignature payments (Multisig) can be redeemed by the bearer of M out of N public keys")]
        Multisig = 7,

        [Description("Nonstandard scripts are not currently supported within Bitocoin")]
        NonStandard = 8,
        
        [Description("Pay to Witness Public Key Hash (P2WPKH) payments can be redeemed by the bearer of the public key (public key known at redeem time). V0 SegWit.")]
        P2WPKH = 9,
        
        [Description("Pay to Witness Script Hash (P2WSH) payments can be redeemed by the bearer of a script that solves the witness script (redeem script known at redeem time). V0 SegWit.")]
        P2WSH = 10,
        
        [Description("V1 SegWit output. Taproot script may be public key, aggregate public key or script hash.")]
        V1Segwit
    }
}
