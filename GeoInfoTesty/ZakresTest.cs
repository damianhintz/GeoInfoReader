using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;

namespace NysaZakresyTesty
{
    [TestClass]
    public class ZakresTest
    {
        [TestMethod]
        public void test_czy_kod_zakresu_jest_goszzg()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            Assert.AreEqual(expected: "GOSZZG", actual: zakres.Kod);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void test_czy_kod_zakresu_jest_poza_zakresem()
        {
            var zakres = new ElementGeoInfo(kod: string.Empty, typ: 1);
            Assert.Fail();
        }

        [TestMethod]
        public void test_czy_typ_zakresu_jest_od_1_do_5()
        {
            for (int i = 1; i <= 5; i++)
            {
                var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: i);
                Assert.AreEqual(expected: i, actual: zakres.Typ);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void test_czy_typ_zakresu_nie_jest_mniejszy_od_1()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 0);
            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void test_czy_typ_zakresu_nie_jest_większy_niż_5()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 6);
            Assert.Fail();
        }

        [TestMethod]
        public void test_czy_zakres_jest_pusty()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 2);
            Assert.AreEqual(expected: "GOSZZG", actual: zakres.Kod);
            Assert.AreEqual(expected: 2, actual: zakres.Typ);
            Assert.AreEqual(expected: null, actual: zakres.Id);
            Assert.AreEqual(expected: 0, actual: zakres.Punkty.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Atrybuty.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Etykiety.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Obiekty.Count());
        }

        [TestMethod]
        public void test_czy_zakres_zawiera_dodany_punkt()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 2);
            var punkt = new PunktOparciaGeoInfo(x: 1.2, y: 3.4);
            zakres.DodajPunkt(punkt);
            Assert.AreEqual(expected: 1, actual: zakres.Punkty.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Atrybuty.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Etykiety.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Obiekty.Count());
        }

        [TestMethod]
        public void test_czy_zakres_zawiera_dodany_atrybut()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 2);
            var punkt = new PunktOparciaGeoInfo(x: 1.2, y: 3.4);
            zakres.DodajPunkt(punkt);
            Assert.AreEqual(expected: 1, actual: zakres.Punkty.Count());
            var atrybut = new AtrybutGeoInfo(nazwa: "n", wartość: "w");
            zakres.DodajAtrybut(atrybut);
            Assert.AreEqual(expected: 1, actual: zakres.Atrybuty.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Etykiety.Count());
            Assert.AreEqual(expected: 0, actual: zakres.Obiekty.Count());
        }

        [TestMethod]
        public void test_czy_zakres_zawiera_atrybut_numer_operatu()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 2);
            var punkt = new PunktOparciaGeoInfo(x: 1.2, y: 3.4);
            zakres.DodajPunkt(punkt);
            Assert.AreEqual(expected: 1, actual: zakres.Punkty.Count());
            var atrybut = new AtrybutGeoInfo(nazwa: "KRG.n", wartość: "1/2016");
            zakres.DodajAtrybut(atrybut);
            Assert.AreEqual(expected: 1, actual: zakres.Atrybuty.Count());
            Assert.AreEqual(expected: "1/2016", actual: zakres.Operat);
        }

        [TestMethod]
        public void test_czy_zakres_nie_zawiera_atrybutu_numer_operatu()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 2);
            var punkt = new PunktOparciaGeoInfo(x: 1.2, y: 3.4);
            zakres.DodajPunkt(punkt);
            Assert.AreEqual(expected: 1, actual: zakres.Punkty.Count());
            var atrybut = new AtrybutGeoInfo(nazwa: "KRG", wartość: "1/2016");
            zakres.DodajAtrybut(atrybut);
            Assert.AreEqual(expected: 1, actual: zakres.Atrybuty.Count());
            Assert.AreEqual(expected: string.Empty, actual: zakres.Operat);
        }

        [TestMethod]
        public void test_czy_zakres_zawiera_numer_operatu_z_jednostki_segregującej()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            var krg = new AtrybutGeoInfo(nazwa: "KRG.n", wartość: "");
            zakres.DodajAtrybut(krg);
            var jsg = new AtrybutGeoInfo(nazwa: "JSG", wartość: "1/2016");
            zakres.DodajAtrybut(jsg);
            Assert.AreEqual(expected: 2, actual: zakres.Atrybuty.Count());
            Assert.AreEqual(expected: "1/2016", actual: zakres.Operat);
        }

        [TestMethod]
        public void test_czy_zakres_usunie_punkty()
        {
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 2);
            var punkt = new PunktOparciaGeoInfo(x: 1.2, y: 3.4);
            zakres.DodajPunkt(punkt);
            Assert.AreEqual(expected: 1, actual: zakres.Punkty.Count());
            zakres.UsuńPunkty();
            Assert.IsFalse(zakres.Punkty.Any());
        }
    }
}
