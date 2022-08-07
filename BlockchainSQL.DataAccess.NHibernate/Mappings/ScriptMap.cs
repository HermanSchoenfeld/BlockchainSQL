using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {

    public class ScriptMap : ClassMap<Script> {

        public ScriptMap() {
            Table("Script");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            Map(x => x.ScriptType).Column("ScriptType").Not.Nullable().CustomType<ScriptType>();
            Map(x => x.ScriptClass).Column("ScriptClass").Not.Nullable().CustomType<ScriptClass>();
            Map(x => x.ScriptByteLength).Column("ScriptByteLength").Nullable();
            Map(x => x.InstructionCount).Column("InstructionCount").Not.Nullable();
            HasMany(x => x.Instructions).KeyColumn("ScriptID").Inverse().ForeignKeyCascadeOnDelete();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
  
}
