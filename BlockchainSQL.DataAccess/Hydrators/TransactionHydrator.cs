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

        public static Transaction HydrateTransaction(DataRow sourceRow) {
            return HydrateTransaction(sourceRow, "");
        }
        public static Transaction HydrateTransaction(DataRow sourceRow, string colPreFix) {
            var entity = new Transaction();
            HydrateTransaction(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateTransaction(DataRow sourceRow, Transaction entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<long>(colPreFix + "ID");
            entity.TXID = sourceRow.Get<byte[]>(colPreFix + "TXID");
            entity.WTXID = sourceRow.Get<byte[]>(colPreFix + "WTXID");
            entity.Size = sourceRow.Get<uint>(colPreFix + "Size");
            entity.Version = sourceRow.Get<int>(colPreFix + "Version");
            entity.InputCount = sourceRow.Get<uint>(colPreFix + "InputCount");
            entity.OutputCount = sourceRow.Get<uint>(colPreFix + "OutputCount");
            entity.InputsBTC = sourceRow.Get<decimal?>(colPreFix + "InputsBTC");
            entity.OutputsBTC = sourceRow.Get<decimal?>(colPreFix + "OutputsBTC");
            entity.FeeBTC = sourceRow.Get<decimal?>(colPreFix + "FeeBTC");
            entity.LockTime = sourceRow.Get<uint>(colPreFix + "LockTime");
            entity.Index = sourceRow.Get<uint>(colPreFix + "Index");
            entity.Block = new Block { ID = sourceRow.Get<int>(colPreFix + "BlockID") };
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");
        }
    }
}