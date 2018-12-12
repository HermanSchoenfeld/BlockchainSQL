using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static Script HydrateScript(DataRow sourceRow) {
            return HydrateScript(sourceRow, "");
        }

        public static Script HydrateScript(DataRow sourceRow, string colPreFix) {
            var entity = new Script();
            HydrateScript(sourceRow, entity, colPreFix);
            return entity;
        }    

        public static void HydrateScript(DataRow sourceRow, Script entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<int>(colPreFix + "ID");
            entity.ScriptType = (ScriptType)sourceRow.Get<uint>(colPreFix + "ScriptType");
            entity.ScriptClass = (ScriptClass) sourceRow.Get<uint>(colPreFix + "ScriptClass");
            entity.ScriptByteLength = sourceRow.Get<int>(colPreFix + "ScriptByteLength");
            entity.InstructionCount = sourceRow.Get<int>(colPreFix + "InstructionCount");
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");

        }

    }
}