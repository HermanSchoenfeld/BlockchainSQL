using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataAccess {
    public enum DatabaseIndex {

        /// <summary>
        /// This is needed to stop dead-locks caused by cascade-deletes on Branch (e.g. a delete of empty branch requires full tablescan of block).
        /// REMOVED: no foreign key at all now between block-branch
        /// </summary>
        //[Description("IX_BLOCK_BRANCH")]
        //Block_Branch,


        [Description("IX_BLOCK_BLOCKHASH")]
        Block_BlockHash,

        [Description("IX_TRANSACTION_TXID")]
        Transaction_TXID,

        [Description("IX_TRANSACTION_BLOCK")]
        Transaction_BlockID,

        [Description("IX_TRANSACTIONINPUT_SUBJECTADDRESS")]
        TransactionInput_Address,

        //[Description("IX_TRANSACTIONINPUT_OUTPOINT")]
        //TransactionInput_Outpoint,

        [Description("IX_TRANSACTIONINPUT_OUTPUT")]
        TransactionInput_Output,

        [Description("IX_INPUT_TRANSACTION")]
        TransactionInput_TransactionID,

        [Description("IX_TRANSACTIONOUTPUT_SUBJECTADDRESS")]
        TransactionOutput_Address,

        [Description("IX_OUTPUT_TRANSACTION")]
        TransactionOutput_TransactionID,

        [Description("IX_SCRIPTINSTRUCTION_SCRIPT")]
        ScriptInstruction_ScriptID,

    }
}
