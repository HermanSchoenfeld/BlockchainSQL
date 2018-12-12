using System.IO;
using System.IO.MemoryMappedFiles;

namespace BlockchainSQL.Processing.Scanning {
     public class MemoryMappedBlockFileReader : BlockFileReader {
         private MemoryMappedFile _mmf;

         public MemoryMappedBlockFileReader(string blockFilePath) : base(blockFilePath) {             
         }

         protected override Stream OpenStream(string blockFilePath) {
             _mmf = MemoryMappedFile.CreateFromFile(blockFilePath, FileMode.Open, "BlockchainSQL", 0, MemoryMappedFileAccess.Read);
             return _mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.Read);             
         }


         protected override void CloseStream() {
             base.CloseStream();
             _mmf.Dispose();
         }
     }
}
