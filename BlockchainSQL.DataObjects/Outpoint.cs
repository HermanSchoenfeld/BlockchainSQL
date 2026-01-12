// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;

namespace BlockchainSQL.DataObjects {

    public struct Outpoint {

        public byte[] TXID { get; set; }

        public UInt32 OutputIndex { get; set; }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Outpoint && Equals((Outpoint) obj);
        }

        public bool Equals(Outpoint other) {
            return string.Equals(TXID, other.TXID) && OutputIndex == other.OutputIndex;
        }

        public override int GetHashCode() {
            unchecked {
                return ((TXID != null ? TXID.GetHashCode() : 0) * 397) ^ (int)OutputIndex;
            }
        }
    }
}
