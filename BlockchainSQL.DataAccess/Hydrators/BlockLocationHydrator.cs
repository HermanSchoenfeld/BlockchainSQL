using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static BlockLocation HydrateBlockLocation(DataRow sourceRow) {
            return HydrateBlockLocation(sourceRow, "");
        }
        public static BlockLocation HydrateBlockLocation(DataRow sourceRow, string colPreFix) {
            var entity = new BlockLocation();
            HydrateBlockLocation(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateBlockLocation(DataRow sourceRow, BlockLocation entity, string colPreFix = "") {
            entity.Height = sourceRow.Get<int>(colPreFix + "Height");
            entity.Hash = sourceRow.Get<byte[]>(colPreFix + "Hash");
        }
    }
}