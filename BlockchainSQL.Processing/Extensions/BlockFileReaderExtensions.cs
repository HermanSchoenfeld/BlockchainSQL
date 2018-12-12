using System;
using BlockchainSQL.Processing.Scanning;

namespace BlockchainSQL.Processing {
    public static class BlockFileReaderExtensions {

        public static IDisposable OpenFileScope(this IBlockFileReader reader, string blockFilePath) {
            return new BlockFileScope(reader, blockFilePath);
        }

    }
}
