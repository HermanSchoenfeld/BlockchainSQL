// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Exceptions;
using NHibernate.Mapping.ByCode;
using Sphere10.Framework;


namespace BlockchainSQL.DataAccess.NHibernate {
    public class DemoDataGenerator : PrimingDataGenerator {

        public DemoDataGenerator(
            ISessionFactory sessionFactory,
            string databaseName)
            : base(sessionFactory, databaseName) {
        }

        protected override IEnumerable<object> CreateNonPrimingData() {
            return base.CreateNonPrimingData();
        }
    }

}