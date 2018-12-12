using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using Sphere10.Framework;

namespace BlockchainSQL.NUnit {
    internal static class TestHelper {

        public static byte[] ToHashBytes(string hash) {
            return BitcoinProtocolHelper.ConvertHashStringToDatabaseBytes(Pad(hash));
        }

        public static string Pad(string hash) {
            if (hash.Length < 64) {
                var sb = new StringBuilder();
                for (int i = 0; i < 64 - hash.Length; i++)
                    sb.Append("0");
                sb.Append(hash);
                return sb.ToString();
            } 
            return hash;
        }
    }
  
}
