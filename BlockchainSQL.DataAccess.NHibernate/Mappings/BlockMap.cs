using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class BlockMap : ClassMap<Block> {

        public BlockMap() {
            Table("Block");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            Map(x => x.Height).Column("Height").Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.Block_Height));
            Map(x => x.PreviousBlockHash).Column("PreviousBlockHash").Length(32).Not.Nullable().CustomSqlType("BINARY(32)");
            Map(x => x.Hash).Column("Hash").Length(32).Not.Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.Block_BlockHash)).CustomSqlType("BINARY(32)");
            References(x => x.Branch).Column("BranchID").ForeignKey("none"); // .Nullable().ForeignKey().No.Cascade.All().Index(Tools.Enums.GetDescription(DatabaseIndex.Block_Branch));
            Map(x => x.Size).Column("Size").Not.Nullable();
            Map(x => x.Nonce).Column("Nonce").Not.Nullable();
            Map(x => x.TimeStampUnix).Column("TimeStampUnix").Not.Nullable();
            Map(x => x.TimeStampUtc).Column("TimeStampUtc").Not.Nullable();
            Map(x => x.MerkleRoot).Column("MerkleRoot").Length(32 * 2).Not.Nullable()/*.Index(Tools.Enums.GetDescription(DatabaseIndex.Block_MerkleRoot))*/.CustomSqlType("BINARY(32)"); 
            Map(x => x.Bits).Column("Bits").Not.Nullable();
            Map(x => x.Difficulty).Column("Difficulty").Nullable();
            Map(x => x.Version).Column("Version").Not.Nullable();
            Map(x => x.TransactionCount).Column("TransactionCount").Not.Nullable();
            Map(x => x.OutputsBTC).Column("OutputsBTC").Nullable().Scale(8).Precision(20);
            Map(x => x.RewardBTC).Column("RewardBTC").Not.Nullable().Scale(8).Precision(20);
            Map(x => x.FeesBTC).Column("FeesBTC").Nullable().Scale(8).Precision(20);
            HasMany(x => x.Transactions).KeyColumn("BlockID").Inverse().ForeignKeyCascadeOnDelete();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
}

