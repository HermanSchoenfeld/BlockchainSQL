using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using Sphere10.Framework;

namespace BlockchainSQL.Web.Code
{
    public static class JSONMappers {

        public static object MapBlock(Block block) {
            return new {
                ID= block.ID.ToString(),
                Height = block.Height.ToString(),
                Branch = MapBranch( block.Branch),
                Size= block.Size.ToString(),
                Nonce = block.Nonce.ToString(),
                PreviousBlockHash = BitcoinProtocolHelper.BytesToString(block.PreviousBlockHash),
                Hash = BitcoinProtocolHelper.BytesToString(block.Hash),
                Difficulty = block.Difficulty.ToString(),
                TimeStampUnix = block.TimeStampUnix.ToString(),
                TimeStampUtc = $"{block.TimeStampUtc:yyyy-MM-dd HH:mm:ss}",
                MerkleRoot = BitcoinProtocolHelper.BytesToString(block.MerkleRoot),
                Bits = block.Bits.ToString(),
                Version = block.Version.ToString(),
                TransactionCount = block.TransactionCount.ToString(),
                OutputsBTC = $"{block.OutputsBTC:0.#############################}",
                RewardBTC = $"{block.RewardBTC:0.#############################}",
                FeesBTC = $"{block.FeesBTC:0.#############################}"
            };
        }


        public static object MapTransaction(Transaction transaction) {
            return new {
                ID = transaction.ID.ToString(),
                TXID = BitcoinProtocolHelper.BytesToString(transaction.TXID),
                Size = transaction.Size.ToString(),
                Version = transaction.Version.ToString(),
                LockTime = transaction.LockTime.ToString(),
                BlockID = transaction.Block.ID.ToString(),
                BlockHash = transaction.Block?.Hash != null ? BitcoinProtocolHelper.BytesToString(transaction.Block.Hash) : string.Empty,
                Index = transaction.Index.ToString(),
                InputCount = transaction.InputCount.ToString(),
                OutputCount = transaction.OutputCount.ToString(),
                InputsBTC = $"{transaction.InputsBTC:0.#############################}",
                OutputsBTC = $"{transaction.OutputsBTC:0.#############################}",
                FeeBTC = $"{transaction.FeeBTC:0.#############################}",
            };
        }

        public static object MapTransactionInput(TransactionInput transactionInput) {
            var coinbaseOutpoint = ByteArrayEqualityComparer.Instance.Equals(transactionInput.Outpoint.TXID, BitcoinProtocolHelper.EmptyHash);
            var outpointText =
                !coinbaseOutpoint ?
                    string.Format("<a href='/Txn/{0}'><b>IX:</b><small>{1}</small> <b>TX:</b><small>{0}</small></a>", BitcoinProtocolHelper.BytesToString(transactionInput.Outpoint.TXID), transactionInput.Outpoint.OutputIndex) :
                    "<small><i>This payment is financed by the coinbase reward and the blocks transaction fees</i></small>";

            var fromAddressType = !coinbaseOutpoint ? transactionInput.TransactionOutput?.ToAddressType : AddressType.None;

            return new {
                ID                      = transactionInput.ID.ToString(),
                Index                   = transactionInput.Index.ToString(),
                FromAddressType         = fromAddressType,
                FromAddress             = transactionInput.TransactionOutput?.ToAddress,
                FromAddressDisplay      = !coinbaseOutpoint ? transactionInput.TransactionOutput?.ToAddress : "coinbase",
                FromAddressTypeDisplay  = ToFriendlyAddressType(fromAddressType),
                Outpoint                = MapOutpoint(transactionInput.Outpoint),
                OutpointDisplay         = outpointText,
                Value                   = SatoshiToBTC(transactionInput.Value),
                Sequence                = transactionInput.Sequence.ToString()
            };
        }

        public static object MapTransactionOutput(TransactionOutput transactionOutput) {
            return new {
                ID = transactionOutput.ID.ToString(),
                TransactionID = transactionOutput.Transaction.ID,
                Index = transactionOutput.Index.ToString(),
                ToAddressType = transactionOutput.ToAddressType.ToString(),
                ToAddressTypeDisplay = ToFriendlyAddressType(transactionOutput.ToAddressType),
                ToAddress = transactionOutput.ToAddress,
                ToAddressDisplay = transactionOutput.ToAddress,
                Value = $"{SatoshiToBTC(transactionOutput.Value):0.#############################}"
            };
        }

        public static object MapOutpoint(Outpoint outpoint) {
            return new {
                TXID = BitcoinProtocolHelper.BytesToString(outpoint.TXID),
                Index = outpoint.OutputIndex.ToString()
            };
        }

        public static object MapBranch(Branch branch) {
            return new {
                ID = branch.ID.ToString()
            };
        }
            

        public static string ToFriendlyAddressType(AddressType? addressType) {
            if (addressType == null)
                return string.Empty;
            return addressType.Value.ToString();
        }

        public static decimal? SatoshiToBTC(long? satoshi) {
            if (satoshi == null)
                return null;
            return (decimal) satoshi.Value/100000000;
        }
    }
}