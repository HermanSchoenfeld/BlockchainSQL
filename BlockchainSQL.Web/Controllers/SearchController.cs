// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
				return Redirect($"/explorer/block/{height}");
			}
			if (!(BitcoinProtocolHelper.IsValidHashString(term) || BitcoinProtocolHelper.IsValidAddress(term))) {
				return HomePageRedirect("Invalid search pattern");
			}
			var repo = DatabaseManager.GetBlockchainRepository();
			var result = await repo.SearchHash(term);
			return result.ResultType switch {
				SearchResultType.Block => Redirect($"/explorer/block/{result.Key}"),
				SearchResultType.Transaction => Redirect($"/explorer/transaction/{result.Key}"),
				_ => Redirect($"/explorer/address/{term}")
			};
		}
	}
}
