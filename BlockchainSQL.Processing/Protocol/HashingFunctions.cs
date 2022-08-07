using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hydrogen;

namespace BlockchainSQL.Processing.Domain {

	/// <remarks>
	/// Written as static methods for performance (heavy-duty use)
	/// </remarks>
	public static class HashingFunctions {
        public static byte[] SHA256(IEnumerable<byte> source) {
            var hasher =  new SHA256Managed();
            var sourceArr = source as byte[] ?? source.ToArray();
            return hasher.ComputeHash(sourceArr);
        }

        public static byte[] RIPEMD160(IEnumerable<byte> source) {
            var sourceArr = source as byte[] ?? source.ToArray();
            return NBitcoin.Crypto.Hashes.RIPEMD160(sourceArr);            
        }

        public static byte[] Hash160(byte[] source) {
            throw new NotImplementedException();
        }


        public static byte[] ComputeBlockHeaderHash(int version, string prevBlockHash, byte[] merkleRoot, uint unixTime, uint bits, uint nonce) {
            var prevBlockHashBytes = Encoding.ASCII.GetBytes(prevBlockHash);
            Array.Reverse(prevBlockHashBytes);
            return ComputeBlockHeaderHash(version, prevBlockHashBytes, merkleRoot, unixTime, bits, nonce);
        }

        /// <summary>
        /// Computes the block hash using fields from the block heaer.
        /// </summary>
        /// <param name="version">Block version number</param>
        /// <param name="prevBlockHash">Previous block hash bytes (internal byte order)</param>
        /// <param name="merkleRoot">The merkle-root bytes</param>
        /// <param name="unixTime">The unix time field</param>
        /// <param name="bits">The bits field</param>
        /// <param name="nonce">The nonce field</param>
        /// <returns>Block hash bytes (little-endian)</returns>
        public static byte[] ComputeBlockHeaderHash(int version, byte[] prevBlockHash, byte[] merkleRoot, uint unixTime, uint bits, uint nonce) {
            // source: https://en.bitcoin.it/wiki/Block_hashing_algorithm
            var versionBytes = EndianBitConverter.Little.GetBytes(version);
            var unixTimeBytes = EndianBitConverter.Little.GetBytes(unixTime);
            var bitsBytes = EndianBitConverter.Little.GetBytes(bits);
            var nonceBytes = EndianBitConverter.Little.GetBytes(nonce);
            var headerBytes = versionBytes.Concat(prevBlockHash).Concat(merkleRoot).Concat(unixTimeBytes).Concat(bitsBytes).Concat(nonceBytes);
            return ComputeBlockHeaderHash(headerBytes);
        }

        /// <summary>
        /// Computes the block hash using the raw block header bytes.
        /// </summary>
        /// <param name="rawBlockHeaderBytes">Block header bytes (little-endian)</param>
        /// <returns>Block hask bytes (little-endian)</returns>
        public static byte[] ComputeBlockHeaderHash(IEnumerable<byte> rawBlockHeaderBytes) {
            Debug.Assert(rawBlockHeaderBytes.Count() == 80);
            var bytes = SHA256(SHA256(rawBlockHeaderBytes));
            Array.Reverse(bytes);
            return bytes;
        }

        /// <summary>
        /// Computes the block hash using the raw block header bytes.
        /// </summary>
        /// <param name="rawBlockHeaderBytes">Block header bytes (little-endian)</param>
        /// <returns>String representation of block hash (RPC byte order)</returns>
        public static string BlockHeaderHashString(IEnumerable<byte> rawBlockHeaderBytes) {
            var hash = ComputeBlockHeaderHash(rawBlockHeaderBytes);
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }

        /// <summary>
        /// Computers the transaction hash (or the TXID) from the raw transaction bytes.
        /// </summary>
        /// <param name="rawTransactionBytes"></param>
        /// <returns></returns>
        public static byte[] ComputeTransactionHashInternalByteOrder(IEnumerable<byte> rawTransactionBytes) {
            return SHA256(SHA256(rawTransactionBytes));
        }

        public static byte[] ComputeTransactionHash(IEnumerable<byte> rawTransactionBytes) {
            var hash = ComputeTransactionHashInternalByteOrder(rawTransactionBytes);
            Array.Reverse(hash);
            return hash;
        }

        public static string ComputeTransactionHashString(IEnumerable<byte> rawTransactionBytes) {
            var hashBytes = ComputeTransactionHash(rawTransactionBytes);
            return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
        }
    }
}
