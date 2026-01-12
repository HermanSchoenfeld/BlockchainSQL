// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Web.DataObjects;


namespace BlockchainSQL.Web.DataObjects {
    public class QueryCategory {
        public virtual int ID { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual QueryCategory Parent { get; set; }

        public virtual ICollection<TemplateQuery> Templates { get; set; }

        public virtual IEnumerable<TemplateQuery> AddTemplates {
            set {
                if (Templates == null)
                    Templates = new List<TemplateQuery>();
                if (value != null) {
                    foreach (var q in value) {
                        q.Category = this;
                        Templates.Add(q);
                    }
                }
            }
        }

    }
}
