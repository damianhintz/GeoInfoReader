using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader.Tables;

namespace NysaZakresyTesty
{
    [TestClass]
    public class GeoInfoZakresyReaderTest
    {
        [TestMethod]
        public void test_czy_zakresy_reader_działa()
        {
            var zakresy = new GeoInfoZakresy();
            var reader = new GeoInfoZakresyReader(zakresy);
            var fileName = Path.Combine(@"..\..\Samples", "G_SZKICE.csv");
            reader.WczytajPlik(fileName);
            //reader.WczytajDatabase(server: ".\GEOINFO", database: "Zakresy");
            Assert.AreEqual(expected: 3868, actual: zakresy.Count);
            var id2 = zakresy.Szukaj(numerZasobu: "P.1607.1980.64");
            Assert.AreEqual(expected: 2, actual: id2.Value);
            var id3869 = zakresy.Szukaj(numerZasobu: "P.1607.2014.1022");
            Assert.AreEqual(expected: 3869, actual: id3869.Value);
        }
    }
}
