using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader.Rozszerzenia;

namespace NysaZakresyTesty.GeoInfo
{
    [TestClass]
    public class OperatTest
    {
        [TestMethod]
        public void test_czy_normalizuje_operat_188_13_2494()
        {
            var op = "188/13/2494".NormalizujNumerOperatu();
            Assert.AreEqual(expected: "188-13/2494", actual: op);
        }

        [TestMethod]
        public void test_czy_normalizuje_operat_3005_25_200()
        {
            var op = "3005/25/200".NormalizujNumerOperatu();
            Assert.AreEqual(expected: "3005-25/200", actual: op);
        }

        [TestMethod]
        public void test_czy_normalizuje_operat_2909_51_20007()
        {
            var op = "2909/51/20007".NormalizujNumerOperatu();
            Assert.AreEqual(expected: "2909-51/20007", actual: op);
        }
    }
}
