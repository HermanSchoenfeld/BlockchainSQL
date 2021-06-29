using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using System.Threading.Tasks;
using Sphere10.Framework;

namespace BlockchainSQL.Web.DataAccess {
    public abstract class BaseDataGenerator : IDataGenerator {
	    protected BaseDataGenerator(ISessionFactory sessionFactory) {
			SessionFactory = sessionFactory;
	    }

		protected ISessionFactory SessionFactory { get; set; }

        public void Populate() {
            using (var session = SessionFactory.OpenSession()) {
                using (var transaction = session.BeginTransaction()) {
                    var data = CreateData();
                    foreach (var o in data) {
	                    session.Save(o);
                    }
                    transaction.Commit();
                }
                session.Flush();
            } 
        }

        protected abstract IEnumerable<object> CreateData();
    }
}