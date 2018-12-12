using System;

namespace BlockchainSQL.DataObjects {
    public class ScriptInstruction {

        public ScriptInstruction() {
            RowState = 1;
        }

        public virtual long ID { get; set; }

        public virtual OpCode OpCode { get; set; }

        public virtual Script Script { get; set; }

        public virtual ushort Index { get; set; }

        public virtual bool Valid { get; set; }

        public virtual byte[] DataLE { get; set; }

        public virtual byte RowState { get; set; }

    }
}
