using BlockchainSQL.Web.DataObjects;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Output;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.DataAccess { 
    public class SavedQueryMap : ClassMap<SavedQuery> {

        public SavedQueryMap() {
            Table("SavedQuery");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            Map(x => x.WebID).Column("WebID").Nullable().Index("Query_WebID");
            Map(x => x.DBMS).Column("DBMS").Not.Nullable().CustomType<SupportedDBMS>(); ;
            Map(x => x.DateTime).Column("DateTimeUTC").Not.Nullable();
            Map(x => x.SQL).Column("SQL").Not.Nullable();
            Map(x => x.ContentHash).Column("ContentHash").Not.Nullable().Index("Query_ContentHash");            
            Map(x => x.Result).Column("Result").Not.Nullable();
        }
    }
}

