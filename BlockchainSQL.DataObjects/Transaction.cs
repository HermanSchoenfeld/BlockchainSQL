// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;

namespace BlockchainSQL.DataObjects
{
    public class Transaction {
        public Transaction() {
            RowState = 1;
        }
        public virtual long ID { get; set; }

        public virtual byte[] TXID { get; set; }
        
        public virtual byte[] WTXID { get; set; }

        public virtual uint Size { get; set; }

        public virtual int Version { get; set; }

        public virtual uint LockTime { get; set; }
        
        public virtual Block Block { get; set; }

        public virtual uint Index { get; set; }

        public virtual uint InputCount { get; set; }

        public virtual uint OutputCount { get; set; }

        public virtual decimal? InputsBTC { get; set; }
        
        public virtual decimal? OutputsBTC { get; set; }
        
        public virtual decimal? FeeBTC { get; set; }

        public virtual IList<TransactionInput> Inputs { get; set; }

        public virtual IList<TransactionOutput> Outputs { get; set; }
        
        public virtual byte[] Witness { get; set; }
        
        public virtual byte RowState { get; set; }
        
        public virtual void AddInput(TransactionInput input) {
            AddInputs(new[] { input });
        }

        public virtual void AddInputs(IEnumerable<TransactionInput> inputs) {
            if (Inputs == null)
                Inputs = new List<TransactionInput>();
            foreach (var input in inputs) {
                input.Index = (uint) Inputs.Count;
                Inputs.Add(input);
                input.Transaction = this;
            }
        }

        public virtual void AddOutput(TransactionOutput output) {
            AddOutputs(new[] { output });
        }

        public virtual void AddOutputs(IEnumerable<TransactionOutput> outputs) {
            if (Outputs == null)
                Outputs = new List<TransactionOutput>();
            foreach (var output in outputs) {
                output.Index =(uint)Outputs.Count;
                Outputs.Add(output);
                output.Transaction = this;
            }
        }
    }
}
