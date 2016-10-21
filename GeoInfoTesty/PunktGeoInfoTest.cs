using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;

namespace NysaZakresyTesty.GeoInfo
{
    [TestClass]
    public class PunktGeoInfoTest
    {
        [TestMethod]
        public void test_czy_punkt_jest_zainicjowany()
        {
            var punkt = new PunktOparciaGeoInfo(x: 1.2, y: 3.4);
            Assert.AreEqual(expected: 1.2, actual: punkt.X);
            Assert.AreEqual(expected: 3.4, actual: punkt.Y);
            Assert.IsNull(punkt.Układ);
        }
    }
}
