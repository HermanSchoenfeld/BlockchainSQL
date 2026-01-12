// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainSQL.Common;

namespace BlockchainSQL.Common.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod, TestCategory("Byte Array")]
        public void Should_Swap_Byte_Order_Test()
        {
            Byte[] data = new Byte[] { 0x00, 0x01, 0x02, 0x03 };
            Byte[] expected = new Byte[] { 0x03, 0x02, 0x01, 0x00 };
            Byte[] actual = data.SwapOrder();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Shoud()
        {
            String genisisBlockHash = "0x000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f";
            Byte[] actual = genisisBlockHash.StringToByteArray();
            Byte[] expected = new Byte[] { 0x00, 0x00, 0x00 };

            Assert.Inconclusive();
            //CollectionAssert.AreEqual(expected, actual);
        }
    }
}
