using BlockchainSQL.Web.DataObjects;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Output;

namespace BlockchainSQL.Web.DataAccess {
	public class ExecutedQueryMap : ClassMap<ExecutedQuery> {
		public ExecutedQueryMap() {
			Table("ExecutedQuery");
			Id(x => x.ID).Column("ID").GeneratedBy.Identity();
			Map(x => x.Query);
			Map(x => x.PageNumber);
			Map(x => x.PageSize);
			Map(x => x.IP);
			Map(x => x.ExecutedOn);
			Map(x => x.ExecutionDurationMS);
		}

	}
}
