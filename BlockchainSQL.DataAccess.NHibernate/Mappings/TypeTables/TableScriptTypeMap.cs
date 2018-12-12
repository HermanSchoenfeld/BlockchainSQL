using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class TableScriptTypeMap : ClassMap<TableScriptType> {
        public TableScriptTypeMap() {
            Table("ScriptType");
            Id(x => x.ID).Column("ID").GeneratedBy.Assigned();
            Map(x => x.Name).Column("Name").Not.Nullable().CustomType("AnsiString");
            Map(x => x.Description).Column("Description").Nullable();
        }
    }
}
