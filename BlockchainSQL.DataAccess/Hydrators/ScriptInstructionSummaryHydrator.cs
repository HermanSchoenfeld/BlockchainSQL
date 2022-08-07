using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen;
using Hydrogen.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static ScriptInstructionSummary HydrateScriptInstructionSummary(DataRow sourceRow) {
            return HydrateScriptInstructionSummary(sourceRow, "");
        }

        public static ScriptInstructionSummary HydrateScriptInstructionSummary(DataRow sourceRow, string colPreFix) {
            var entity = new ScriptInstructionSummary();
            HydrateScriptInstructionSummary(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateScriptInstructionSummary(DataRow sourceRow, ScriptInstructionSummary entity, string colPreFix = "") {
            entity.OpCode = sourceRow.Get<string>(0);
            entity.DataLE = sourceRow.Get<byte[]>(1)?.ToHexString(true);
            entity.Description = sourceRow.Get<string>(2);
        }
    }
}