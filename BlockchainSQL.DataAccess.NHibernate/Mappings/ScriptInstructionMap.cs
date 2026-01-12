// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {

    public class ScriptInstructionMap : ClassMap<ScriptInstruction> {

        public ScriptInstructionMap() {
            Table("ScriptInstruction");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Script).Column("ScriptID").Not.Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.ScriptInstruction_ScriptID));
            Map(x => x.OpCode).Column("OpCode").Not.Nullable().CustomType<ScriptClass>();
            Map(x => x.Index).Column("Index").Nullable();
            Map(x => x.Valid).Column("Valid").Not.Nullable();
            Map(x => x.DataLE).Column("DataLE").Nullable().Length(1500);
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
  
}
