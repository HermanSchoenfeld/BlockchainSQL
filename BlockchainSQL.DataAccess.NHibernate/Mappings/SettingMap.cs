using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class SettingMap : ClassMap<Setting> {
        public SettingMap() {
            Table("Settings");
            Id(x => x.ID).Column("ID").GeneratedBy.Assigned();
            Map(x => x.Name).Column("Name").Not.Nullable().CustomType("AnsiString");
            Map(x => x.Value).Column("Value").Nullable().CustomType("AnsiString");
        }
    }
}
