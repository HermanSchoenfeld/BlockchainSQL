// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.Web.Models {
	public class QueryPanelModel {

        public QueryPanelModel(string sql) {
            SQL = sql;
        }
        public string SQL { get; set; }

        public static QueryPanelModel Empty {
            get { return new QueryPanelModel(string.Empty); }
        }

    }
}