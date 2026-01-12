// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Linq;
using BlockchainSQL.Web.DataObjects;
using NHibernate;
using NHibernate.Linq;

// ReSharper disable InconsistentNaming

namespace BlockchainSQL.Web {
	public class DataCache {

		public IEnumerable<QueryCategory> QueryCategoriesWithTemplates { get; private set; }

		public IDictionary<int, TemplateQuery> Templates { get; private set; }

		public void Load(ISessionFactory sessionFactory) {
			using (var session = sessionFactory.OpenSession()) {
				QueryCategoriesWithTemplates =
					(from q in session.Query<QueryCategory>()
					 select q)
						.FetchMany(q => q.Templates)
						.ToList();


				Templates = session.Query<TemplateQuery>().Fetch(t => t.Category).ToDictionary(t => t.ID);
			}
		}
	}
}