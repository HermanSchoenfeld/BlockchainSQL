using System.Linq;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing
{
    public class BlockLocators {
        public BlockLocation[] Locations { get; set; } // Database byte-order

        public static BlockLocators Empty => new BlockLocators {
            Locations = new BlockLocation[]  { new BlockLocation { Hash = BitcoinProtocolHelper.EmptyHash, Height = -1 } }
        };

	    public override bool Equals(object obj) {
		    return this.Equals(obj as BlockLocators);
	    }

	    public bool Equals(BlockLocators other) {
		    if (ReferenceEquals(null, other)) return false;
		    if (ReferenceEquals(this, other)) return true;
			if ((Locations == null && other.Locations != null) || (Locations != null && other.Locations == null))
				return false;
		    return Locations.SequenceEqual(other.Locations);
	    }

	    public override int GetHashCode() {
		    return (Locations != null ? Locations.GetHashCode() : 0);
	    }

	    public static bool operator ==(BlockLocators left, BlockLocators right) {
		    return Equals(left, right);
	    }

	    public static bool operator !=(BlockLocators left, BlockLocators right) {
		    return !Equals(left, right);
	    }
    }

}
