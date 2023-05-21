using Hydrogen;

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
