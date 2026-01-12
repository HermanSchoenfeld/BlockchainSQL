// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;

namespace BlockchainSQL.DataObjects {

    public class Block {

        public Block() {
            RowState = 1;
            Transactions = new List<Transaction>();
        }
        public virtual int ID { get; set; }
        public virtual int Height { get; set; }
        public virtual Branch Branch { get; set; }        
        public virtual uint Size { get; set; }       
        public virtual uint Nonce { get; set; }
        public virtual byte[] PreviousBlockHash { get; set; }

        private byte[] _hash;
        public virtual byte[] Hash {
            get { return _hash; }
            set {
                _hash = value;
                if (_hash != null && _hash[0] != 0) {
                    var x = 1;
                }
            }
        }

        public virtual float Difficulty { get; set; }
        public virtual uint TimeStampUnix { get; set; }
        public virtual DateTime TimeStampUtc { get; set; }
        public virtual byte[] MerkleRoot { get; set; }        
        public virtual uint Bits { get; set; }
        public virtual int Version { get; set; } // Note: this is signed!
        public virtual uint TransactionCount { get; set; }

        public virtual decimal? OutputsBTC { get; set; }

        public virtual decimal? RewardBTC { get; set; }
        public virtual decimal? FeesBTC { get; set; }
        
        public virtual IList<Transaction> Transactions { get; set; }
        public virtual byte RowState { get; set; }

        public virtual void AddTransaction(Transaction transaction) {
            AddTransactions(new[] { transaction});
        }

        public virtual void AddTransactions(IEnumerable<Transaction> transactions) {
            foreach (var transaction in transactions) {
                transaction.Index = (uint)Transactions.Count;
                Transactions.Add(transaction);
                transaction.Block = this;
            }
        }
    }
}
