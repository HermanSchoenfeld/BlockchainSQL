using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {

    public class OutpointMap : ComponentMap<Outpoint> {
        public OutpointMap() {
            Map(x => x.TXID).Column("TXID").Length(32).Not.Nullable().CustomSqlType("BINARY(32)");
            Map(x => x.OutputIndex).Column("OutputIndex").Not.Nullable();
        }
    }
}
