using System;

namespace BlockchainSQL.Processing.Scanning {
     public class BlockFileScope : IDisposable {
         private readonly IBlockFileReader _reader;
         public BlockFileScope(IBlockFileReader reader, string blockFilePath) {
            _reader = reader;
            //if (Reader.IsOpen)
            //    throw new ApplicationException("BlockFileReader is already open");
            //Reader.Open(blockFilePath);
        }


         public void Dispose() {
             _reader.Dispose();
         }
     }
}
