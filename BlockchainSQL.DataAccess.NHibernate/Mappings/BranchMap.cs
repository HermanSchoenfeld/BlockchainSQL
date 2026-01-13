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
    public class BranchMap : ClassMap<Branch> {

        public BranchMap() {
            Table("Branch");
            Id(x => x.ID).Column("ID").GeneratedBy.Assigned();
            Map(x => x.ForkHeight).Column("ForkHeight").Not.Nullable();
            Map(x => x.TimeStampUnix).Column("TimeStampUnix").Not.Nullable();
            Map(x => x.TimeStampUtc).Column("TimeStampUtc").Not.Nullable();
            References(x => x.ParentBranch).Column("ParentBranchID").Nullable().ForeignKey().Cascade.All();
            //HasMany(x => x.Blocks).KeyColumn("BranchID").Inverse().ForeignKeyCascadeOnDelete();
            Map(x => x.RowState).Column("RowState").Not.Nullable();
        }
    }
}

