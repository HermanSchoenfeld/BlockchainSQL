using FluentNHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using Sphere10.Framework.Data.NHibernate;

namespace BlockchainSQL.Web.DataAccess {
	internal static class NHibernateDatabaseGeneratorHelper {
		public static FluentConfiguration ConfigureBSQL(this FluentConfiguration config)
			=> config.Mappings(c => c.FluentMappings.AddFromAssemblyOf<SavedQueryMap>())
					.Mappings(c => c.FluentMappings.Conventions.Add<CoreConventions>())
					//.Mappings(c => c.FluentMappings.Conventions.Add<AnsiStringConvention>())
					.Mappings(c => c.FluentMappings.Conventions.Add<ForeignKeyConventions>())
					.Mappings(c => c.FluentMappings.Conventions.Add<StringColumnLengthConvention>())
					.Mappings(c => c.FluentMappings.Conventions.Add<BinaryColumnLengthConvention>())
					//.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
					//.ExposeConfiguration(c => c.SetInterceptor(new LoggingInterceptor()))
					.ExposeConfiguration(c => c.SetProperty(global::NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "auto-quote"))
					.ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns);
					
	}
}
