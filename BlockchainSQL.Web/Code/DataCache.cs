using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Razor.Parser.SyntaxTree;
using BlockchainSQL.Web.DataObjects;
using BlockchainSQL.Web.DataObjectss;
using NHibernate;
using NHibernate.Linq;
using Sphere10.Framework;
using Sphere10.Framework.Data;
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