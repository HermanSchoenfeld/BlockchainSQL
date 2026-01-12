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
    public class TextMap : ClassMap<Text> {

        public TextMap() {
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            Map(x => x.Name).Column("Name").Unique().Not.Nullable();
            Map(x => x.en).Column("en").Length(4000);
            Map(x => x.es).Column("es").Length(4000);
            Map(x => x.pt).Column("pt").Length(4000);
            Map(x => x.zh).Column("zh").Length(4000);
            Map(x => x.ar).Column("ar").Length(4000);
            Map(x => x.ru).Column("ru").Length(4000);
            Map(x => x.fr).Column("fr").Length(4000);
            Map(x => x.mn).Column("mn").Length(4000);
            Map(x => x.System).Column("System").Not.Nullable();
        }
    }
}
