using System;
using System.Net;

namespace BlockchainSQL.Processing
{
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
