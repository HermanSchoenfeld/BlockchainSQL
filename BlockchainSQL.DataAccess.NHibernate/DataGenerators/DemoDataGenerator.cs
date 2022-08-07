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
using Hydrogen;


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