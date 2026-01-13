// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.DataObjects;
using FluentNHibernate.Mapping;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public class TransactionInputMap : ClassMap<TransactionInput> {
        public TransactionInputMap() {
            Table("TransactionInput");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Transaction).Column("TransactionID").Not.Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.TransactionInput_TransactionID));
            Map(x => x.Index).Column("Index").Not.Nullable();            
            References(x => x.Script).Column("ScriptID").Nullable().ForeignKey().Cascade.Delete();
            References(x => x.WitScript).Column("WitScriptID").Nullable().ForeignKey().Cascade.Delete();
            Component(x => x.Outpoint, o => {
                o.Map(y => y.TXID).Column("OutpointTXID").Length(32).Not.Nullable().CustomType("AnsiString")/*.Index(Tools.Enums.GetDescription(DatabaseIndex.TransactionInput_Outpoint))*/.CustomSqlType("BINARY(32)");
                o.Map(y => y.OutputIndex).Column("OutpointIndex").Not.Nullable()/*.Index(Tools.Enums.GetDescription(DatabaseIndex.TransactionInput_Outpoint))*/;
            });
            References(x => x.TransactionOutput).Column("TransactionOutputID").Nullable().ForeignKey().Cascade.Delete().Index(Tools.Enums.GetDescription(DatabaseIndex.TransactionInput_Output));
            Map(x => x.Value).Column("Value").Nullable();
            Map(x => x.Sequence).Column("Sequence").Not.Nullable();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }  
}

