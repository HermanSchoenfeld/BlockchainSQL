using System.Linq;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing
{
    public static class SizeEstimator {

        public static long Estimate(Block block) {
            if (block == null)
                return 0;
            var size = 64L + 64L + 64L;
            if (block.PreviousBlockHash != null)
                size += block.PreviousBlockHash.Length * 2;
            if (block.MerkleRoot != null)
                size += block.MerkleRoot.Length;
            if (block.Hash != null)
                size += block.Hash.Length * 2;
            if (block.Transactions != null)
                size += block.Transactions.Sum(t => Estimate(t));
            return size;
        }

        public static long Estimate(Transaction transaction) {
            if (transaction == null)
                return 0;
            var size = 13 * 64L;
            if (transaction.TXID != null)
                size += transaction.TXID.Length * 2;
            if (transaction.Inputs != null)
                size += transaction.Inputs.Sum(t => Estimate(t));
            if (transaction.Outputs != null)
                size += transaction.Outputs.Sum(t => Estimate(t));
            return size;
        }

        public static long Estimate(TransactionInput transactionInput) {
            if (transactionInput == null)
                return 0;
            var size = 6 * 64L;
            if (transactionInput.Outpoint.TXID != null)
                size += transactionInput.Outpoint.TXID.Length * 2;
            size += Estimate(transactionInput.Script);
            return size;
        }

        public static long Estimate(TransactionOutput transactionOutput) {
            if (transactionOutput == null)
                return 0;
            var size = 5 * 64L;
            size += Estimate(transactionOutput.Script);
            return size;
        }

        public static long Estimate(Script script) {
            if (script == null)
                return 0;

            var size = 7 * 64L;
            if (script.Instructions != null)
                size += script.Instructions.Sum(i => Estimate(i));
            return size;
        }

        public static long Estimate(ScriptInstruction scriptInstruction) {
            if (scriptInstruction == null)
                return 0;
            var size = 6 * 64L;
            if (scriptInstruction.DataLE != null)
                size += scriptInstruction.DataLE.Length * 2;
            return size;
        }
    }
}
