// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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

		public async Task<ActionResult> Block(string id) {
			if (string.IsNullOrWhiteSpace(id))
				return View("Index");

			// Load by block height
			if (long.TryParse(id, out var height)) {
				if (height < 0) {
					AddPageMessage("Invalid block height '{0}'".FormatWith(height), "Block not found", PageMessageSeverity.Error);
					return View("Index");
				}

				var repo = DatabaseManager.GetBlockchainRepository();
				try {
					var block = await repo.GetBlockByHeight(height);
					return View(block);
				} catch (NoSingleRecordException) {
					AddPageMessage("No block with height {0} is known to this node".FormatWith(height), "Block not found", PageMessageSeverity.Error);
					return View("Index");
				}
			}

			// Load by block hash
			if (BitcoinProtocolHelper.IsValidHashString(id)) {
				var repo = DatabaseManager.GetBlockchainRepository();
				var block = await repo.GetBlock(id);
				return View(block);
			}
			
			AddPageMessage("Invalid block hash '{0}'".FormatWith(id), "Block not found", PageMessageSeverity.Error);
			return View("Index");

		}

		public async Task<ActionResult> Transaction(string id) {
			if (string.IsNullOrWhiteSpace(id))
				return View("Index");

			if (!BitcoinProtocolHelper.IsValidHashString(id)) {
				AddPageMessage("Transaction identifier (TXID) `{0}` was invalidly formatted".FormatWith(id),
					"Transaction not found",
					PageMessageSeverity.Error);
				return View("Index");
			}

			try {
				var repo = DatabaseManager.GetBlockchainRepository();
				var txn = await repo.GetTransaction(id);
				return View(txn);
			} catch (NoSingleRecordException) {
				AddPageMessage("No transaction with TXID `{0}` was found".FormatWith(id), "Block not found", PageMessageSeverity.Error);
				return View("Index");
			}
		
		}

		public async Task<ActionResult> Address(string id, int? page) {
			if (string.IsNullOrWhiteSpace(id))
				return View("Index");

			if (!BitcoinProtocolHelper.IsValidAddress(id)) {
				AddPageMessage($"Invalid address '{id}'", "Address not found", PageMessageSeverity.Error);
				return View("Index");
			}

			var repo = DatabaseManager.GetBlockchainRepository();
			var items = await repo.GetStatementLines(id);

			var model = ConstructModel(id, items);
			return View(model);

		}

		public async Task<ActionResult> TransactionInputScripts(long key) {
			var repo = DatabaseManager.GetBlockchainRepository();

			var txin = await repo.GetTransactionInputById(key);

			var model = new TransactionInputScriptModel();
			
			if (txin.Script is not null)
				model.ScriptSig = await repo.GetScriptSummary(txin.Script.ID);

			if (txin.WitScript is not null)
				model.Witness = await repo.GetScriptSummary(txin.WitScript.ID);
			
			return View(model);
		}
		
		public async Task<ActionResult> Script(long key) {
			var repo = DatabaseManager.GetBlockchainRepository();
			var summary = await repo.GetScriptSummary(key);
			return View(summary);
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
