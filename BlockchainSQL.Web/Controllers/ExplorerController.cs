using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Sphere10.Framework;
using Sphere10.Framework.Data.Exceptions;

namespace BlockchainSQL.Web.Controllers {

	public class ExplorerController : BaseController {
		public ActionResult Index() {
			return View();
		}

		public async Task<ActionResult> Block([FromQuery] int height, [FromQuery] string hash) {

			if (height > 0) {
				var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);

				try {
					var block = await repo.GetBlockByHeight(height);
					return View(block);
				} catch (NoSingleRecordException e) {
					AddPageMessage("No block with height {0} is known to this node".FormatWith(height),
						"Block not found",
						PageMessageSeverity.Error);
					return View("Index");
				}
			}

			if (!string.IsNullOrEmpty(hash)) {
				if (!BitcoinProtocolHelper.IsValidHashString(hash)) {
					AddPageMessage("Invalid block hash '{0}'".FormatWith(hash), "Block not found", PageMessageSeverity.Error);
					return View("Index");

				}

				var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
				var block = await repo.GetBlock(hash);
				return View(block);
			}

			return View("Index");
		}

		public async Task<ActionResult> Transaction(string txid) {
			if (!BitcoinProtocolHelper.IsValidHashString(txid))
				throw new Exception("Invalid TXID");
			var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
			var txn = await repo.GetTransaction(txid);

			return View(txn);
		}

		public async Task<ActionResult> TransactionInputScripts(int key) {
			var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);

			var txin = await repo.GetTransactionInputById(key);

			var model = new TransactionInputScriptModel();
			
			if (txin.Script is not null)
				model.ScriptSig = await repo.GetScriptSummary(txin.Script.ID);

			if (txin.WitScript is not null)
				model.Witness = await repo.GetScriptSummary(txin.WitScript.ID);
			
			return View(model);
		}
		
		public async Task<ActionResult> Script(int key) {
			var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
			var summary = await repo.GetScriptSummary(key);
			return View(summary);
		}

		public async Task<ActionResult> Address([FromQuery] string address) {
			if (!BitcoinProtocolHelper.IsValidAddress(address))
				return HomePageRedirect();

			var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
			var items = await repo.GetStatementLines(address);

			// If address was not a P2PKH address and it's empty, just re-direct (only show 0 balance for p2pkh addresses)
			if (!items.Any() && !BitcoinProtocolHelper.IsValidP2PKHAddress(address, true))
				return HomePageRedirect();

			var model = ConstructModel(address, items);
			return View(model);
			
		}

		private static AddressPageModel ConstructModel(string address, IEnumerable<StatementLine> lines) {
			if (!lines.Any()) {
				return AddressPageModel.EmptyFor(address);
			}

			var model = new AddressPageModel();
			model.Address = address;
			model.Balance = model.TotalCredits = model.TotalDebits = 0M;
			model.LineItems = lines.Select(ConstructLineModel).ToArray();
			model.LineItems.Aggregate(0M,
				(s, li) => {
					switch (li.ItemType) {
						case AddressPageModel.LineItemType.Debit:
							li.TotalBTC = s - li.AmountBTC;
							model.TotalDebits += li.AmountBTC;
							break;
						case AddressPageModel.LineItemType.Credit:
							li.TotalBTC = s + li.AmountBTC;
							model.TotalCredits += li.AmountBTC;
							break;
						default:
							throw new NotSupportedException(li.ItemType.ToString());
					}
					model.Balance = model.TotalCredits - model.TotalDebits;
					return li.TotalBTC;
				});
			return model;
		}

		private static AddressPageModel.LineItem ConstructLineModel(StatementLine line) {
			if (!line.TXType.IsIn("C", "D"))
				throw new ArgumentException("Line TXType must be 'C' or 'D'", nameof(line));
			return new AddressPageModel.LineItem {
				DateTime = line.TXDate,
				TXID = BitcoinProtocolHelper.BytesToString(line.TXID),
				Index = (int)line.TXID_IX,
				AmountBTC = line.TXAmount,
				ItemType = line.TXType == "C" ? AddressPageModel.LineItemType.Credit : AddressPageModel.LineItemType.Debit,
				TotalBTC = -1
			};
		}
	}
}
