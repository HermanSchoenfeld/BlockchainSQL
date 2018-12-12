using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {

    public class ScriptInstructionMap : ClassMap<ScriptInstruction> {

        public ScriptInstructionMap() {
            Table("ScriptInstruction");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Script).Column("ScriptID").Not.Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.ScriptInstruction_ScriptID));
            Map(x => x.OpCode).Column("OpCode").Not.Nullable().CustomType<ScriptClass>();
            Map(x => x.Index).Column("Index").Nullable();
            Map(x => x.Valid).Column("Valid").Not.Nullable();
            Map(x => x.DataLE).Column("DataLE").Nullable().Length(1500);
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
  
}
