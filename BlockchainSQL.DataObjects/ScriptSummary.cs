using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.DataObjects {
    public class ScriptSummary {
        public string Type { get; set; }

        public string Class { get; set; }

        public int Size { get; set; }

        public ScriptInstructionSummary[] Instructions { get; set; }
    }
}
