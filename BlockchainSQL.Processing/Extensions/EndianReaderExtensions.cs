using Sphere10.Framework;

namespace BlockchainSQL.Processing
{
    public static class EndianReaderExtensions {
        public static ulong ReadCompactVarInt(this EndianBinaryReader reader) {
            return BitcoinProtocolParser.ParseCompactVarInt(reader);
        }

        public static long ReadVarInt(this EndianBinaryReader reader) {
            return BitcoinProtocolParser.ParseVarInt(reader);
        }
    }
}
