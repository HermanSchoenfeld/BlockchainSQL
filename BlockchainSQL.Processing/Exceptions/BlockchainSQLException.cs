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
