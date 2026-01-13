// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Net;

namespace BlockchainSQL.Processing {
	public class NodeEndpoint {
        public const int DefaultBitcoinPort = 8333;
        private NodeEndpoint(IPAddress ip, int port) {
            IP = ip;
            Port = port;
        }

        public static NodeEndpoint For(string ipAddress, int? port = null) {
            IPAddress ip;
            if (!IPAddress.TryParse(ipAddress, out ip)) {
                throw new ArgumentException("Malformed IP address", "ipAddress");
            }
            return new NodeEndpoint(ip, port ?? DefaultBitcoinPort);
        }

        public static NodeEndpoint For(IPAddress ipAddress, int? port = null) {
            return new NodeEndpoint(ipAddress, port ?? DefaultBitcoinPort);
        }

        public static bool TryParse(string ipAddress, int? port, out NodeEndpoint nodeEndpoint) {
            nodeEndpoint = null;
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;
            IPAddress ip;
            if (IPAddress.TryParse(ipAddress, out ip)) {
                nodeEndpoint = NodeEndpoint.For(ip, port);
                return true;
            }
            return false;
        }
        public IPAddress IP { get; set; }

        public int Port { get; set; }
    }
}
