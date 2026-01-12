// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC : DACDecorator {
        private readonly IDBVendorSpecificImplementation _vendorSpecificImplementation;
        internal ApplicationDAC(IDAC decoratedDAC, IDBVendorSpecificImplementation vendorSpecificImplementation) : base(decoratedDAC) {
            _vendorSpecificImplementation = vendorSpecificImplementation;
        }

        public bool HasDisabledApplicationIndexes() {
            return _vendorSpecificImplementation.HasDisabledApplicationIndexes(DecoratedDAC);
        }

        public void EnableAllApplicationIndexes() {
            _vendorSpecificImplementation.EnableAllApplicationIndexes(DecoratedDAC);
        }

        public void DisableAllApplicationIndexes() {
             _vendorSpecificImplementation.DisableAllApplicationIndexes(DecoratedDAC);
        }

        public void CleanupDatabase() {
            _vendorSpecificImplementation.CleanupDatabase(DecoratedDAC);
        }

        public DataTable ExecuteUserSQL(string userSql, int page, int pageSize, string orderByHint, out int pageCount) {
            return _vendorSpecificImplementation.ExecuteUserSQL(DecoratedDAC, userSql, page, pageSize, orderByHint, out pageCount);
        }

        public IEnumerable<StatementLine> GetStatementLines(string address) {
            return _vendorSpecificImplementation.GetStatementLines(DecoratedDAC, address);
        }

        public bool IsValidSchema() => _vendorSpecificImplementation.IsValidSchema(DecoratedDAC);

    }
}
