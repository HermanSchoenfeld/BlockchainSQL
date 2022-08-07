using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static Branch HydrateBranch(DataRow sourceRow) {
            return HydrateBranch(sourceRow, "");
        }
        public static Branch HydrateBranch(DataRow sourceRow, string colPreFix) {
            var entity = new Branch();
            HydrateBranch(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateBranch(DataRow sourceRow, Branch entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<int>(colPreFix + "ID");
            entity.ForkHeight = sourceRow.Get<int>(colPreFix + "ForkHeight");
            entity.TimeStampUnix = sourceRow.Get<uint>(colPreFix + "TimeStampUnix");
            entity.TimeStampUtc = sourceRow.Get<DateTime>(colPreFix + "TimeStampUtc");
            var parentBranchID = sourceRow.Get<int?>(colPreFix + "ParentBranchID");
            entity.ParentBranch = parentBranchID.HasValue ? new Branch { ID = parentBranchID.Value } : null;
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");
        }
    }
}

