using System;
using System.IO;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;


namespace BlockchainSQL.Processing {
	public class BlockFileReader : IBlockFileReader {
        private StreamProfiler _stream;
        private EndianBinaryReader _reader;
        protected int BlocksRead;


        public BlockFileReader(string filePath) {
            _stream = null;
            _reader = null;
            BlocksRead = 0;
            SkipTransactions = false;
            ReadMagicID = true;
            ReadSize = true;
            Path = filePath;
            _stream = new StreamProfiler(OpenStream(filePath), true);
            _reader = new EndianBinaryReader(new LittleEndianBitConverter(), _stream);
            BlocksRead = 0;
        }

        #region IBlockFileReader Implementation

        public string Path { get; private set; }

        public bool SkipTransactions { get; set; }

        public bool ReadMagicID { get; set; }

        public bool ReadSize { get; set; }



        public virtual Block ReadNextBlock() {
            return ReadBlockInternal();
        }

        public BlockFileReaderPeekResult PeekNextBlock() {
            return PeekNextBlockInternal();
        }

        public void Seek(int offset, SeekOrigin seekOrigin) {
            #region optimization: don't seek if already there
            switch (seekOrigin) {
                case SeekOrigin.Begin:
                    if (_stream.Position == offset)
                        return;
                    break;
                case SeekOrigin.Current:
                    if (offset == 0)
                        return;
                    break;
                case SeekOrigin.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(seekOrigin), seekOrigin, null);
            }

            #endregion
            _stream.Seek(offset, seekOrigin);
        }

        public bool HasMoreBlocks {
            get {
                var diff = _stream.Length - _stream.Position;
                var hasMore = _stream.Position < _stream.Length;
                return hasMore && _stream.PeekByte() != 0;
            }
        }

        public virtual long TotalBytesProcessed {
            get { return _stream.Position; }
        }

        public long TotalBlocksRead {
            get { return BlocksRead; }
        }


        public virtual void Dispose() {
            try {
                Tools.Lambda.ActionIgnoringExceptions(_reader.Close)();
                Tools.Lambda.ActionIgnoringExceptions(_reader.Dispose)();
                Tools.Lambda.ActionIgnoringExceptions(CloseStream)();
                Tools.Lambda.ActionIgnoringExceptions(_stream.Dispose)();
                _reader = null;
                _stream = null;
            } catch {
                // ignore
            }
        }

        #endregion

        #region Implementation Methods

        protected virtual Block ReadBlockInternal() {
            var block = BitcoinProtocolParser.ParseBlock(_reader, !SkipTransactions, false, ReadMagicID, ReadSize);
            BlocksRead++;
            return block;
        }

        internal virtual BlockFileReaderPeekResult PeekNextBlockInternal() {
            if (!HasMoreBlocks) {
                throw new SoftwareException("No more blocks in file");
            }
            var peekBytes = _stream.PeekBytes(44);
            using (var peekStream = new MemoryStream(peekBytes))
            using (var peekReader = new EndianBinaryReader(EndianBitConverter.Little, peekStream)) {
                var magicID = peekReader.ReadUInt32();
                var blockSize = peekReader.ReadUInt32();
                var versionNumber = peekReader.ReadInt32();
                var prevBlockHash = BitcoinProtocolParser.ParseHashBytes(peekReader);
                return new BlockFileReaderPeekResult {
                    NextBlockPrevBlockHash = prevBlockHash, NextBlockSize = blockSize, AdditionalOffset = 2*sizeof (uint)
                };
            }
        }


        protected virtual Stream OpenStream(string blockFilePath) {
            return new FileStream(blockFilePath, FileMode.Open);
        }

        protected virtual void CloseStream() {
            //_stream.Flush();
            _stream.Close();
        }

        #endregion
    }
}
