using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static StatementLine HydrateStatementLine(DataRow sourceRow) {
            return HydrateStatementLine(sourceRow, "");
        }
        public static StatementLine HydrateStatementLine(DataRow sourceRow, string colPreFix) {
            var entity = new StatementLine();
            HydrateStatementLine(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateStatementLine(DataRow sourceRow, StatementLine entity, string colPreFix = "") {
            entity.TXDate = sourceRow.Get<DateTime>("TXDate");
            entity.TXType = sourceRow.Get<string>("TXType");
            entity.TXID = sourceRow.Get<byte[]>("TXID");
            entity.TXID_IX = sourceRow.Get<uint>("TXID_IX");
            entity.TXAmount = sourceRow.Get<decimal>("TXAmount");
            entity.BlockHeight = sourceRow.Get<uint>("Block");
        }
    }
}
