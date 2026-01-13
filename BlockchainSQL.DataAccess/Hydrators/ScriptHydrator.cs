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

        public static Script HydrateScript(DataRow sourceRow) {
            return HydrateScript(sourceRow, "");
        }

        public static Script HydrateScript(DataRow sourceRow, string colPreFix) {
            var entity = new Script();
            HydrateScript(sourceRow, entity, colPreFix);
            return entity;
        }    

        public static void HydrateScript(DataRow sourceRow, Script entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<long>(colPreFix + "ID");
            entity.ScriptType = (ScriptType)sourceRow.Get<uint>(colPreFix + "ScriptType");
            entity.ScriptClass = (ScriptClass) sourceRow.Get<uint>(colPreFix + "ScriptClass");
            entity.ScriptByteLength = sourceRow.Get<int>(colPreFix + "ScriptByteLength");
            entity.InstructionCount = sourceRow.Get<int>(colPreFix + "InstructionCount");
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");
        }
    }
}