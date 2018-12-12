using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BlockchainSQL.Web.Code;

namespace BlockchainSQL.Web.Models {
    public class QueryResultModel {

        public QueryResultModel() {
            Messages = new List<PageMessage>();
            Result = QueryResult.Empty;
        }

        public DateTime ExecutedOn { get; set; }

        public TimeSpan ExecutionDuration { get; set; }

        public QueryResult Result { get; set; }

        public List<PageMessage> Messages { get; set; }
        
    }
}