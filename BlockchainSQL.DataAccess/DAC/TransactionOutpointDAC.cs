// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC {
        public virtual TransactionOutput GetTransactionOutpoint(Outpoint outpoint) {
                const string query =
                    #region Query

 @"SELECT
	[TO].[ID] AS TO_ID,
	[TO].[Index] AS TO_Index,
	[TO].[Value] AS TO_Value,
    [TO].[ToAddressType] AS TO_ToAddressType,
    [TO].[ToAddress] AS TO_ToAddress,
	[TO].[TransactionID] AS TO_TransactionID,
	[TO].[ScriptID] AS TO_ScriptID,
	[TO].[RowState] AS TO_RowState,
	[T].[ID] AS T_ID,
	[T].[TXID] AS T_TXID,
	[T].[Version] AS T_Version,
	[T].[InputCount] AS T_InputCount,
	[T].[OutputCount] AS T_OutputCount,
    [T].[InputSum] AS T_InputSum,
    [T].[OutputSum] AS T_OutputSum,
    [T].[Fee] AS T_Fee,
	[T].[LockTime] AS T_LockTime,
	[T].[Index] AS T_Index,
	[T].[BlockID] AS T_BlockID,
    [T].[RowState] AS T_RowState,
	[S].[ID] AS S_ID,
	[S].[ScriptClass] AS S_ScriptClass,
	[S].[Result] AS S_Result,
	[S].[ScriptByteLength] AS S_ScriptByteLength,
	[S].[ScriptBytesLE AS S_ScriptBytes,
	[S].[InstructionCount] AS S_InstructionCount,
    [S].[RowState] AS S_RowState,
FROM
	[TransactionOutput] [TO] INNER JOIN
	[Transaction] [T] ON [TO].TransactionID = T.ID INNER JOIN
	[Script] [S] ON [TO].[ScriptID] = [S].[ID]
WHERE
	[T].[TXID] = '{0}' AND [TO].[Index] = {1}";

                #endregion

                var results = this.ExecuteQuery(query, outpoint.TXID, outpoint.OutputIndex)
                    .Rows
                    .Cast<DataRow>()
                    .Select(r => {
                        var to = Hydrators.HydrateTransactionOutput(r, "TO_");
                        var t = Hydrators.HydrateTransaction(r, "T_");
                        var s = Hydrators.HydrateScript(r, "S_");
                        to.Transaction = t;
                        to.Script = s;
                        return to;
                    });
                return results.SingleOrDefault();
#warning deal with special case dup TXID's here

        }
    }
}


