using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC {
        private readonly string[] ScriptColumns = new[] {
            "ID",
            "ScriptType",
            "ScriptClass",
            "ScriptByteLength",
            "InstructionCount",
            "RowState"
        };

        public Script GetScript(long scriptID) {
            var results = (FindScripts(new[] { new ColumnValue("ID", scriptID) })).ToArray();
            if (results.Length != 1)
                throw new NoSingleRecordException("Script", scriptID, results.Length);
            var script = results[0];
            return script;
        }
        
        
        public ScriptSummary GetScriptSummary(long scriptID) {
            const string instructionSummaryQuery =
@"SELECT
	O.[Name] Name,
	SI.[DataLE] Data,
	OT.[en] Txt
FROM
	ScriptInstruction SI INNER JOIN
	OpCode O ON SI.OpCode = O.ID INNER JOIN
	[Text] OT ON O.[Description] = OT.Name
WHERE
	SI.ScriptID = {0}
ORDER BY
	SI.[Index] ASC";

            var script = GetScript(scriptID);
            var instructions = ExecuteQuery(instructionSummaryQuery.FormatWith(scriptID))
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateScriptInstructionSummary)
                .ToArray();
            return new ScriptSummary {
                Class = script.ScriptClass.ToString(),
                Type = script.ScriptType.ToString(),
                Size = script.ScriptByteLength,
                Instructions = instructions
            };
        }


        public virtual IEnumerable<Script> FindScripts(IEnumerable<ColumnValue> columnMatches = null, string whereClause = null) {
            return (this.Select(
                "Script",
                ScriptColumns,
                columnMatches: columnMatches,
                whereClause: whereClause
            ))
            .Rows
            .Cast<DataRow>()
            .Select(Hydrators.HydrateScript);
        }
    }
}


