using BlockchainSQL.Web.DataObjects;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Output;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.DataAccess { 
    public class SavedQueryLoadMap : ClassMap<SavedQueryLoad> {

        public SavedQueryLoadMap() {
            Table("SavedQueryLoad");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.SavedQuery).Column("SavedQueryID").Not.Nullable();
            Map(x => x.LoadTimeUTC).Column("LoadTimeUTC").Not.Nullable();
        }
    }
}

