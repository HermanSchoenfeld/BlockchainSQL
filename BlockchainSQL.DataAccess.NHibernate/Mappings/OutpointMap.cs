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

    public class OutpointMap : ComponentMap<Outpoint> {
        public OutpointMap() {
            Map(x => x.TXID).Column("TXID").Length(32).Not.Nullable().CustomSqlType("BINARY(32)");
            Map(x => x.OutputIndex).Column("OutputIndex").Not.Nullable();
        }
    }
}
