// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {

	public interface ITransactionClassifier {

        ScriptClass ClassifyInputScript(ScriptInstruction[] inputScriptInstructions, ScriptType scriptType, bool hasWitness, out AddressType addressType);

        ScriptClass ClassifyOutputScript(ScriptInstruction[] outputScriptInstructions, out AddressType addressType, out string address);

        ScriptClass ClassifyWitnessScript(ScriptInstruction[] instructions);
    }
}
