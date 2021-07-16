using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {

	public interface ITransactionClassifier {

        ScriptClass ClassifyInputScript(ScriptInstruction[] inputScriptInstructions, ScriptType scriptType, bool hasWitness, out AddressType addressType);

        ScriptClass ClassifyOutputScript(ScriptInstruction[] outputScriptInstructions, out AddressType addressType, out string address);

        ScriptClass ClassifyWitnessScript(ScriptInstruction[] instructions);
    }
}
