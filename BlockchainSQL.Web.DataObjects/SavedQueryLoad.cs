using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Web.DataObjects {
    public class SavedQueryLoad {
        public virtual int ID { get; set; }

        public virtual SavedQuery SavedQuery { get; set; }

        public virtual DateTime LoadTimeUTC { get; set; }
    
    }
}
