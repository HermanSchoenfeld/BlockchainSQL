using BlockchainSQL.Web.DataObjectss;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Output;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.DataAccess { 
    public class QueryCategoryMap : ClassMap<QueryCategory> {
        public QueryCategoryMap() {
            Table("QueryCategory");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            Map(x => x.Title).Column("Title");
            Map(x => x.Description).Column("Description");
            References(x => x.Parent).Column("ParentID").Nullable();
            HasMany(x => x.Templates);
        }
    }
}
