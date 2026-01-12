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

    public class ScriptMap : ClassMap<Script> {

        public ScriptMap() {
            Table("Script");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            Map(x => x.ScriptType).Column("ScriptType").Not.Nullable().CustomType<ScriptType>();
            Map(x => x.ScriptClass).Column("ScriptClass").Not.Nullable().CustomType<ScriptClass>();
            Map(x => x.ScriptByteLength).Column("ScriptByteLength").Nullable();
            Map(x => x.InstructionCount).Column("InstructionCount").Not.Nullable();
            HasMany(x => x.Instructions).KeyColumn("ScriptID").Inverse().ForeignKeyCascadeOnDelete();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
  
}
