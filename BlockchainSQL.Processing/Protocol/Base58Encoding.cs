using System;
using System.Collections.Generic;
//using System.Diagnostics.Contracts;
using System.Linq;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing.Domain;
using System.Numerics;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {

	public static class Base58Helper {

        public static string Base58CheckEncode(IEnumerable<byte> payload, AddressType addressType) {
            ValidateAddressType(addressType);
            var version = DetermineVersionPrefix(addressType);
            return Base58CheckEncode(payload, version);
        }

        public static string Base58CheckEncode(IEnumerable<byte> payload, IEnumerable<byte> version) {
            var bytes = new List<byte>();
            bytes.AddRange(version);
            bytes.AddRange(payload);
            var checksum = HashingFunctions.SHA256(HashingFunctions.SHA256(bytes)).Take(4);
            bytes.AddRange(checksum);
            return Base58Encode(bytes);
        }


        public const string Digits = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        public static string Base58Encode(IEnumerable<byte> data) {
            //Contract.Requires<ArgumentNullException>(data != null);
            //Contract.Ensures(Contract.Result<string>() != null);

            var dataArr = data as byte[] ?? data.ToArray();

            // Decode byte[] to BigInteger
            BigInteger intData = 0;            
            for (var i = 0; i < dataArr.Length; i++) {
                intData = intData * 256 + dataArr[i];
            }

            // Encode BigInteger to Base58 string
            var result = "";
            while (intData > 0) {
                var remainder = (int)(intData % 58);
                intData /= 58;
                result = Digits[remainder] + result;
            }
            
            // Append `1` for each leading 0 byte
            for (var i = 0; i < dataArr.Length && dataArr[i] == 0; i++) {
                result = '1' + result;
            }
            return result;
        }

        private static void ValidateAddressType(AddressType addressType) {
            if (!addressType.IsIn(AddressType.PublicKeyHash, AddressType.ScriptHash, AddressType.BitcoinTestnet, AddressType.PrivateKeyWIF, AddressType.EncryptedPrivateKey, AddressType.ExtendedPublicKey))
                throw new ArgumentOutOfRangeException("addressType", "{0} not supported address type for Base58Check".FormatWith(addressType));            
        }

        public static byte[] DetermineVersionPrefix(AddressType addressType) {
            switch (addressType) {
                case AddressType.PublicKeyHash:
                    return new byte[] { 0x00 };
                case AddressType.ScriptHash:
                    return new byte[] { 0x05 };
                case AddressType.BitcoinTestnet:
                    return new byte[] { 0x6F };
                case AddressType.PrivateKeyWIF:
                    return new byte[] { 0x80 };
                case AddressType.EncryptedPrivateKey:
                    return new byte[] { 0x01, 0x42 };
                case AddressType.ExtendedPublicKey:
                    return new byte[] { 0x04, 0x88, 0xB2, 0x1E };
            }
            throw new ArgumentOutOfRangeException("addressType", "{0} not supported address type for Base58Check".FormatWith(addressType));            
        }

    }
}
