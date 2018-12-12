//#define USE_EMBEDDED_MAPPINGS 

using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using BlockchainSQL.Web.DataObjects;


namespace BlockchainSQL.Web.DataAccess {

    internal abstract class DatabaseGeneratorBase : IDatabaseGenerator {

        #region IDbGenerator Implementation

        public abstract bool DatabaseExists(string connectionString);
        public abstract void DropDatabase(string connectionString);

        public abstract void CreateEmptyDatabase(string connectionString);

        public void CreateNewDatabase(string connectionString, DatabaseGenerationDataPolicy dataPolicy, string databaseName) {
            using (var sessionFactory =  GetFluentConfig(connectionString, c => CreateDatabaseViaConfiguration(c, connectionString)).BuildSessionFactory()) {
                OnDatabaseCreated(connectionString);
                IDataGenerator dataPopulator = null;
                switch (dataPolicy) {
                    case DatabaseGenerationDataPolicy.NoData:
                        dataPopulator = new EmptyDataGenerator(sessionFactory);
                        break;
                    case DatabaseGenerationDataPolicy.DemoData:
                    case DatabaseGenerationDataPolicy.PrimingData:
                        dataPopulator = new PrimingDataGenerator(sessionFactory);
                        break;
                }
                dataPopulator?.Populate();
            }
        }

        protected virtual void OnDatabaseCreated(string connectionString) {
        } 

        public ISessionFactory CreateSessionFactory(string connectionString) {
            return  GetFluentConfig(connectionString).BuildSessionFactory();
        }

        #endregion

        protected virtual void CreateDatabaseViaConfiguration(Configuration configuration, string connectionString) {
            var schemaExport = new SchemaExport(configuration);
            //schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
        }

        //protected abstract Task CreateEmptyDatabaseIfNone(string connectionString);

        protected abstract IPersistenceConfigurer GetPersistenceConfigurer(string connectionString);

        protected virtual void ApplyDatabaseSpecificMappingConfiguration(MappingConfiguration mappingConfiguration) {
        }

#if USE_EMBEDDED_MAPPINGS

        protected virtual FluentConfiguration GetFluentConfig(string connectionString, Action<global::NHibernate.Cfg.Configuration> configurationAction = null) {
            if (configurationAction == null)
                configurationAction = (x) => { };

            return
                Fluently
                    .Configure()
                    .Database(GetPersistenceConfigurer(connectionString))
                    //.Mappings(c => c.FluentMappings.AddFromAssemblyOf<Query>())
                    .Mappings(m => m.HbmMappings.AddFromAssemblyOf<Query>())

                    .Mappings(c => c.FluentMappings.Conventions.Add<CoreConventions>())
                    //.Mappings(c => c.FluentMappings.Conventions.Add<AnsiStringConvention>())
                    .Mappings(c => c.FluentMappings.Conventions.Add<ForeignKeyConventions>())
                    .Mappings(c => c.FluentMappings.Conventions.Add<StringColumnLengthConvention>())
                    .Mappings(ApplyDatabaseSpecificMappingConfiguration)
                    //.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                    .ExposeConfiguration(c => c.AppendListeners(ListenerType.PreInsert, new[] {NHibernateListener.Instance}))
                    .ExposeConfiguration(c => c.AppendListeners(ListenerType.PreUpdate, new[] {NHibernateListener.Instance}))
                    //.ExposeConfiguration(c => c.SetInterceptor(new LoggingInterceptor()))
                    .ExposeConfiguration(c => c.SetProperty(global::NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "auto-quote"))
                    .ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns)
                    .ExposeConfiguration(configurationAction);

        }
#else
        protected virtual FluentConfiguration GetFluentConfig(string connectionString, Action<global::NHibernate.Cfg.Configuration> configurationAction = null) {
            if (configurationAction == null)
                configurationAction = (x) => { };

            return
                Fluently
                    .Configure()
                    .Database(GetPersistenceConfigurer(connectionString))
                    .Mappings(c => c.FluentMappings.AddFromAssemblyOf<SavedQueryMap>())
                    .Mappings(c => c.FluentMappings.Conventions.Add<CoreConventions>())
                    //.Mappings(c => c.FluentMappings.Conventions.Add<AnsiStringConvention>())
                    .Mappings(c => c.FluentMappings.Conventions.Add<ForeignKeyConventions>())
                    .Mappings(c => c.FluentMappings.Conventions.Add<StringColumnLengthConvention>())
                    .Mappings(ApplyDatabaseSpecificMappingConfiguration)
                    //.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                    .ExposeConfiguration(c => c.AppendListeners(ListenerType.PreInsert, new[] {NHibernateListener.Instance}))
                    .ExposeConfiguration(c => c.AppendListeners(ListenerType.PreUpdate, new[] {NHibernateListener.Instance}))
                    //.ExposeConfiguration(c => c.SetInterceptor(new LoggingInterceptor()))
                    .ExposeConfiguration(c => c.SetProperty(global::NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "auto-quote"))
                    .ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns)
                    .ExposeConfiguration(configurationAction);
            //.Mappings(x => x.FluentMappings.ExportTo("c:\\temp"));

        }

#endif
    }
}