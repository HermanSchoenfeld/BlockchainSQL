using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static TransactionInput HydrateTransactionInput(DataRow sourceRow) {
            return HydrateTransactionInput(sourceRow, "");
        }
        public static TransactionInput HydrateTransactionInput(DataRow sourceRow, string colPreFix) {
            var entity = new TransactionInput();
            HydrateTransactionInput(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateTransactionInput(DataRow sourceRow, TransactionInput entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<long>(colPreFix + "ID");
            entity.Transaction = new Transaction { ID = sourceRow.Get<long>(colPreFix + "TransactionID") };
            entity.Index = sourceRow.Get<uint>(colPreFix + "Index");
            var scriptID = sourceRow.Get<long?>(colPreFix + "ScriptID");
            entity.Script = scriptID != null ? new Script {ID = scriptID.Value} : null;
            var witScriptID = sourceRow.Get<long?>(colPreFix + "WitScriptID");
            entity.WitScript = witScriptID != null ? new Script { ID = witScriptID.Value } : null;
            entity.Outpoint = new Outpoint {
                TXID = sourceRow.Get<byte[]>(colPreFix + "OutpointTXID"),
                OutputIndex = sourceRow.Get<uint>(colPreFix + "OutpointIndex"),                
            };
            var txoid = sourceRow.Get<long?>(colPreFix + "TransactionOutputID");
            entity.TransactionOutput = txoid.HasValue ? new TransactionOutput { ID = txoid.Value } : null;
            entity.Value = sourceRow.Get<long>(colPreFix + "Value");
            entity.Sequence = sourceRow.Get<uint>(colPreFix + "Sequence");
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");
        }
    }
}
      