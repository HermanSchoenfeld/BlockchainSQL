
using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class TransactionMap : ClassMap<Transaction> {

        public TransactionMap() {
            Table("Transaction");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Block).Column("BlockID").Not.Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.Transaction_BlockID));
            Map(x => x.Index).Column("Index").Not.Nullable();
            Map(x => x.TXID).Column("TXID").Not.Nullable().Length(32).Index(Tools.Enums.GetDescription(DatabaseIndex.Transaction_TXID)).CustomSqlType("BINARY(32)");
            Map(x => x.Size).Column("Size").Not.Nullable();                        
            Map(x => x.InputCount).Column("InputCount").Not.Nullable();
            Map(x => x.OutputCount).Column("OutputCount").Not.Nullable();
            Map(x => x.InputsBTC).Column("InputsBTC").Nullable().Scale(8).Precision(20);
            Map(x => x.OutputsBTC).Column("OutputsBTC").Nullable().Scale(8).Precision(20);
            Map(x => x.FeeBTC).Column("FeeBTC").Nullable().Scale(8).Precision(20);
            Map(x => x.LockTime).Column("LockTime").Not.Nullable();
            HasMany(x => x.Inputs).KeyColumn("TransactionID").Inverse().ForeignKeyCascadeOnDelete();
            HasMany(x => x.Outputs).KeyColumn("TransactionID").Inverse().ForeignKeyCascadeOnDelete();
            Map(x => x.Version).Column("Version").Not.Nullable();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
}
