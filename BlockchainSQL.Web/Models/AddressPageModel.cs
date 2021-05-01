using System;
using System.Collections.Generic;

namespace BlockchainSQL.Web.Models
{
    public class AddressPageModel {

        public string Address { get; set; }
        public IList<LineItem> LineItems { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal Balance { get; set; }

        public class LineItem {
            public DateTime DateTime { get; set; }

            public string TXID { get; set; }

            public int Index { get; set; }

            public LineItemType ItemType { get; set; }
            public decimal AmountBTC { get; set; }
            public decimal TotalBTC { get; set; }
        }

        public enum LineItemType {
            Debit,
            Credit
        }


        public static AddressPageModel EmptyFor(string address) {
            return new AddressPageModel {
                Address = address,
                LineItems = new LineItem[0],
                TotalCredits = 0M,
                TotalDebits = 0M,
                Balance = 0M
            };
        }
    }
}