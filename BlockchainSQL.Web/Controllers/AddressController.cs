using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.Models;
using NHibernate.Criterion;
using Sphere10.Framework;

namespace BlockchainSQL.Web.Controllers
{
    public class AddressController : BaseController
    {
        // GET: Address
        public async Task<ActionResult> Index(string address) {
            if (!BitcoinProtocolHelper.IsValidAddress(address))
                return HomePageRedirect();

            var repo = new DBBlockchainRepository(base.Config.BlockchainConnectionString);
            var items = await repo.GetStatementLines(address);

            // If address was not a P2PKH address and it's empty, just re-direct (only show 0 balance for p2pkh addresses)
            if (!items.Any() && !BitcoinProtocolHelper.IsValidP2PKHAddress(address, true))
                return HomePageRedirect();

            var model = ConstructModel(address, items);
            return View(model);
        }

        private static AddressPageModel ConstructModel(string address, IEnumerable<StatementLine> lines) {            
            if (!lines.Any()) {
                return AddressPageModel.EmptyFor(address);
            }

            var model = new AddressPageModel();
            model.Address = address;
            model.Balance = model.TotalCredits = model.TotalDebits = 0M;            
            model.LineItems = lines.Select(ConstructLineModel).ToArray();            
            model.LineItems.Aggregate(0M, (s, li) => {
                switch (li.ItemType) {
                    case AddressPageModel.LineItemType.Debit:
                        li.TotalBTC =  s - li.AmountBTC;
                        model.TotalDebits += li.AmountBTC;
                        break;
                    case AddressPageModel.LineItemType.Credit:
                        li.TotalBTC = s + li.AmountBTC;
                        model.TotalCredits += li.AmountBTC;
                        break;
                    default:
                        throw new NotSupportedException(li.ItemType.ToString());
                }
                model.Balance = model.TotalCredits - model.TotalDebits;
                return li.TotalBTC;
            });
            return model;
        }

        private static AddressPageModel.LineItem ConstructLineModel(StatementLine line) {
            if (!line.TXType.IsIn("C","D"))
                throw new ArgumentException("Line TXType must be 'C' or 'D'", nameof(line));
            return new AddressPageModel.LineItem {
                DateTime   =  line.TXDate,
                TXID = BitcoinProtocolHelper.BytesToString(line.TXID),
                Index =  (int)line.TXID_IX,
                AmountBTC = line.TXAmount,
                ItemType = line.TXType == "C" ? AddressPageModel.LineItemType.Credit : AddressPageModel.LineItemType.Debit,
                TotalBTC = -1
            };
        }
    }
}