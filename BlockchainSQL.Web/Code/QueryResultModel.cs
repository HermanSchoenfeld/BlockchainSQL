using System.Data;

namespace BlockchainSQL.Web.Code
{
    public class QueryResult {

        public int PageSize { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public DataTable Data { get; set; }

        public static QueryResult Empty => new QueryResult {
            Page = 0,
            PageSize = 0,
            PageCount = 0,
            Data = null
        };
    }
}