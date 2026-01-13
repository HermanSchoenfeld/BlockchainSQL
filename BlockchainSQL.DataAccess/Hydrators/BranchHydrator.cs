// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

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

