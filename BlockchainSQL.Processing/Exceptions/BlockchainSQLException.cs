// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
	public class BlockchainSQLException : SoftwareException {
        public BlockchainSQLException() : base() {
        }

        public BlockchainSQLException(string error)
            : base(error) {
        }

        public BlockchainSQLException(string error, params object[] formatArgs)
            : base(error, formatArgs) {
        }

        public BlockchainSQLException(Exception innerException, string error, params object[] formatArgs)
            : base(innerException, error, formatArgs) {
        }

        public BlockchainSQLException(string error, Exception innerException)
            : base(error, innerException) {
        }
    }
}
