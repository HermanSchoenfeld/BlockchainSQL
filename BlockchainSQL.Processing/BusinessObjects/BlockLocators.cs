using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using NBitcoin.Crypto;
using NBitcoin.Protocol;
using Sphere10.Framework.Collections;

namespace BlockchainSQL.Processing {
    public class BlockLocators {
        public BlockLocation[] Locations { get; set; } // Database byte-order

        public static BlockLocators Empty => new BlockLocators {
            Locations = new BlockLocation[0]  //new [] { new BlockLocation { Hash = BitcoinProtocolHelper.EmptyHash, Height = -1 } }
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
