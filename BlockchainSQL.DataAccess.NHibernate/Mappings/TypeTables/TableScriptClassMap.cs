using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class TableScriptClassMap : ClassMap<TableScriptClass> {
        public TableScriptClassMap() {
            Table("ScriptClass");
            Id(x => x.ID).Column("ID").GeneratedBy.Assigned();
            Map(x => x.Name).Column("Name").Not.Nullable().CustomType("AnsiString");
            Map(x => x.Description).Column("Description").Nullable();

        }
    }
}