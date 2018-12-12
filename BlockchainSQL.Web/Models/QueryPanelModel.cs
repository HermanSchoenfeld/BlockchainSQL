using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainSQL.Web.Models {
    public class QueryPanelModel {

        public QueryPanelModel(string sql) {
            SQL = sql;
        }
        public string SQL { get; set; }

        public static QueryPanelModel Empty {
            get { return new QueryPanelModel(string.Empty); }
        }

    }
}