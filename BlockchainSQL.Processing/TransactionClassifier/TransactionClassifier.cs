using System;
using System.Linq;
using BlockchainSQL.DataObjects;
using NBitcoin;
using NBitcoin.DataEncoders;
using Sphere10.Framework;
using ScriptType = BlockchainSQL.DataObjects.ScriptType;

namespace BlockchainSQL.Processing
{

    public class TransactionClassifier : ITransactionClassifier {

        public ScriptClass ClassifyInputScript(ScriptInstruction[] instructions, ScriptType scriptType, out AddressType addressType) {
            addressType = AddressType.Unknown;

            if (scriptType == ScriptType.Coinbase) {
                addressType = AddressType.None;
                return ScriptClass.CoinBase;
            }

            // NullData
            if (MatchNullData_InputScript(instructions)) {                
                addressType = AddressType.None;
                return ScriptClass.NullData;
            }

            // P2PK (or Coinbase)
            if (MatchesP2PK_InputScript(instructions)) {
                addressType = AddressType.PublicKey;
                return ScriptClass.P2PK;
            }

            // P2PKH (or Coinbase)
            if (MatchesP2PKH_InputScript(instructions)) {                
                addressType = AddressType.PublicKeyHash;
                return ScriptClass.P2PKH;
            }

            // P2S
            if (MatchesP2S_Inputcript(instructions)) {                
                throw new NotSupportedException();
            }

            // P2SH
            if (MatchesP2SH_Inputcript(instructions)) {                
                addressType = AddressType.ScriptHash;
                return ScriptClass.P2SH;
            }

            // Multisig
            if (MatchesMultiSig_InputScript(instructions)) {                
                addressType = AddressType.MultiplePublicKeyHashes;
                return ScriptClass.Multisig;
            }

            return ScriptClass.NonStandard;
        }

        public ScriptClass ClassifyOutputScript(ScriptInstruction[] instructions, out AddressType addressType, out string address) {
            addressType = AddressType.Unknown;
            address = null;

            // Null Data
            if (MatchNullData_OutputScript(instructions)) {                
                addressType = AddressType.None;
                address = null;
                return ScriptClass.NullData;
            }

            // P2PK
            if (MatchesP2PK_OutputScript(instructions)) {                
                addressType = AddressType.PublicKey;
                address = BitcoinProtocolHelper.ConvertToFriendlyString(instructions[0].DataLE);
                return ScriptClass.P2PK;
            }

            // P2PKH
            if (MatchesP2PKH_OutputScript(instructions)) {
                addressType = AddressType.PublicKeyHash;
                address = Base58Helper.Base58CheckEncode(instructions[2].DataLE, AddressType.PublicKeyHash);
                return ScriptClass.P2PKH;
            }

            // P2S
            if (MatchesP2S_OutputScript(instructions)) {
                throw new NotSupportedException();
            }
           
            // P2SH
            if (MatchesP2SH_OutputScript(instructions)) {
                addressType = AddressType.ScriptHash;
                address = Base58Helper.Base58CheckEncode(instructions[1].DataLE, AddressType.ScriptHash);
                return ScriptClass.P2SH;
            }

            if (MatchesP2WPKH_OutputScript(instructions)) {
	            addressType = AddressType.WitnessPublicKeyHash;
	            address = Base58Helper.Base58Encode(Tools.Array.ConcatArrays(instructions[0].DataLE, instructions[1].DataLE));
	            return ScriptClass.P2WPKH;
            }

            if (MatchesP2WSH_OutputScript(instructions)) {
	            addressType = AddressType.WitnessScriptHash;
	            address = Base58Helper.Base58Encode(Tools.Array.ConcatArrays(instructions[0].DataLE, instructions[1].DataLE));
	            return ScriptClass.P2WSH;
            }

            // Multisig
            if (MatchesMultiSig_OutputScript(instructions)) {
                addressType = AddressType.MultiplePublicKeyHashes;
                address = null;
                return ScriptClass.Multisig;
            }

            return ScriptClass.NonStandard;

        }

        public static bool IsPushByteOpCode(OpCode opcode) {
            return OpCode.OP_PUSHBYTES01 <= opcode && opcode <= OpCode.OP_PUSHBYTES75;
        }

        public static bool IsPushDataOpCode(OpCode opcode) {
            return OpCode.OP_PUSHDATA1 <= opcode && opcode <= OpCode.OP_PUSHDATA4;
        }


        protected virtual bool MatchNullData_InputScript(ScriptInstruction[] instructions) {
            return instructions.Length == 0;
        }

        protected virtual bool MatchNullData_OutputScript(ScriptInstruction[] instructions) {
            return 
                instructions.Length == 1 && 
                instructions[0].OpCode == OpCode.OP_RETURN;
        }

        protected virtual bool MatchesP2PK_InputScript(ScriptInstruction[] instructions) {
            return 
                instructions.Length == 1 && 
                IsPushByteOpCode(instructions[0].OpCode);
        }

        protected virtual bool MatchesP2PK_OutputScript(ScriptInstruction[] instructions) {
            return
                instructions.Length == 2 &&
                IsPushByteOpCode(instructions[0].OpCode) &&
                instructions[1].OpCode == OpCode.OP_CHECKSIG;
        }

        protected virtual bool MatchesP2PKH_InputScript(ScriptInstruction[] instructions) {
            return 
                instructions.Length == 2 && 
                instructions
                    .Select(i => i.OpCode)
                    .All(IsPushByteOpCode);
        }

        protected virtual bool MatchesP2PKH_OutputScript(ScriptInstruction[] instructions) {
            return
                instructions.Length == 5 &&
                instructions[0].OpCode == OpCode.OP_DUP &&
                instructions[1].OpCode == OpCode.OP_HASH160 &&
                IsPushByteOpCode(instructions[2].OpCode) &&
                instructions[3].OpCode == OpCode.OP_EQUALVERIFY &&
                instructions[4].OpCode == OpCode.OP_CHECKSIG;
        }

        protected virtual bool MatchesP2S_Inputcript(ScriptInstruction[] instructions) {
            return false;
        }

        protected virtual bool MatchesP2S_OutputScript(ScriptInstruction[] instructions) {
            return false;
        }

        protected virtual bool MatchesP2SH_Inputcript(ScriptInstruction[] instructions) {
            return 
                instructions.Length >= 2 && 
                IsPushDataOpCode(instructions.Last().OpCode) && 
                instructions
                    .Select(i => i.OpCode)
                    .Reverse()
                    .Skip(1)
                    .All(IsPushByteOpCode);
        }

        protected virtual bool MatchesP2SH_OutputScript(ScriptInstruction[] instructions) {
            return
                instructions.Length == 3 &&
                instructions[0].OpCode == OpCode.OP_HASH160 &&
                IsPushByteOpCode(instructions[1].OpCode) &&
                instructions[2].OpCode == OpCode.OP_EQUAL;

        }

        protected virtual bool MatchesMultiSig_InputScript(ScriptInstruction[] instructions) {
            return 
                instructions.Length >= 2 && 
                instructions.First().OpCode == OpCode.OP_0 && 
                instructions
                    .Select(i => i.OpCode)
                    .Skip(1)
                    .All(IsPushByteOpCode);
        }

        protected virtual bool MatchesMultiSig_OutputScript(ScriptInstruction[] instructions) {
            return 
                instructions.Length >= 3 &&
                instructions.Last().OpCode == OpCode.OP_CHECKMULTISIG;
        }

        protected virtual bool MatchesP2WPKH_OutputScript(ScriptInstruction[] instructions) {
	        return instructions.Length == 2 && instructions[0].OpCode == OpCode.OP_0 &&
	               instructions[1].DataLE?.Length == 20;
        }

        protected virtual bool MatchesP2WSH_OutputScript(ScriptInstruction[] instructions) {
	        return instructions.Length == 2 && instructions[0].OpCode == OpCode.OP_0 &&
	               instructions[1].DataLE?.Length == 32;
        }
    }
}
