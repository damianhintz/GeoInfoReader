using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;

namespace NysaZakresyTesty
{
    [TestClass]
    public class TangoReaderTest
    {
        [TestMethod]
        public void test_czy_zakresy_tango_poprawione()
        {
            var zakresy = new MapaGeoInfo();
            var zakresyReader = new TangoReader(zakresy);
            var zakresyPath = Path.Combine(@"..\..\..\Samples", "Olsztyn_zakresy_obr.tng");
            zakresyReader.Wczytaj(zakresyPath);
            //Opcje.WersjaFormatu
            //Opcje.System
            //Opcje.Skala
            //Opcje.Układ
            Assert.AreEqual(expected: 3868, actual: zakresy.Count());
            var pierwszyZakres = zakresy.First();
            Assert.AreEqual("GOSZZG", pierwszyZakres.Kod);
            Assert.AreEqual(3, pierwszyZakres.Typ);
            Assert.AreEqual("1", pierwszyZakres.Id);
            Assert.AreEqual("473-414/12/1980", pierwszyZakres.Operat);
            Assert.AreEqual(483, pierwszyZakres.Punkty.Count());
            Assert.AreEqual(30, pierwszyZakres.Atrybuty.Count());
            Assert.AreEqual(0, pierwszyZakres.Etykiety.Count());
            Assert.AreEqual(0, pierwszyZakres.Obiekty.Count());
            var ostatniZakres = zakresy.Last();
            Assert.AreEqual("GOSZZG", ostatniZakres.Kod);
            Assert.AreEqual(3, ostatniZakres.Typ);
            Assert.AreEqual("3868", ostatniZakres.Id);
            Assert.AreEqual("GG-III.6640.1367.2014", ostatniZakres.Operat);
            Assert.AreEqual(399, ostatniZakres.Punkty.Count());
            Assert.AreEqual(30, ostatniZakres.Atrybuty.Count());
            Assert.AreEqual(0, ostatniZakres.Etykiety.Count());
            Assert.AreEqual(0, ostatniZakres.Obiekty.Count());
            Assert.AreEqual(expected: "0024", actual: ostatniZakres.NumerObrębu);
        }
    }
}
