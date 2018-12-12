using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Web.Models {
    public enum SearchResultType {
        Block,
        Transaction,
        NotFound
    }
}