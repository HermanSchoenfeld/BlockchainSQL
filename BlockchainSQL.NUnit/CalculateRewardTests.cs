using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using NUnit.Framework;
using Hydrogen;
using Hydrogen.Data;

namespace BlockchainSQL.NUnit {

    [TestFixture]
    public class CalculateRewardTests : BaseTestFixture {

        [Test]
        public void Calculate_GenesisReward() {
            Assert.AreEqual(50L * 100000000, BitcoinProtocolHelper.CalculateReward(0));
        }
        [Test]
        public void Calculate_100thBlockRward()
        {
            Assert.AreEqual(50L * 100000000, BitcoinProtocolHelper.CalculateReward(100));
        }

        [Test]
        public void Calculate_BeforeSplit1()
        {
            Assert.AreEqual(50L * 100000000, BitcoinProtocolHelper.CalculateReward(210000-1));
        }


        [Test]
        public void Calculate_Split1()
        {
            Assert.AreEqual(25L * 100000000, BitcoinProtocolHelper.CalculateReward(210000));
        }

        [Test]
        public void CalculateAfterSplit1()
        {
            Assert.AreEqual(25L * 100000000, BitcoinProtocolHelper.CalculateReward(210000+1));
        }



        [Test]
        public void Calculate_BeforeSplit2()
        {
            Assert.AreEqual(25L * 100000000, BitcoinProtocolHelper.CalculateReward(420000 - 1));
        }

        [Test]
        public void Calculate_Split2()
        {
            Assert.AreEqual(12.5 * 100000000, BitcoinProtocolHelper.CalculateReward(420000));
        }


        [Test]
        public void Calculate_AfterSplit2()
        {
            Assert.AreEqual((long)(12.5 * 100000000), BitcoinProtocolHelper.CalculateReward(420000+1));
        }


        [Test]
        public void Calculate_BeforeSplit3()
        {
            Assert.AreEqual(12.5 * 100000000, BitcoinProtocolHelper.CalculateReward(630000 - 1));
        }

        [Test]
        public void Calculate_Split3()
        {
            Assert.AreEqual(6.25 * 100000000, BitcoinProtocolHelper.CalculateReward(630000));
        }


        [Test]
        public void Calculate_AfterSplit3()
        {
            Assert.AreEqual(6.25 * 100000000, BitcoinProtocolHelper.CalculateReward(630000 + 1));
        }

    }
}
