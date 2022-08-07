using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using NBitcoin.Protocol;
using Hydrogen;
using NB = NBitcoin;

namespace BlockchainSQL.Processing {
	public class NodeCommunicator : INodeCommunicator {
        public const int BitcoinPortNumber = 8333;
        private readonly NodeEndpoint nodeEndpoint;

        public NodeCommunicator(NodeEndpoint endpoint) {
            nodeEndpoint = endpoint;
        }



        #region INodeCommunicator Implementation

        public static async Task<Result> TestConnection(NodeEndpoint endPoint) {
            if (endPoint == null)
                throw new ArgumentNullException("endPoint");
            var result = Result.Default;
            try {
                using (await ConnectInternal(endPoint)) ;
            } catch (Exception error) {
                result.AddError("Failed to connect to endpoint '{0}:{1}'", endPoint.IP, endPoint.Port);
                result.AddError(error.ToDisplayString());
            }
            return result;
        }

        public async Task<long> GetPeerChainHeight() {
            if (nodeEndpoint == null)
                throw new ArgumentNullException("nodeEndpoint");

            using (var node = await ConnectInternal(nodeEndpoint)) {
                return node.PeerVersion.StartHeight;
            }
        }

        public async Task<FetchResult> FetchNextBlocks(BlockLocators blockLocators, int batchSize, bool skipTransactions) {
            if (nodeEndpoint == null)
                throw new ArgumentNullException("nodeEndpoint");

            using (var node = await ConnectInternal(nodeEndpoint)) {
                NB.uint256[] nextBlocks;
                using (var listener = node.CreateListener().OfType<InvPayload>()) {
                    var payload = new GetBlocksPayload { 
                        BlockLocators = new NB.BlockLocator {
                            Blocks =
                                blockLocators
                                    .Locations
                                    .Select(l => l.Hash)
                                    .Select(BitcoinProtocolHelper.ConvertHashToInternalOrderBytes)
                                    .Select(ByteArrayToUInt256).ToList()
                        }
                    };
                    await node.SendMessageAsync(payload);
                    nextBlocks = await Task.Run(() => listener.ReceivePayload<InvPayload>().Inventory.Select(i => i.Hash).ToArray());
                }

                if (nextBlocks.Length == 0) {
                    return new FetchResult {
                        Blocks = new Block[0],
                        NodeHeight = node.PeerVersion.StartHeight
                    };
                }
				// special case: also fetch genesis block (nodes seem to assume you have this)
	            if (blockLocators == BlockLocators.Empty) {
					nextBlocks =Tools.Array.Concat<NB.uint256>(
						new[] { new NB.uint256(BitcoinProtocolHelper.ConvertHashToInternalOrderBytes(BitcoinProtocolHelper.GenesisHash)) },
						nextBlocks);
	            }

                var nbBlocks = await Task.Run(() =>
                    node.GetBlocks(
                        nextBlocks
                            .Take(batchSize)
                            .Select(h => new NB.uint256(h))
                        )
                        .InLinkedListOrder(b => b.Header.GetHash(), b => b.Header.HashPrevBlock)
                        .Reverse()
                        .ToArray()
                    );

                return new FetchResult {
                    Blocks = nbBlocks.Select(b => Convert(skipTransactions, b)).ToArray(),
                    NodeHeight = node.PeerVersion.StartHeight
                };
            }
        }

        #endregion

        private static async Task<NB.Protocol.Node> ConnectInternal(NodeEndpoint nodeEndPoint) {
            System.Diagnostics.Trace.TraceInformation("ConnectInternal");
            System.Diagnostics.Trace.Flush();
            var nodeParams = await DefaultNodeParams();
            var node = await Task.Run(() => NBitcoin.Protocol.Node.Connect(NB.Network.Main, new NB.Protocol.NetworkAddress(new IPEndPoint(nodeEndPoint.IP, nodeEndPoint.Port)), nodeParams));
            try {
                await Task.Run(() => node.VersionHandshake());
            } catch (Exception error) {
                Tools.Exceptions.ExecuteIgnoringException(node.Disconnect);
                Tools.Exceptions.ExecuteIgnoringException(node.Dispose);
                throw;

            }
            return node;
        }

        private static async Task<NodeConnectionParameters> DefaultNodeParams() {
            var nodeParams= new NB.Protocol.NodeConnectionParameters {
                AddressFrom = new IPEndPoint( await Task.Run( () => Tools.Network.GetNetworkAddress()), 0),
                Advertize = false,
                IsRelay = false,
                Services = NB.Protocol.NodeServices.Nothing,
                //ReuseBuffer = true,
                Version = NB.Network.Main.MaxP2PVersion,
                ConnectCancellation = new CancellationToken(),
                //ReceiveBufferSize = 5000000,
                //SendBufferSize = 1000000,
                UserAgent = NB.Protocol.VersionPayload.GetNBitcoinUserAgent(),
            };
            nodeParams.TemplateBehaviors.Add(new NB.Protocol.Behaviors.PingPongBehavior());
            return nodeParams;
        }

        private static NB.uint256 ByteArrayToUInt256(byte[] byteArr) {
            var missingZero = 32 - byteArr.Length;
			if(missingZero < 0)
				throw new InvalidOperationException("Awful bug, this should never happen");
			if(missingZero != 0)
			{
                byteArr = byteArr.Concat(new byte[missingZero]).ToArray();
			}
            return new NB.uint256(byteArr);
        }

        private Block Convert(bool skipTransactions, NB.Block nbBlock) {
            var rawBytes = NB.BitcoinSerializableExtensions.ToBytes(nbBlock);
            using (var ms = new MemoryStream(rawBytes))
            using (var stream = new StreamProfiler(ms, true))
            using (var reader = new EndianBinaryReader(new LittleEndianBitConverter(), stream)) {
                var block = BitcoinProtocolParser.ParseBlock(reader, !skipTransactions, false, false, false, blockSize: (uint)rawBytes.Length);
                return block;
            }
        }
    }
}
