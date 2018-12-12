using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlockchainSQL.Web.DataObjects;
using BlockchainSQL.Web.DataObjectss;

namespace BlockchainSQL.Web.Models {
    public class LoadTemplateModel {

        public LoadTemplateModel(IEnumerable<QueryCategory> queryCategoriesWithTemplates) {
            Categories =
                from q in queryCategoriesWithTemplates
                select new Category {
                    ID = q.ID,
                    Title = q.Title,
                    Description = q.Description,
                    Templates =
                        from t in q.Templates
                        where t.Active
                        select new Template {
                            ID = t.ID,
                            Title = t.Title,
                            Description = t.Description,
                            TemplateSQL = t.MSSQL
                        }
                };
        }

        public IEnumerable<Category> Categories;

        public class Category {
            public int ID { get; set; }
            public string Title { get; set; }

            public string Description { get; set; }

            public IEnumerable<Template>  Templates { get; set; }
        }

        public class Template {

            public int ID { get; set; }
            public string Title { get; set; }

            public string Description { get; set; }

            public string TemplateSQL { get; set; }


        }

    }
}