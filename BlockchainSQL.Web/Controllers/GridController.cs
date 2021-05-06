using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockchainSQL.Web.Code;
using Omu.AwesomeMvc;
using Sphere10.Framework;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainSQL.Web.Controllers
{
    public class GridController : BaseController {
        // GET: LatestBlocksGrid
        public async Task<ActionResult> Blocks(GridParams gridParams) {
            var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
            var model = await BuildGridModel(
                gridParams,
                repo.GetBlocks,
                repo.GetBlockCount,
                defaultSortOption: new SortOption("ID", SortDirection.Descending),
                map: JSONMappers.MapBlock,
                key: "ID"
            );
            return Json(model);
        }


        public async Task<ActionResult> BlockTransactions(string hash, GridParams gridParams) {
            //gridParams.Paging = false;
            var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
            var model = await BuildGridModel(
                gridParams,
                (page, pageSize, sortOptions) => repo.GetBlockTransactions(hash, page, pageSize, sortOptions),
                async () => (int)(await repo.GetBlock(hash)).TransactionCount,
                defaultSortOption: new SortOption("Index", SortDirection.Ascending),
                map: JSONMappers.MapTransaction
            );
            return Json(model);
        }


        public async Task<ActionResult> TransactionInputs(string txid, GridParams gridParams) {
            gridParams.Paging = false;
            var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
            var model = await BuildGridModel(
                gridParams,
                (page, pageSize, sortOptions) => repo.GetTransactionInputs(txid),
                defaultSortOption: new SortOption("Index", SortDirection.Ascending),
                map: JSONMappers.MapTransactionInput
            );
            gridParams.Paging = false;
            gridParams.Page = -1;
            return Json(model);
        }


        public async Task<ActionResult> TransactionOutputs(string txid, GridParams gridParams) {
            gridParams.Paging = false;
            var repo = new DBBlockchainRepository(AppConfig.BlockchainConnectionString);
            var model = await BuildGridModel(
                gridParams,
                (page, pageSize, sortOptions) => repo.GetTransactionOutputs(txid),
                defaultSortOption: new SortOption("Index", SortDirection.Ascending),
                map: JSONMappers.MapTransactionOutput
            );
            gridParams.Paging = false;
            gridParams.Page = -1;
            return Json(model);
        }

        private async Task<GridModelDto<T>> BuildGridModel<T>(
            GridParams gridParams,
            Func<int, int, SortOption[], Task< IEnumerable<T>>> itemFetchFunc,
            Func<Task<int>> itemCountFunc = null,
            SortOption defaultSortOption = null,
            Func<T, object> map = null,
            string key = null) {
            return await Task.Run(
                () => BuildGridModel(
                    gridParams, 
                    (x, y, z) => itemFetchFunc(x,y,z).ResultSafe(),
                    () => itemCountFunc().ResultSafe(),
                    defaultSortOption,
                    map,
                    key
                )
            );
        }

        private GridModelDto<T> BuildGridModel<T>(
            GridParams gridParams,
            Func<int, int, SortOption[], IEnumerable<T>> itemFetchFunc,
            Func<int> itemCountFunc = null,
            SortOption defaultSortOption = null,
            Func<T, object> map = null,
            string key = null) {
            const int defaultGridPageSize = 10;
            int page, pageSize;
            try {
                if (gridParams.Paging) {
                    page = gridParams.Page - 1;
                    pageSize = gridParams.PageSize.Value;
                } else {
                    page = 0;
                    pageSize = int.MaxValue;
                }
                var sortNames = gridParams.SortNames ?? new string[0];
                var sortDirs = gridParams.SortDirections ?? new string[0];
                var sortColumns = sortNames.Zip(sortDirs, (n, d) => new SortOption(n, ParseSortDirection(d))).ToArray();
                if (sortColumns.Length == 0 && defaultSortOption != null)
                    sortColumns = new[] {defaultSortOption};

                var results = itemFetchFunc(page, pageSize, sortColumns).ToArray();
                


                if (gridParams.Paging) {
                    gridParams.Page = 1; // repo already offset data in query
                } 
                var gridModelBuilder = new GridModelBuilder<T>(results.AsQueryable(), gridParams) {
                    Map = map,
                    Key = key
                };
                var gridModel = gridModelBuilder.BuildModel();

                // update grid model for client
                if (gridParams.Paging) {
                    gridModel.Page = page + 1;
                    var blockCount = itemCountFunc?.Invoke() ?? results.Length;
                    gridModel.PageCount = (int) Math.Ceiling((float) blockCount/pageSize);
                }
	            return gridModel.ToDto();
            } catch (Exception error) {
                var xxx = error.ToDiagnosticString();
                throw;
            }
        }

        private SortDirection ParseSortDirection(string mvcAwesomeSortDirectionString) {
            //  0 = none, 1 = asc, 2 = desc
            switch (mvcAwesomeSortDirectionString.ToUpperInvariant()) {
                case "ASC":
                    return SortDirection.Ascending;
                case "DESC":
                    return SortDirection.Descending;
                default:
                    return SortDirection.None;
            }
        }
    }
}