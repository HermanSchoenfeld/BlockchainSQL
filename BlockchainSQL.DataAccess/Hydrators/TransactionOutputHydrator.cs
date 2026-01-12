using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static TransactionOutput HydrateTransactionOutput(DataRow sourceRow) {
            return HydrateTransactionOutput(sourceRow, "");
        }
        public static TransactionOutput HydrateTransactionOutput(DataRow sourceRow, string colPreFix) {
            var entity = new TransactionOutput();
            HydrateTransactionOutput(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateTransactionOutput(DataRow sourceRow, TransactionOutput entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<long>(colPreFix + "ID");
            entity.Transaction = new Transaction { ID = sourceRow.Get<long>(colPreFix + "TransactionID") };
            entity.Index = sourceRow.Get<uint>(colPreFix + "Index");
            entity.ToAddressType = sourceRow.Get<AddressType>(colPreFix + "ToAddressType");
            entity.ToAddress = sourceRow.Get<string>(colPreFix + "ToAddress");
            entity.Value = sourceRow.Get<long>(colPreFix + "Value");
            var scriptID = sourceRow.Get<long?>(colPreFix + "ScriptID");
            entity.Script = scriptID != null ? new Script { ID = scriptID.Value } : null;
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");
        }
    }
}                    