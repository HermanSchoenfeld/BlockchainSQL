using System;
using System.Threading;

namespace BlockchainSQL.Processing {
	public abstract class BlockStreamBase : BizComponent, IBlockStream {
        private bool _isOpen;

        protected BlockStreamBase() {
            _isOpen = false;
            SkipTransactions = false;
        }

        public virtual bool IsOpen => _isOpen;
        
        public bool SkipTransactions { get; set; }

        public abstract bool HasMore { get; }

        public void Open() {
            CheckNotOpened();
            OpenInternal();
            _isOpen = true;
        }

        public void Close() {
            CheckOpened();
            try {
                CloseInternal();
            } finally {
                _isOpen = false;
            }
        }

        public int SeekToTip(BlockLocators blockLocators, CancellationToken cancellationToken) {
            CheckOpened();
            return SeekToTipInternal(blockLocators, cancellationToken);
        }

        public BlockStreamReadResult ReadNextBlocks(CancellationToken cancellationToken) {
            CheckOpened();
            return ReadNextBlocksInternal(SkipTransactions, cancellationToken);
        }

        public virtual void Dispose() {
            if (IsOpen) {
                Tools.Exceptions.ExecuteIgnoringException(Close);
            }
        }

        protected abstract void OpenInternal();

        protected abstract void CloseInternal();

        protected abstract int SeekToTipInternal(BlockLocators blockLocators, CancellationToken cancellationToken);

        protected abstract BlockStreamReadResult ReadNextBlocksInternal(bool skipTransactions, CancellationToken cancellationToken);

        protected void CheckOpened() {
            if (!_isOpen)
                throw new Exception("Block source is not opened");
        }

        protected void CheckNotOpened() {
            if (_isOpen)
                throw new Exception("Block source has already been opened");
        }
    }
}
