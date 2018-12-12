using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainSQL.Web.Models {
    public class SearchResult {
        public SearchResultType ResultType { get; set;  }

        public string Key { get; set; }
    }
}