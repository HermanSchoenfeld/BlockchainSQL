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
