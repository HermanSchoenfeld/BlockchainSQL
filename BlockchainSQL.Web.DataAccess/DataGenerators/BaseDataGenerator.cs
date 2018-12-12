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
                    data.Apply(session.SaveOrUpdate);
                    transaction.Commit();
                }
                session.Flush();
                session.Close();
            } 
        }

        protected abstract IEnumerable<object> CreateData();
    }
}