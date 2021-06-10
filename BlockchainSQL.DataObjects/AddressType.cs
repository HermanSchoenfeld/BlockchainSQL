using System.ComponentModel;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataObjects
{
    public enum AddressType : byte {

        [Description("Public key address")]
        PublicKey = 1,

        [Description("Standard Bitcoin address (l)")] 
        PublicKeyHash = 2,

        [Description("Miltiple standard Bitcoin addresses (each prefixed 'M:' and delimitted with ,")]
        MultiplePublicKeyHashes = 3,

        [Description("Address denotes a script")] 
        Script = 4,

        [Description("Address denotes the hash of the redeem script used in P2SH payments (3)")]
        ScriptHash = 5,

        [Description("Standard Bitcoin Testnet address (m or n)")] 
        BitcoinTestnet = 6,

        [Description("Private key (WIF) address (5, K or L)")] 
        PrivateKeyWIF = 7,

        [Description("BIP38 Encrypted Private Key (6P) ")] 
        EncryptedPrivateKey = 8,

        [Description("BIP32 Extended Public Key (xpub)")] 
        ExtendedPublicKey = 9,

        [Description("No address (null or data script)")]
        None = 10,

        [Description("Unknown (or indeterminable) type of address")]
        Unknown = 11,
        
        [Description("Address denotes the hash of the redeem script (witness program) used in P2WSH payments")]
        WitnessScriptHash = 12,
        
        [Description("Address denotes the hash of the ")]
        WitnessPublicKeyHash = 13,
        
        [Description("Segwit V1 program, address is public key unhashed or the hash of a script.")]
        V1Segwit = 14
    }
}
