using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Web.DataObjectss;

namespace BlockchainSQL.Web.DataObjects {
    public class TemplateQuery {
        public virtual int ID { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual QueryCategory Category { get; set; }

        public virtual string MSSQL { get; set; }

        public virtual string MySQL { get; set; }
        public virtual string Oracle { get; set; }

        public virtual string Sqlite { get; set; }

        public virtual string Firebird { get; set; }

        public virtual bool Active { get; set; }

    }
}
