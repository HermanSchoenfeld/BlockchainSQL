// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
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
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static ScriptInstructionSummary HydrateScriptInstructionSummary(DataRow sourceRow) {
            return HydrateScriptInstructionSummary(sourceRow, "");
        }

        public static ScriptInstructionSummary HydrateScriptInstructionSummary(DataRow sourceRow, string colPreFix) {
            var entity = new ScriptInstructionSummary();
            HydrateScriptInstructionSummary(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateScriptInstructionSummary(DataRow sourceRow, ScriptInstructionSummary entity, string colPreFix = "") {
            entity.OpCode = sourceRow.Get<string>(0);
            entity.DataLE = sourceRow.Get<byte[]>(1)?.ToHexString(true);
            entity.Description = sourceRow.Get<string>(2);
        }
    }
}