// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.Web.DataObjects;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Output;

namespace BlockchainSQL.Web.DataAccess { 
    public class TemplateQueryMap : ClassMap<TemplateQuery> {

        public TemplateQueryMap() {
            Table("TemplateQuery");
            Id(x => x.ID).Column("ID").GeneratedBy.Identity();
            References(x => x.Category).Column("QueryCategoryID");
            Map(x => x.Title).Column("Title");
            Map(x => x.Description).Column("Description");
            Map(x => x.MSSQL).Column("MSSQL");
            Map(x => x.MySQL).Column("MySQL");
            Map(x => x.Oracle).Column("Oracle");
            Map(x => x.Sqlite).Column("Sqlite");
            Map(x => x.Active).Column("Active").Not.Nullable();
        }
    }
}
