using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
    public class PersistResult {
        public PersistResult() {
            Block = Range.Empty;
            Transaction = Range.Empty;
            TransactionInput = Range.Empty;
            TransactionOutput = Range.Empty;
            Script = Range.Empty;
            ScriptInstruction = Range.Empty;
        }
        public Range Block { get; set; }
        public Range Transaction { get; set; }
        public Range TransactionInput { get; set; }
        public Range TransactionOutput { get; set; }
        public Range Script { get; set; }
        public Range ScriptInstruction { get; set; }

        public class Range : IEquatable<Range> {
            public long From { get; set; }

            public long To { get; set; }

            public long Total { get; set; }

            public static Range Empty => new Range {
                From = 0, To = 0, Total = 0
            };

            public override bool Equals(object obj) {
                return base.Equals(obj);
            }

            public bool Equals(Range other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return From == other.From && To == other.To && Total == other.Total;
            }

            public override int GetHashCode() {
                unchecked {
                    var hashCode = (int) From;
                    hashCode = (hashCode*397) ^ (int) To;
                    hashCode = (hashCode*397) ^ (int) Total;
                    return hashCode;
                }
            }

            public static bool operator ==(Range left, Range right) {
                return Equals(left, right);
            }

            public static bool operator !=(Range left, Range right) {
                return !Equals(left, right);
            }
        }
    }
}
