// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework;

namespace BlockchainSQL.Processing {
	public static class EndianReaderExtensions {
        public static ulong ReadCompactVarInt(this EndianBinaryReader reader) {
            return BitcoinProtocolParser.ParseCompactVarInt(reader);
        }

        public static ulong ReadVarInt(this EndianBinaryReader reader) {
            return BitcoinProtocolParser.ParseVarInt(reader);
        }
    }
}
