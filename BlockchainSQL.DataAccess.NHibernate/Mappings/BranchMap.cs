using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class BranchMap : ClassMap<Branch> {

        public BranchMap() {
            Table("Branch");
            Id(x => x.ID).Column("ID").GeneratedBy.Assigned();
            Map(x => x.ForkHeight).Column("ForkHeight").Not.Nullable();
            Map(x => x.TimeStampUnix).Column("TimeStampUnix").Not.Nullable();
            Map(x => x.TimeStampUtc).Column("TimeStampUtc").Not.Nullable();
            References(x => x.ParentBranch).Column("ParentBranchID").Nullable().ForeignKey().Cascade.All();
            //HasMany(x => x.Blocks).KeyColumn("BranchID").Inverse().ForeignKeyCascadeOnDelete();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
}

