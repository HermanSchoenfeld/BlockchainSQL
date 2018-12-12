using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static Setting HydrateSettings(DataRow sourceRow) {
            return HydrateSettings(sourceRow, "");
        }
        public static Setting HydrateSettings(DataRow sourceRow, string colPreFix) {
            var entity = new Setting();
            HydrateSettings(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateSettings(DataRow sourceRow, Setting entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<int>(colPreFix + "ID");
            entity.Name = sourceRow.Get<string>(colPreFix + "Name");
            entity.Value = sourceRow.Get<string>(colPreFix + "Value");
        }
    }
}

