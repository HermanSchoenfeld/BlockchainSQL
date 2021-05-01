using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;

namespace BlockchainSQL.Web.Controllers
{
    public class ExplorerController : BaseController {
        public ActionResult Index() {
            return View();
        }

		public async Task<ActionResult> ViewBlockByHeight(int height) {
			var repo = new DBBlockchainRepository(base.Config.BlockchainConnectionString);
			if (height > await repo.GetBlockCount()) {
				base.AddPageMessage("No block with height {0} is known to this node".FormatWith(height), "Block not found", PageMessageSeverity.Error);
				return View("Index");
			}

			var block = await repo.GetBlockByHeight(height);
			return View("ViewBlock", block);
		}


		public async Task<ActionResult> ViewBlockByHash(string hash) {
            if (!BitcoinProtocolHelper.IsValidHashString(hash)) { 
				base.AddPageMessage("Invalid block hash '{0}'".FormatWith(hash), "Block not found", PageMessageSeverity.Error);
				return View("Index");

			}

			var repo = new DBBlockchainRepository(base.Config.BlockchainConnectionString);
            var block = await repo.GetBlock(hash);
            return View("ViewBlock", block);
        }

        public async Task<ActionResult> ViewTransaction(string txid) {
            if (!BitcoinProtocolHelper.IsValidHashString(txid))
                throw new Exception("Invalid TXID");
            var repo = new DBBlockchainRepository(base.Config.BlockchainConnectionString);
            var txn = await repo.GetTransaction(txid);
            return View(txn);
        }


        public async Task<ActionResult> ViewScript(string txid, TransactionItemType txItemType) {
            var key = int.Parse(Request["key"].ToString());
            var repo = new DBBlockchainRepository(base.Config.BlockchainConnectionString);
            var summary = await repo.GetScriptSummary(txid, (int)key, txItemType);
            return View(summary);
        }
    }
}