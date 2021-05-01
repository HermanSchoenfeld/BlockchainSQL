using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataObjects
{
    public class Script {
        public Script() {
            RowState = 1;
        }

        public virtual long ID { get; set; }

        public virtual ScriptType ScriptType { get; set; }

        public virtual ScriptClass ScriptClass { get; set; }

        public virtual int ScriptByteLength { get; set; }

        public virtual int InstructionCount { get; set; }

        public virtual ICollection<ScriptInstruction> Instructions { get; set; }

        public virtual byte RowState { get; set; }

        // Memory only
        public virtual byte[] ScriptBytesLE { get; set; }

        public virtual void AddInstruction(ScriptInstruction instruction) {
            AddInstructions(new[] { instruction });
        }

        public virtual void AddInstructions(IEnumerable<ScriptInstruction> instructions) {
            if (Instructions == null)
                Instructions = new List<ScriptInstruction>();
            foreach (var instruction in instructions) {
                instruction.Index = (ushort) Instructions.Count;
                Instructions.Add(instruction);
                instruction.Script = this;                
            }
            InstructionCount = Instructions.Count;
        }


    }
}
