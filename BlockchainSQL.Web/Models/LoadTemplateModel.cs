// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Linq;
using BlockchainSQL.Web.DataObjects;

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