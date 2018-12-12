using System;

namespace BlockchainSQL.DataObjects {

    public struct AddressReference {

        public AddressType? AddressType { get; set; }

        public string Address { get; set; }
    }
}
