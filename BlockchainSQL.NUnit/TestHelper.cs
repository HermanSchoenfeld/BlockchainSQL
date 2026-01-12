// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Text;
using BlockchainSQL.Processing;

namespace BlockchainSQL.NUnit
{
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
