using System.Threading.Tasks;
using System.Web.Mvc;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.Models;

namespace BlockchainSQL.Web.Controllers
{
    public class SearchController : BaseController {
        // GET: Search
        public async Task<ActionResult> Index(string text) {
            if (string.IsNullOrWhiteSpace(text)) {
                return HomePageRedirect();
            }
	        uint height;
	        if (uint.TryParse(text, out height)) {
		        return Redirect("/Block/" + height);
	        }
	        if (!(BitcoinProtocolHelper.IsValidHashString(text) || BitcoinProtocolHelper.IsValidAddress(text))) {
                return HomePageRedirect("Invalid search pattern");
            }
            var repo = new DBBlockchainRepository(base.Config.BlockchainConnectionString);
            var result = await repo.SearchHash(text);
            switch (result.ResultType) {
                case SearchResultType.Block:
                    return Redirect("/Block/" + result.Key);                    
                case SearchResultType.Transaction:
                    return Redirect("/TXN/" + result.Key);
            }
            return Redirect("/Address/" + text);
        }
    }
}