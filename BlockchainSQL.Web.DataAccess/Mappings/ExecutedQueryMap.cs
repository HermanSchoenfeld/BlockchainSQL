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
	public class ExecutedQueryMap : ClassMap<ExecutedQuery> {
		public ExecutedQueryMap() {
			Table("ExecutedQuery");
			Id(x => x.ID).Column("ID").GeneratedBy.Identity();
			Map(x => x.Query);
			Map(x => x.PageNumber);
			Map(x => x.PageSize);
			Map(x => x.IP);
			Map(x => x.ExecutedOn);
			Map(x => x.ExecutionDurationMS);
		}

	}
}
