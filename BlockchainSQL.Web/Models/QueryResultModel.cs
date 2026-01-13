// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using BlockchainSQL.Web.Code;

namespace BlockchainSQL.Web.Models {
	public class QueryResultModel {

        public QueryResultModel() {
            Messages = new List<PageMessage>();
            Result = QueryResult.Empty;
        }

        public DateTime ExecutedOn { get; set; }

        public TimeSpan ExecutionDuration { get; set; }

        public QueryResult Result { get; set; }

        public List<PageMessage> Messages { get; set; }
        
    }
}