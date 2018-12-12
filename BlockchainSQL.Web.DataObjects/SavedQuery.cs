using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Web.DataObjects {
    public class SavedQuery {
        public virtual int ID { get; set; }

        public virtual string WebID { get; set; }

        public virtual SupportedDBMS DBMS { get; set; }

        public virtual DateTime DateTime { get; set; }

        public virtual string SQL { get; set; }

        public virtual string ContentHash { get; set; }

        public virtual string Result { get; set; }


    }
}
