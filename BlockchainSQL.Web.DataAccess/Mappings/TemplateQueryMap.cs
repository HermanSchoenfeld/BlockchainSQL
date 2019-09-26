using BlockchainSQL.Web.DataObjects;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Output;

namespace BlockchainSQL.Web.DataAccess { 
    public class TemplateQueryMap : ClassMap<TemplateQuery> {

        public TemplateQueryMap() {
            Table("TemplateQuery");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Category).Column("QueryCategoryID");
            Map(x => x.Title).Column("Title");
            Map(x => x.Description).Column("Description");
            Map(x => x.MSSQL).Column("MSSQL");
            Map(x => x.MySQL).Column("MySQL");
            Map(x => x.Oracle).Column("Oracle");
            Map(x => x.Sqlite).Column("Sqlite");
            Map(x => x.Active).Column("Active").Not.Nullable();
        }
    }
}
