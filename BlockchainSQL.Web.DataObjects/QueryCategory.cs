using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Web.DataObjects;


namespace BlockchainSQL.Web.DataObjectss {
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
