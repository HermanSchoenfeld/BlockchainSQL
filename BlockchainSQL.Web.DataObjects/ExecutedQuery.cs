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
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Web.DataObjects {
	public class ExecutedQuery {
		public virtual int ID { get; set; }
		public virtual string Query { get; set; }
		public virtual int PageNumber { get; set; }
		public virtual int PageSize { get; set; }
		public virtual string IP { get; set; }
		public virtual DateTime ExecutedOn { get; set; }
		public virtual int ExecutionDurationMS { get; set; }
	}
}
