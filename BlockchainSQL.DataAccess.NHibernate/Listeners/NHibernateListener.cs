using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace BlockchainSQL.DataAccess.NHibernate {
    public class NHibernateListener : IPreInsertEventListener, IPreUpdateEventListener {
        private static volatile NHibernateListener _instance;
        private static volatile object _threadLock;

        static NHibernateListener() {
            _instance = null;
            _threadLock = new object();
        }

        private NHibernateListener() {
        }

        public static NHibernateListener Instance {
            get {
                if (_instance == null) {
                    lock (_threadLock) {
                        if (_instance == null) {
                            _instance = new NHibernateListener();
                        }
                    }
                }
                return _instance;
            }
        }

        public bool OnPreInsert(PreInsertEvent @event) {
            //if (@event.Entity is BusinessEntity) {
            //    var entity = (BusinessEntity)@event.Entity;
            //    entity.RowVersion = 1;
            //    entity.LastUpdatedOn = DateTime.UtcNow;
            //    entity.Active = true;
            //    Set(@event.Persister, @event.State, "RowVersion", entity.RowVersion);
            //    Set(@event.Persister, @event.State, "LastUpdatedOn", entity.LastUpdatedOn);
            //    Set(@event.Persister, @event.State, "Active", entity.Active);
            //}
            return false;
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        public bool OnPreUpdate(PreUpdateEvent @event) {
            //if (@event.Entity is BusinessEntity) {
            //    var entity = (BusinessEntity)@event.Entity;
            //    entity.RowVersion++;
            //    entity.LastUpdatedOn = DateTime.UtcNow;
            //    Set(@event.Persister, @event.State, "RowVersion", entity.RowVersion);
            //    Set(@event.Persister, @event.State, "LastUpdatedOn", entity.LastUpdatedOn);
            //}
            return false;
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value) {
            int index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}