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
