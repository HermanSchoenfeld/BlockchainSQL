using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {

    public interface ITransactionClassifier {

        ScriptClass ClassifyInputScript(ScriptInstruction[] inputScriptInstructions, ScriptType scriptType, out AddressType addressType);

        ScriptClass ClassifyOutputScript(ScriptInstruction[] outputScriptInstructions, out AddressType addressType, out string base58CheckAddress);

    }
}
