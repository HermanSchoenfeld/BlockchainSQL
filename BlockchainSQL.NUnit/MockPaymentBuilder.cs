using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using Sphere10.Framework;

namespace BlockchainSQL.NUnit {
    public class MockPaymentBuilder {
        private readonly List<Transaction> _transactionHistory;
        private static readonly Outpoint CoinbaseUTXO;

        static MockPaymentBuilder() {
            CoinbaseUTXO = new Outpoint {
                OutputIndex = 4294967295,
                TXID = BitcoinProtocolHelper.EmptyHash
            };
        }

        public MockPaymentBuilder() {
            _transactionHistory = new List<Transaction>();
        }

        public IEnumerable<Transaction> History => _transactionHistory.AsReadOnly();

        public Transaction Pay(string from, string to, long satoshi) {

            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException(nameof(to));
            var isCoinbase = string.IsNullOrWhiteSpace(from);

            // find outpoint
            var outpoint = isCoinbase ? CoinbaseUTXO : FindUTXO(from, satoshi);

            var payment = new Transaction {
                TXID = Guid.NewGuid().ToByteArray().Concat(Guid.NewGuid().ToByteArray()).ToArray(), //random txid
            };
            payment.AddInput(
                new TransactionInput {
                    Outpoint = outpoint,
                    Script = isCoinbase ? MockSigScript_Coinbase() : MockSigScript_P2PKH(),
                    Value = satoshi
                }
            );
            payment.AddOutput(
                new TransactionOutput {
                    ToAddress = to,
                    Script = MockPubKeyScript_P2PKH(),
                    Value = satoshi
                }
            );
            _transactionHistory.Add(payment);
            return payment;
        }

        private Outpoint FindUTXO(string from, long satoshi) {
            var allInputs = _transactionHistory.SelectMany(tx => tx.Inputs).ToArray();
            var matchingOutputs = _transactionHistory.SelectMany(tx => tx.Outputs).Where(txo => txo.ToAddress == from && txo.Value == satoshi);
            var unspentOutputs = matchingOutputs.Where(txo => !allInputs.Any(txi => txi.Outpoint.TXID.SequenceEquals(txo.Transaction.TXID))).ToArray();
            if (unspentOutputs.Length == 0)
                throw new SoftwareException("No UTXO found for '{0}' with quantity '{1}", from, satoshi);

            var output = unspentOutputs.First();
            return new Outpoint {
                OutputIndex = output.Index,
                TXID = output.Transaction.TXID
            };
        }

        private Script MockSigScript_Coinbase() {
            return GenScript();
        }

        private Script MockSigScript_P2PKH() {
            return GenScript();
        }


        private Script MockPubKeyScript_P2PKH() {
            return GenScript();
        }


        private Script GenScript() {
            var script = new Script {
                ScriptType = ScriptType.Coinbase,
                ScriptClass = ScriptClass.CoinBase,
                RowState = 1
            };
            script.AddInstructions(Enumerable.Range(1, Tools.Maths.RandomNumberGenerator.Next(1, 10)).Select(i => GenScriptInstruction()));
            script.InstructionCount = script.Instructions.Count;
            return script;
        }

        private ScriptInstruction GenScriptInstruction() {
            return new ScriptInstruction {
                OpCode = OpCode.OP_0,
                Valid = true,
                RowState = 1,
            };
        }
    }
}





//private TransactionInput GenTXIN() {
//    return new TransactionInput {
//        Index = 1,
//        Outpoint = new Outpoint {
//            TXID = TestHelper.ToHashBytes("0"),
//            OutputIndex = 0,
//        },
//        Sequence = 0,
//        RowState = 1,
//        Script = GenScript()
//    };
//}

//private TransactionOutput GenTXOUT() {
//    return new TransactionOutput {
//        Index = 1,
//        ToAddressType = AddressType.Unknown,
//        Value = 0,
//        RowState = 1,
//        Script = GenScript()
//    };
//}

//private Script GenScript() {
//    var script = new Script {
//        ScriptType = ScriptType.Coinbase,
//        ScriptClass = ScriptClass.CoinBase,
//        RowState = 1
//    };
//    script.AddInstructions(Enumerable.Range(1, Tools.Maths.RandomNumberGenerator.Next(1, 10)).Select(i => GenScriptInstruction()));
//    script.InstructionCount = script.Instructions.Count;
//    return script;
//}

//private ScriptInstruction GenScriptInstruction() {
//    return new ScriptInstruction {
//        OpCode = OpCode.OP_0,
//        Valid = true,
//        RowState = 1,
//    };
//}
