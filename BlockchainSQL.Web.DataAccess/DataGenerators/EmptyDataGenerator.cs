using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace BlockchainSQL.Web.DataAccess {
    public sealed class EmptyDataGenerator : BaseDataGenerator {
        public EmptyDataGenerator(ISessionFactory sessionFactory) : base(sessionFactory) {
        }

        protected override IEnumerable<object> CreateData() {
            return Enumerable.Empty<object>();
        }
    }
}
