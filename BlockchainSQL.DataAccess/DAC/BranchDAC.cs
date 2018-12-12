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

        public virtual Branch GetBranchByID(long branchID) {
           var branches = FindBranches(new[] {new ColumnValue("ID", branchID)});
            if (branches.Count() != 1)
                throw new NoSingleRecordException("Branch", branchID, branches.Count());
            return branches.Single();
        }

        public virtual void InsertBranch(Branch branch) {
            this.Insert(
                "Branch",
                new[] {
                    new ColumnValue("ID", branch.ID),
                    new ColumnValue("ForkHeight", branch.ForkHeight),
                    new ColumnValue("TimeStampUnix", branch.TimeStampUnix),
                    new ColumnValue("TimeStampUtc", branch.TimeStampUtc),
                    new ColumnValue("ParentBranchID", branch.ParentBranch != null ? (long?)branch.ParentBranch.ID : null), 
                    new ColumnValue("RowState", branch.RowState), 
                });
        }

        public virtual void DeleteBranch(long branchID) {
            this.Delete("Branch", new[] {new ColumnValue("ID", branchID)});
        }

        public virtual void MigrateBranch(long branchID, long fromHeight, long newBranchID) {
            this.ExecuteNonQuery("UPDATE Block SET BranchID = {0} WHERE BranchID = {1} AND Height >= {2}".FormatWith(newBranchID, branchID, fromHeight));
            this.ExecuteNonQuery("UPDATE Branch SET ParentBranchID = {0} WHERE ParentBranchID = {1} AND ForkHeight > {2}".FormatWith(newBranchID, branchID, fromHeight));
        }
        
        public virtual IEnumerable<Branch> FindBranches(ColumnValue[] columnMatches, string whereClause = null) {
            return (this.Select(
                "Branch",
                new [] {"ID", "ForkHeight", "TimeStampUnix", "TimeStampUtc", "ParentBranchID", "RowState"},
                columnMatches:columnMatches,
                whereClause:whereClause
            ))
            .Rows
            .Cast<DataRow>()
            .Select(Hydrators.HydrateBranch);
        }
    }
}


