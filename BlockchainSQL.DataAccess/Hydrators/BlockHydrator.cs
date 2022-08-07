using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen.Data;

namespace BlockchainSQL.DataAccess {
    public static partial class Hydrators {

        public static Block HydrateBlock(DataRow sourceRow) {
            return HydrateBlock(sourceRow, "");
        }
        public static Block HydrateBlock(DataRow sourceRow, string colPreFix) {
            var entity = new Block();
            HydrateBlock(sourceRow, entity, colPreFix);
            return entity;
        }

        public static void HydrateBlock(DataRow sourceRow, Block entity, string colPreFix = "") {
            entity.ID = sourceRow.Get<int>(colPreFix + "ID");
            entity.Height = sourceRow.Get<int>(colPreFix + "Height");
            entity.PreviousBlockHash = sourceRow.Get<byte[]>(colPreFix + "PreviousBlockHash");
            entity.Hash = sourceRow.Get<byte[]>(colPreFix + "Hash");
            entity.Branch = new Branch { ID = sourceRow.Get<int>(colPreFix + "BranchID") };
            entity.Size = sourceRow.Get<uint>(colPreFix + "Size");
            entity.Nonce = sourceRow.Get<uint>(colPreFix + "Nonce");
            entity.TimeStampUnix = sourceRow.Get<uint>(colPreFix + "TimeStampUnix");
            entity.TimeStampUtc = sourceRow.Get<DateTime>(colPreFix + "TimeStampUtc");
            entity.MerkleRoot = sourceRow.Get<byte[]>(colPreFix + "MerkleRoot");
            entity.Bits = sourceRow.Get<uint>(colPreFix + "Bits");
            entity.Difficulty = sourceRow.Get<float>(colPreFix + "Difficulty");
            entity.Version = sourceRow.Get<int>(colPreFix + "Version");
            entity.TransactionCount = sourceRow.Get<uint>(colPreFix + "TransactionCount");
            entity.OutputsBTC = sourceRow.Get<decimal?>(colPreFix + "OutputsBTC");
            entity.RewardBTC = sourceRow.Get<decimal?>(colPreFix + "RewardBTC");
            entity.FeesBTC = sourceRow.Get<decimal?>(colPreFix + "FeesBTC");
            entity.RowState = sourceRow.Get<byte>(colPreFix + "RowState");
        }
    }
}