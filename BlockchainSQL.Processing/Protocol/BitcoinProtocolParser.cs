using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing.Domain;
using NBitcoin;
using Sphere10.Framework;
using Block = BlockchainSQL.DataObjects.Block;
using Script = BlockchainSQL.DataObjects.Script;
using ScriptType = BlockchainSQL.DataObjects.ScriptType;
using Transaction = BlockchainSQL.DataObjects.Transaction;

namespace BlockchainSQL.Processing {

	/// <remarks>
	/// Current this parses the network protocol binary data into the domain model of BlockchainSQL.
	/// This should be refactored to parse into an object model that mirrors the actual protocol, which can then
	/// be converted into the domain model of BlockchainSQL.
	/// </remarks>
#warning Proper checks need to be made before reading.
	public class BitcoinProtocolParser {
		public static ulong ParseCompactVarInt(EndianBinaryReader reader) {
			ulong n = 0;
			while (true) {
				byte chData = reader.ReadByte();
				ulong a = (n << 7);
				byte b = (byte)(chData & 0x7F);
				n = (a | b);
				if ((chData & 0x80) != 0)
					n++;
				else
					break;
			}
			return n;
		}

		public static long ParseVarInt(EndianBinaryReader reader) {
			var t = reader.ReadByte();
			if (t < 0xfd) return t;
			if (t == 0xfd) return reader.ReadInt16();
			if (t == 0xfe) return reader.ReadInt32();
			if (t == 0xff) return reader.ReadInt64();

			throw new InvalidDataException("Reading Var Int");
		}

		public static byte[] ParseStringAsBytes(EndianBinaryReader reader) {
			var scriptLength = ParseVarInt(reader);
			return reader.ReadBytes((int)scriptLength);
		}

		public static byte[] ParseHashBytesInternalByteOrder(EndianBinaryReader reader) {
			var bytes = reader.ReadBytes(32);
			return bytes;
		}

		public static byte[] ParseMerkleRoot(EndianBinaryReader reader) {
			return reader.ReadBytes(32);
		}

		public static byte[] ParseHashBytes(EndianBinaryReader reader) {
			var hashBytes = ParseHashBytesInternalByteOrder(reader);
			Array.Reverse(hashBytes);
			return hashBytes;
		}

		public static string ParseHashString(EndianBinaryReader reader) {
			var hashBytes = ParseHashBytesInternalByteOrder(reader);
			return BitcoinProtocolHelper.ConvertToFriendlyString(hashBytes);
		}

		public static string ParseString(EndianBinaryReader reader) {
			return Encoding.ASCII.GetString(ParseStringAsBytes(reader));
		}

		public static Block ParseBlock(EndianBinaryReader reader, bool parseTransaction, bool expandScripts, bool readMagicID = true,
		                               bool readSize = true, uint? blockSize = null) {
			var profiler = reader.BaseStream as StreamProfiler;
			if (profiler == null)
				throw new Exception(
					"Underlying stream is not a StreamProfiler. Please wrap the underlying Stream using a StreamProfiler");
			if (parseTransaction && (!readSize && blockSize == null))
				throw new BlockchainSQLException(
					"Cannot parse transactions unless size is being read from stream or explicitly passed as an argument.");

			var readStart = reader.BaseStream.Position;
			var block = new Block();
			if (readMagicID) {
				var magicID = reader.ReadUInt32();
				if (magicID != BitcoinProtocolHelper.MagicID_Main)
					throw new BlockchainSQLException("Block did not have expected  Magic ID '{0}'", BitcoinProtocolHelper.MagicID_Main);
			}
			if (readSize)
				block.Size = reader.ReadUInt32();
			else if (blockSize != null)
				block.Size = blockSize.Value;

			profiler.StartListening();
			block.Version = reader.ReadInt32();
			block.PreviousBlockHash = ParseHashBytes(reader);
			block.MerkleRoot = ParseMerkleRoot(reader);
			block.TimeStampUnix = reader.ReadUInt32();
			block.TimeStampUtc = new DateTime(1970, 1, 1).AddSeconds(block.TimeStampUnix);
			block.Bits = reader.ReadUInt32();
			block.Nonce = reader.ReadUInt32();
			var rawBlockHeaderBytes = profiler.StopListening();
			block.Hash = HashingFunctions.ComputeBlockHeaderHash(rawBlockHeaderBytes);
			block.TransactionCount = (uint)ParseVarInt(reader);
			if (parseTransaction) {
				for (var i = 0; i < block.TransactionCount; i++) {
					var txn = ParseTransaction(reader, false);

					if (i == 0) {
						// Make sure contains coinbase transaction

						if (txn.Inputs.Count == 0 || txn.Outputs.Count == 0) {
							throw new BlockchainSQLException("Malformed coinbase transaction ({0}) in Block {1}", txn.TXID, block.Hash);
						}

						// Set the coinbase input amount to match the sum of it's outputs (simplify accounting). This amount includes the block reward and all fee's. The block
						// reward is calculated separately in the processing phase.
						var generatedBTC = txn.Outputs.Sum(o => o.Value);
						txn.Inputs[0].Value = generatedBTC; // coinbase input value is also set to generatedbtc (makes accounting easier)

					}
					block.AddTransaction(txn);
				}
			} else {
				var readEnd = reader.BaseStream.Position;
				reader.Seek((int)(block.Size - (readEnd - readStart) + 8), SeekOrigin.Current);
			}

			// expand all script's into domain objects
			if (expandScripts) {
				ProcessingTierHelper.ExpandBlockScripts(new[] { block });
			}
			return block;
		}

		public static Transaction ParseTransaction(EndianBinaryReader reader, bool expandScriptBytes) {
			var profiler = reader.BaseStream as StreamProfiler;
			if (profiler == null)
				throw new Exception(
					"Underlying stream is not a StreamProfiler. Please wrap the underlying Stream using a StreamProfiler");

			ByteArrayBuilder txidBuilder = new ByteArrayBuilder();
			
			//full transaction profile for size calculation
			profiler.StartListening();
			
			var transaction = new Transaction();
			transaction.Version = reader.ReadInt32();
			txidBuilder.Append(EndianBitConverter.Little.GetBytes(transaction.Version));
			
			profiler.StartListening();
			var marker = (uint)ParseVarInt(reader);
			var markerBytes = profiler.StopListening();
			byte flag;
			bool isSegWit = marker == 0;
			
			if (isSegWit) {
				flag = reader.ReadByte();
				Debug.Assert(flag > 0);

				profiler.StartListening();
				transaction.InputCount = (uint)ParseVarInt(reader);
				var inputCountBytes = profiler.StopListening();
				
				txidBuilder.Append(inputCountBytes);
			} else {
				transaction.InputCount = marker;
				txidBuilder.Append(markerBytes);
			}

			profiler.StartListening();
			for (var i = 0; i < transaction.InputCount; i++) {
				var input = ParseInput(reader);
				transaction.AddInput(input);
			}

			transaction.OutputCount = (uint)ParseVarInt(reader);
			for (var i = 0; i < transaction.OutputCount; i++) {
				var output = ParseOutput(reader);
				transaction.AddOutput(output);
			}

			var inOutBytes = profiler.StopListening();
			txidBuilder.Append(inOutBytes);

			if (isSegWit) {
				profiler.StartListening();
				for (int i = 0; i < transaction.InputCount; i++) {
					List<byte[]> itemStack = new List<byte[]>();
					var stackItems = (uint)ParseVarInt(reader);
					for (int j = 0; j < stackItems; j++) {
						var itemSize = (uint)ParseVarInt(reader);
						var item = reader.ReadBytes((int)itemSize);
						itemStack.Add(item);
					}

					transaction.Inputs[i].WitnessStackBytes = itemStack.ToArray();
				}

				
				transaction.Witness = profiler.StopListening();
			}

			if (expandScriptBytes)
				ExpandTransactionItemScript(transaction);

			transaction.LockTime = reader.ReadUInt32();
			byte[] lockTimeBytes = EndianBitConverter.Little.GetBytes(transaction.LockTime);
			txidBuilder.Append(lockTimeBytes);

			var txBytes = profiler.StopListening();
			transaction.Size = (uint)txBytes.Length;
			transaction.TXID = HashingFunctions.ComputeTransactionHash(txidBuilder.ToArray());
			transaction.WTXID = isSegWit
				? HashingFunctions.ComputeTransactionHash(txBytes)
				: transaction.TXID;

			return transaction;
		}

		public static TransactionInput ParseInput(EndianBinaryReader reader) {
			var input = new TransactionInput();
			input.Outpoint = ParseOutpoint(reader);
			input.Script = ParseScriptBytes(reader);
			input.Sequence = reader.ReadUInt32();
			return input;
		}

		public static Outpoint ParseOutpoint(EndianBinaryReader reader) {
			var outpoint = new Outpoint();
			outpoint.TXID = ParseHashBytes(reader);
			outpoint.OutputIndex = reader.ReadUInt32();
			return outpoint;
		}

		public static TransactionOutput ParseOutput(EndianBinaryReader reader) {
			var output = new TransactionOutput();
			output.Value = reader.ReadInt64();
			output.Script = ParseScriptBytes(reader);
			return output;
		}

		public static Script ParseScriptBytes(EndianBinaryReader reader) {
			var script = new Script();
			script.ScriptBytesLE = ParseStringAsBytes(reader);
			script.ScriptByteLength = script.ScriptBytesLE.Length;
			return script;
		}

		public static void ExpandTransactionItemScript(Transaction transaction) {

			foreach (var input in transaction.Inputs) {
				ExpandScript(input);

				if (input.WitnessStackBytes != null) 
					ExpandWitnessScript(input);
			}

			var txnClassifier = new TransactionClassifier();
			foreach (var output in transaction.Outputs) {
				ExpandScript(output);

				output.Script.ScriptClass = txnClassifier.ClassifyOutputScript(
					output.Script.Instructions.ToArray(),
					out var toAddressType,
					out var toAddress
				);
				output.ToAddressType = toAddressType;
				output.ToAddress = toAddress;
			}

			void ExpandScript(TransactionItem item) {
				var script = new NBitcoin.Script(item.Script.ScriptBytesLE);
				var nbOps = script.ToOps().ToArray();
				var instructions = nbOps.Select(ConvertToScriptInstruction).ToArray();
				item.Script.ScriptBytesLE = null;
				item.Script.AddInstructions(instructions);
				item.Script.ScriptType = DetermineScriptType(item);
			}

			void ExpandWitnessScript(TransactionInput input) {
				var script = new NBitcoin.WitScript(input.WitnessStackBytes);
				var ops = script.ToScript().ToOps();
				var instructions = ops.Select(ConvertToScriptInstruction).ToArray();

				input.WitScript = new Script {
					ScriptType = DetermineScriptType(input)
				};

				input.WitScript.AddInstructions(instructions);
			}
		}

		private static ScriptInstruction ConvertToScriptInstruction(Op nop) {
			return new ScriptInstruction {
				OpCode = (OpCode)(byte)nop.Code,
				Valid = !nop.IsInvalid,
				DataLE = nop.PushData
			};
		}

		private static ScriptType DetermineScriptType(TransactionItem transactionItem) {
			var result = ScriptType.Coinbase;

			TypeSwitch.Do(transactionItem,
				TypeSwitch.Case<TransactionInput>(input => {
						var isCoinbase = input.Index == 0 && input.Outpoint.TXID.All(c => c == '0');
						result = isCoinbase ? ScriptType.Coinbase : ScriptType.Unlock;
					}
				),
				TypeSwitch.Case<TransactionOutput>(output => result = ScriptType.Lock),
				TypeSwitch.Default(() => throw new NotSupportedException(transactionItem.GetType().FullName))
			);
			return result;
		}


	}
}
