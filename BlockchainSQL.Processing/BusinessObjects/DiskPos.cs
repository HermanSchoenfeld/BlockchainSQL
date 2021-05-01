using System;

namespace BlockchainSQL.Processing.BusinessObjects
{
    public class DiskPos : IEquatable<DiskPos> {
        public readonly int FileIndex;
        public readonly uint FileOffset;

        public DiskPos(string blockFilePath, uint fileOffset) : this(
            ProcessingTierHelper.BlockFileNumber(blockFilePath), fileOffset) {
        }

        public DiskPos(int fileIndex, uint fileOffset) {
            FileIndex = fileIndex;
            FileOffset = fileOffset;
        }

        public static DiskPos None => new DiskPos(-1, 0);

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DiskPos) obj);
        }

        public bool Equals(DiskPos other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FileIndex == other.FileIndex && FileOffset == other.FileOffset;
        }

        public override int GetHashCode() {
            unchecked {
                return (FileIndex*397) ^ (int) FileOffset;
            }
        }

        public static bool operator ==(DiskPos left, DiskPos right) {
            return Equals(left, right);
        }

        public static bool operator !=(DiskPos left, DiskPos right) {
            return !Equals(left, right);
        }


        public override string ToString() {
            return String.Format("blk{0:0000}.dat:{1}", FileIndex, FileOffset);
        }
    }
}
