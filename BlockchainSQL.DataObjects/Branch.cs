using System;
using System.Collections.Generic;

namespace BlockchainSQL.DataObjects {

    public class Branch {

        public Branch() {
            RowState = 1;
        }

        public virtual int ID { get; set; }
        public virtual int ForkHeight { get; set; }
        public virtual uint TimeStampUnix { get; set; }
        public virtual DateTime TimeStampUtc { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
        public virtual Branch ParentBranch { get; set; }
        public virtual byte RowState { get; set; }

    }
}
