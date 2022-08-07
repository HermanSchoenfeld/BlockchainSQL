using System;
using System.Linq;
using NBitcoin.DataEncoders;
using NHibernate.Engine.Query;
using Hydrogen;

namespace BlockchainSQL.Processing {

	/// <remarks>
	/// Current this parses the network protocol binary data into the domain model of BlockchainSQL.
	/// This should be refactored to parse into an object model that mirrors the actual protocol, which can then
	/// be converted into the domain model of BlockchainSQL.
	/// </remarks>
#warning Proper checks need to be made before reading.
	public class BitcoinProtocolHelper {
        public const string EmptyHashString = "0000000000000000000000000000000000000000000000000000000000000000";
        public const string GenesisHashString = "000000000019D6689C085AE165831E934FF763AE46A2A6C172B3F1B60A8CE26F";
        public static readonly byte[] EmptyHash = ConvertHashStringToDatabaseBytes(EmptyHashString);
        public static readonly byte[] GenesisHash = ConvertHashStringToDatabaseBytes(GenesisHashString);
        public const uint MagicID_Main = 3652501241;


        public static string ConvertToFriendlyString(byte[] rawInternalOrderBytes) {
            return BytesToString(rawInternalOrderBytes.Reverse().ToArray());
        }

        public static byte[] ConvertHashStringToDatabaseBytes(string hashString) {
            if (hashString == null)
                throw new ArgumentNullException(nameof(hashString));

            if (!IsValidHashString(hashString))
                throw new ArgumentException("Not a valid hash string", nameof(hashString));
            
            var bytes = StringToBytes(hashString);
            return bytes;

        }

        public static byte[] ConvertHashStringToInternalOrderBytes(string hashString) {
            var bytes = ConvertHashStringToDatabaseBytes(hashString);
            Array.Reverse(bytes);
            return bytes;
        }


        public static long CalculateReward(uint height) {
            const long maxRewardBTC = 50;
            const long maxRewardSatoshi = maxRewardBTC*100000000;
            const long halvingBlockDuration = 210000;

            return maxRewardSatoshi / (long)Math.Pow(2, height / halvingBlockDuration);
        }

        public static byte[] ConvertHashToInternalOrderBytes(byte[] hash) {
            return hash.Reverse().ToArray();
        }

        public static bool IsValidHashByteArray(byte[] hashBytes) {
            return hashBytes is { Length: 32 };
        }

        public static bool IsValidHashString(string blockHash) {
            return blockHash is { Length: 64 } && Tools.Text.IsValidHexString(blockHash);
        }

        public static bool IsValidAddress(string address) {
	        if (string.IsNullOrWhiteSpace(address))
		        return false;

	        if (address.Length == 130 && HexEncoding.IsValid(address))
		        return true; // P2PK

	        if (IsValidP2PKHAddress(address))
		        return true;

	        if (address.StartsWith("bc") && IsValidBech32Address(address))
		        return true;

	        return false;
        }

		public static bool IsValidP2PKHAddress(string address, bool assumeValidBase58 = false) {
	        // note: assumeValidAddress is intended to by-pass IsValidAddress call if already known to be true
	        return (assumeValidBase58 || IsValidBase58String(address)) && address.Length == 34;
        }


        public static bool IsValidBech32Address(string adr) {
	        try {
		        Encoders.Bech32("bc").Decode(adr, out _);
		        return true;
	        } catch (FormatException) {
		        return false;
	        }
		}

        public static bool IsValidBase58String(string str) {
	        var base58Chars = Base58Helper.Digits.ToCharArray();
	        return str.All(base58Chars.Contains);
        }


		public static string BytesToString(byte[] bytes) {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public static byte[] StringToBytes(string textFormat) {
            return textFormat.ToHexByteArray();
        }

    }
}
