using System.Threading.Tasks;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainSQL.Web.Controllers {
	public class SearchController : BaseController {

		public async Task<ActionResult> Index([FromQuery] string term) {
			if (string.IsNullOrWhiteSpace(term)) {
				return HomePageRedirect();
			}
			uint height;
			if (uint.TryParse(term, out height)) {
				return RedirectToAction("Block", "Explorer", new { height });
			}
			if (!(BitcoinProtocolHelper.IsValidHashString(term) || BitcoinProtocolHelper.IsValidAddress(term))) {
				return HomePageRedirect("Invalid search pattern");
			}
			var repo = DatabaseManager.GetBlockchainRepository();
			var result = await repo.SearchHash(term);
			switch (result.ResultType) {
				case SearchResultType.Block:
					return RedirectToAction("Block", "Explorer", new { hash = result.Key });
				case SearchResultType.Transaction:
					return RedirectToAction("Transaction", "Explorer", new { txid = result.Key });
			}
			return RedirectToAction("Address", "Explorer", new { address = term });
		}
	}
}
