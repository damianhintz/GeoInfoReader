using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader.Tables;

namespace NysaZakresyTesty
{
    [TestClass]
    public class GeoInfoZakresyTest
    {
        [TestMethod]
        public void test_czy_zakresy_geoinfo_zostały_zainicjowane()
        {
            var zakresy = new GeoInfoZakresy();
            Assert.AreEqual(expected: 0, actual: zakresy.Count);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void test_czy_zakresy_geoinfo_zawierają_za_mały_id()
        {
            var zakresy = new GeoInfoZakresy();
            zakresy.Dodaj(id: 0, numerZasobu: "P.1607.2016.1");
            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void test_czy_zakresy_geoinfo_zawierają_zły_numer()
        {
            var zakresy = new GeoInfoZakresy();
            zakresy.Dodaj(id: 0, numerZasobu: "P.1607_2016.1");
            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void test_czy_zakresy_geoinfo_zawierają_powtórzony_numer()
        {
            var zakresy = new GeoInfoZakresy();
            zakresy.Dodaj(id: 1, numerZasobu: "P.1607.2016.1");
            zakresy.Dodaj(id: 2, numerZasobu: "P.1607.2016.1");
            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void test_czy_zakresy_geoinfo_zawierają_powtórzony_id()
        {
            var zakresy = new GeoInfoZakresy();
            zakresy.Dodaj(id: 1, numerZasobu: "P.1607.2016.1");
            zakresy.Dodaj(id: 1, numerZasobu: "P.1607.2016.2");
            Assert.Fail();
        }

        [TestMethod]
        public void test_czy_zakresy_geoinfo_zawierają_dodany_zakres()
        {
            var zakresy = new GeoInfoZakresy();
            zakresy.Dodaj(id: 1, numerZasobu: "P.1607.2016.1");
            Assert.AreEqual(expected: 1, actual: zakresy.Count);
            var id = zakresy.Szukaj(numerZasobu: "P.1607.2016.1");
            Assert.IsTrue(id.HasValue);
            Assert.AreEqual(expected: 1, actual: id.Value);
        }

        [TestMethod]
        public void test_czy_zakresy_geoinfo_nie_zawierają_zakres()
        {
            var zakresy = new GeoInfoZakresy();
            zakresy.Dodaj(id: 1, numerZasobu: "P.1607.2016.1");
            Assert.AreEqual(expected: 1, actual: zakresy.Count);
            var id = zakresy.Szukaj(numerZasobu: "P.1607.2016.2");
            Assert.IsFalse(id.HasValue);
        }
    }
}
