// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.Web.Code;
using System.Data;

namespace BlockchainSQL.Web.Models {
	public class QueryPageModel {

        public QueryPageModel(string sql) {
            QueryPanelModel = new QueryPanelModel(sql);
            Schema = DatabaseManager.BlockchainSchema;
        }

        public QueryPanelModel QueryPanelModel { get; set; }

        public DataTable Schema { get; set; }


        public static QueryPageModel Empty {
            get { return new QueryPageModel(string.Empty); }
        }

	    public static QueryPageModel Default {
		    get {
			    const string defaultQuery =
				    @"-- Returns the duration between consequtive blocks in descending order
-- INTERESTING: when ordering by ascending, it can be seen future blocks were created BEFORE past blocks
SELECT 
	B1.Height AS FromBlock, 
	B2.Height AS ToBlock, 
	B1.TimeStampUtc AS FromTime,
	B2.TimeStampUtc AS ToTime,
	IIF(B2.TimeStampUTC < B1.TimeStampUTC, '-', '') +  RIGHT('00' + CONVERT(varchar, (DATEDIFF(SECOND, B1.TimeStampUTC, B2.TimeStampUTC) / 86400)), 2) + ':' + CONVERT(varchar, DATEADD(ss, DATEDIFF(SECOND, IIF(B1.TimeStampUTC < B2.TimeStampUTC, B1.TimeStampUTC, B2.TimeStampUTC), IIF(B2.TimeStampUTC > B1.TimeStampUTC, B2.TimeStampUTC, B1.TimeStampUTC)), 0), 108) AS Duration_DDHHMMSS,
	DATEDIFF(SECOND, B1.TimeStampUTC, B2.TimeStampUTC) as DurationSeconds
FROM 
	Block B1 INNER JOIN
	Block B2 ON B1.Height = B2.Height - 1
WHERE
	B1.BranchID = 1 AND  -- Ignore orphaned blocks
	B2.BranchID = 1      -- Ignore orphaned blocks
ORDER BY
	DurationSeconds DESC  -- Change between 'ASC' to 'DESC' to order differently
";
			    return new QueryPageModel(defaultQuery);
		    }
	    }
    }
}