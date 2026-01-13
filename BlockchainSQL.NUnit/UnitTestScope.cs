// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.NUnit {
    public class UnitTestScope : IDisposable {
        private readonly BizLogicScope _bizScope;
        private readonly DACScope _dacScope;
        private int _genCount;
        public UnitTestScope(DBMSType dbmsType, string connectionString) {     
            _bizScope = new BizLogicScope(dbmsType, connectionString, new ConsoleLogger() { Options = LogOptions.StandardProfile});
            _dacScope = _bizScope.DAC.BeginScope();
            _dacScope.BeginTransaction();
            _genCount = 0;
            DAC = _bizScope.DAC;
        }

        public ApplicationDAC DAC { get; private set; }

        public WipBlock GenBlock(string hash, string prevHash, uint unixTime) {
			if (hash.All(c => c == '0'))
				throw new ArgumentException("Hash cannot be all zero's as it's reserved", nameof(hash));
            MockPaymentBuilder payments = new MockPaymentBuilder();

            var total = Tools.Maths.RNG.Next(1, 10);
            for (var i = 0; i < total; i++) {
                payments.Pay(null, Tools.Maths.RNG.Next().ToString(), Tools.Maths.RNG.Next());
            }
            return GenBlock(hash, prevHash, unixTime, payments.History.ToArray());
        }


        public WipBlock GenBlock(string hash, string prevHash, uint unixTime, params Transaction[] transactions) {
            // Generate mock payments and cal new GenBlock
            var block = new Block {
                ID = _genCount++,
                Branch = null,
                MerkleRoot = new byte[32],
                Nonce = 0,
                TimeStampUnix = unixTime,
                TimeStampUtc = Tools.Time.FromUnixTime(unixTime),
                Size = 0,
                TransactionCount = 0,
                Version = 1,
                Bits = 0,
                Difficulty = 0,
                Hash = TestHelper.ToHashBytes(hash),
                RewardBTC = 0,
                PreviousBlockHash = prevHash != null ? TestHelper.ToHashBytes(prevHash) : BitcoinProtocolHelper.EmptyHash
            };
            block.AddTransactions(transactions);
            return new WipBlock(block, DateTime.MinValue, DateTime.MinValue, 0);
        }

        public void Dispose() {
            Tools.Exceptions.ExecuteIgnoringException(_dacScope.Rollback);
            Tools.Exceptions.ExecuteIgnoringException(_bizScope.Dispose);
        }
    }
}
