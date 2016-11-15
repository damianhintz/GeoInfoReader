using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;
using Shouldly;

namespace NysaZakresyTesty.GeoInfo
{
    [TestClass]
    public class GeoInfoReaderTest
    {
        [TestMethod]
        public void TangoReader_ShouldReadDocumentsAttachedToObjects()
        {
            var mapa = new MapaGeoInfo();
            var reader = new TangoReader(mapa);
            reader.Wczytaj(Path.Combine(@"..\..\..\Samples", "goszzg.giv"));
            mapa.ShouldNotBeEmpty();
            var obiekt1 = mapa.SzukajId(id: "273000600000705074");
            obiekt1.Dokumenty.ShouldNotBeEmpty();
            //X,"1","160706_5.0004.032_1976.001.tif","SZKICE"
            var dokument = obiekt1.Dokumenty.Single();
            dokument.Nazwa.ShouldBe("1");
            dokument.Plik.ShouldBe("160706_5.0004.032_1976.001.tif");
            dokument.Folder.ShouldBe("SZKICE");
            //273000600000539437
            var obiekt2 = mapa.SzukajId(id: "273000600000539437");
            obiekt2.Dokumenty.ShouldBeEmpty();
        }

        [TestMethod]
        public void test_czy_geoinfo_reader_czyta_układ_punktu()
        {
            var mapa = new MapaGeoInfo();
            var reader = new TangoReader(mapa);
            var mapaPath = Path.Combine(@"..\..\Samples", "UkladPunktu.giv");
            reader.Wczytaj(mapaPath);
            Assert.AreEqual(expected: 1, actual: mapa.Count());
            var punkt = mapa.First();
            Assert.AreEqual(expected: 2, actual: punkt.Punkty.Count());
            var pierwszy = punkt.Punkty.First();
            Assert.IsNull(pierwszy.Układ);
            var drugi = punkt.Punkty.Last();
            Assert.AreEqual(expected: "1965_4", actual: drugi.Układ);
            Assert.AreEqual(expected: 5489402.6, actual: drugi.X);
            Assert.AreEqual(expected: 3756749.75, actual: drugi.Y);
        }

        [TestMethod]
        public void test_czy_geoinfo_reader_czyta_rekord_p()
        {
            var zakresy = new MapaGeoInfo();
            var zakresyReader = new TangoReader(zakresy);
            var zakresyPath = Path.Combine(@"..\..\Samples", "P.giv");
            zakresyReader.Wczytaj(zakresyPath);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
        }

        [TestMethod]
        public void test_czy_geoinfo_reader_czyta_rekord_m_lub_o()
        {
            var zakresy = new MapaGeoInfo();
            var zakresyReader = new TangoReader(zakresy);
            var zakresyPath = Path.Combine(@"..\..\Samples", "MlubO.giv");
            zakresyReader.Wczytaj(zakresyPath);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
        }

        [TestMethod]
        public void test_czy_geoinfo_reader_czyta_rekord_v()
        {
            var zakresy = new MapaGeoInfo();
            var zakresyReader = new TangoReader(zakresy);
            var zakresyPath = Path.Combine(@"..\..\Samples", "V.giv");
            zakresyReader.Wczytaj(zakresyPath);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
        }

        [TestMethod]
        public void test_czy_geoinfo_reader_czyta_atrybut_który_zawiera_znak_równości()
        {
            var zakresy = new MapaGeoInfo();
            var zakresyReader = new TangoReader(zakresy);
            var zakresyPath = Path.Combine(@"..\..\Samples", "C.giv");
            zakresyReader.Wczytaj(zakresyPath);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
            var zakres = zakresy.Single();
            var atrybut = zakres.Atrybuty.Single(a => a.Nazwa.Equals("_remarks"));
            Assert.AreEqual(expected: "błąd transformacji = 0.25 m", actual: atrybut.Wartość);
        }

        [TestMethod]
        public void test_czy_geoinfo_reader_działa()
        {
            var zakresy = new MapaGeoInfo();
            var zakresyReader = new TangoReader(zakresy);
            var zakresyPath = Path.Combine(@"..\..\..\Samples", "Nysa_zakresy.giv");
            zakresyReader.Wczytaj(zakresyPath);
            Assert.AreEqual(expected: 21956, actual: zakresy.Count());
            var pierwszyZakres = zakresy.First();
            Assert.AreEqual("GOSZZG", pierwszyZakres.Kod);
            Assert.AreEqual(3, pierwszyZakres.Typ);
            Assert.AreEqual("861391", pierwszyZakres.Id);
            Assert.AreEqual("188/7/11/1984", pierwszyZakres.Operat);
            Assert.AreEqual(5, pierwszyZakres.Punkty.Count());
            Assert.AreEqual(33, pierwszyZakres.Atrybuty.Count());
            Assert.AreEqual(0, pierwszyZakres.Etykiety.Count());
            Assert.AreEqual(0, pierwszyZakres.Obiekty.Count());
            var ostatniZakres = zakresy.Last();
            Assert.AreEqual("GOSZZG", ostatniZakres.Kod);
            Assert.AreEqual(3, ostatniZakres.Typ);
            Assert.AreEqual("11384480", ostatniZakres.Id);
            Assert.AreEqual("GG-III.6640.1773.2016", ostatniZakres.Operat);
            Assert.AreEqual(6, ostatniZakres.Punkty.Count());
            Assert.AreEqual(31, ostatniZakres.Atrybuty.Count());
            Assert.AreEqual(0, ostatniZakres.Etykiety.Count());
            Assert.AreEqual(0, ostatniZakres.Obiekty.Count());
            Assert.AreEqual(expected: "0018", actual: ostatniZakres.NumerObrębu);
        }
    }
}
