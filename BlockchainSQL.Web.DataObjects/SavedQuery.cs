// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Web.DataObjects {
    public class SavedQuery {
        public virtual int ID { get; set; }

        public virtual string WebID { get; set; }

        public virtual SupportedDBMS DBMS { get; set; }

        public virtual DateTime DateTime { get; set; }

        public virtual string SQL { get; set; }

        public virtual string ContentHash { get; set; }

        public virtual string Result { get; set; }


    }
}
