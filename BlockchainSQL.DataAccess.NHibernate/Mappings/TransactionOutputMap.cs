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
    public class TransactionOutputMap : ClassMap<TransactionOutput> {
        public TransactionOutputMap() {
            Table("TransactionOutput");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Transaction).Column("TransactionID").Not.Nullable().Index(Tools.Enums.GetDescription(DatabaseIndex.TransactionOutput_TransactionID));
            Map(x => x.Index).Column("Index").Not.Nullable();
            Map(x => x.ToAddressType).Column("ToAddressType").Not.Nullable().CustomType<AddressType>();
            Map(x => x.ToAddress).Column("ToAddress").Nullable().CustomType("AnsiString").Length(256).Index(Tools.Enums.GetDescription(DatabaseIndex.TransactionOutput_Address));
            Map(x => x.Value).Column("Value").Not.Nullable();            
            References(x => x.Script).Column("ScriptID").Nullable();
            Map(x => x.RowState).Column("RowState").Not.Nullable();            
        }
    }  
}
