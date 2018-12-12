using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainSQL.Web.Models {
    public class FunLink {
        public FunLinkType LinkType { get; set; }
        public string Name { get; set; }

        public string Badge { get; set; }
        public string Link { get; set; }

        
    }
}