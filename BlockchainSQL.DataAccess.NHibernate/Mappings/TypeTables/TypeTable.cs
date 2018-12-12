using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.DataAccess.NHibernate.Mappings {
    public abstract class TypeTable {
        public virtual byte ID { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

    }
}
