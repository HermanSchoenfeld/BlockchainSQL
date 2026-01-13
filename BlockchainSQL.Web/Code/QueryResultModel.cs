// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Data;

namespace BlockchainSQL.Web.Code {
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