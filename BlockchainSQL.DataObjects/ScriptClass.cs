using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataObjects {
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
        NonStandard = 8
    }

}
