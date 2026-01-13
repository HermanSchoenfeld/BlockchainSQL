// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;
using Sphere10.Framework;
using System.Threading.Tasks;
using System.Transactions;
using BlockchainSQL.DataAccess.NHibernate.Mappings;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.DataAccess.NHibernate {
    public class PrimingDataGenerator : Sphere10.Framework.Data.NHibernate.NHDataGeneratorBase {
        protected readonly string DatabaseName;

        public PrimingDataGenerator(
            ISessionFactory sessionFactory,
            string databaseName
            ) : base(sessionFactory) {
            DatabaseName = databaseName;
        }

        protected override sealed IEnumerable<object> CreateData() {
            return
                Enumerable
                    .Empty<object>()
                    .Concat(CreateTypeTable<TableScriptType, ScriptType>())
                    .Concat(CreateTypeTable<TableScriptClass, ScriptClass>())
                    .Concat(CreateTypeTable<TableAddressType, AddressType>())
                    .Concat(CreateTypeTable<TableOpCode, OpCode>())
                    .Concat(CreateEnumTexts<ScriptType>())
                    .Concat(CreateEnumTexts<ScriptClass>())
                    .Concat(CreateEnumTexts<AddressType>())
                    .Concat(CreateEnumTexts<OpCode>())
                    .Concat(CreateDefaultBranches())
                    .Concat(CreateNonPrimingData());
        }

        protected virtual IEnumerable<Branch> CreateDefaultBranches() {
            return new[] {
                new Branch {
                    ID = (int)KnownBranches.Invalid,
                    ForkHeight = 0,
                    TimeStampUnix = 0,
                    TimeStampUtc = Tools.Time.FromUnixTime(0)
                },
                new Branch {
                    ID = (int)KnownBranches.MainChain,
                    ForkHeight = 0,
                    TimeStampUnix = 1231006505,
                    TimeStampUtc = Tools.Time.FromUnixTime(1231006505)
                }
            };
        }

        protected virtual IEnumerable<object> CreateNonPrimingData() {
            return Enumerable.Empty<object>();
        }

        protected virtual IEnumerable<Text> CreateEnumTexts<T>(bool isSystem = true) {
            var typeofT = typeof (T);
            return
                from object @enum in Enum.GetValues(typeofT) 
                where Tools.Enums.HasDescription((Enum)@enum)
                select new Text {
                    Name = typeofT.Name + "." + typeofT.GetEnumName(@enum),
                    en = Tools.Enums.GetDescription((Enum)@enum),
                    System = isSystem
                };
        }

        protected virtual IEnumerable<TypeTable> CreateTypeTable<TTypeTable, TEnum>() where TTypeTable : TypeTable, new() {
            var typeofEnum = typeof (TEnum);
            return
                from object @enum in Enum.GetValues(typeofEnum)
                select new TTypeTable {
                    ID =   (byte)Enum.Parse(typeof(TEnum), @enum.ToString()),
                    Name = Enum.GetName(typeofEnum, @enum),
                    Description = typeofEnum.Name + "." + typeofEnum.GetEnumName(@enum),
                };
        }
    }
}