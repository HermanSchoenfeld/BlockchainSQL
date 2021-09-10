using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Web.DataObjects {
	public class ExecutedQuery {
		public virtual int ID { get; set; }
		public virtual string Query { get; set; }
		public virtual int PageNumber { get; set; }
		public virtual int PageSize { get; set; }
		public virtual string IP { get; set; }
		public virtual DateTime ExecutedOn { get; set; }
		public virtual int ExecutionDurationMS { get; set; }
	}
}
