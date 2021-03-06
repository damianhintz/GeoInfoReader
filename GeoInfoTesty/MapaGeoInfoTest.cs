﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;
using Shouldly;

namespace NysaZakresyTesty.GeoInfo
{
    [TestClass]
    public class MapaGeoInfoTest
    {
        [TestMethod]
        public void MapaGeoInfo_ShouldHaveUniqueObjects()
        {
            var map = new MapaGeoInfo();
            var element = new ElementGeoInfo("GOSZZG", 3);
            element.DodajAtrybut(new AtrybutGeoInfo("_identifier", "Id1"));
            map.DodajElement(element);
            Should.Throw<InvalidOperationException>(() => { map.DodajElement(element); });
        }

        [TestMethod]
        public void MapaGeoInfo_ShouldHaveTwoUniqueObjects()
        {
            var map = new MapaGeoInfo();
            var element = new ElementGeoInfo("GOSZZG", 3);
            element.DodajAtrybut(new AtrybutGeoInfo("_identifier", "Id1"));
            map.DodajElement(element);
            var element2 = new ElementGeoInfo("GOSZZG", 3);
            element2.DodajAtrybut(new AtrybutGeoInfo("_identifier", "Id2"));
            map.DodajElement(element2);
            map.Count().ShouldBe(expected: 2);
            map.SzukajId("Id1").ShouldNotBeNull();
            map.SzukajId("Id2").ShouldNotBeNull();
            map.SzukajId("Id3").ShouldBeNull();
        }

        [TestMethod]
        public void test_czy_brak_zakresów()
        {
            var zakresy = new MapaGeoInfo();
            Assert.AreEqual(expected: 0, actual: zakresy.Count());
        }

        [TestMethod]
        public void test_czy_zakresy_zostały_dodane()
        {
            var zakresy = new MapaGeoInfo();
            var id = new AtrybutGeoInfo(nazwa: "_identifier", wartość: "1234");
            var element = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            element.DodajAtrybut(id);
            zakresy.DodajElement(element);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void test_czy_zakresy_nie_zostały_dodane()
        {
            var zakresy = new MapaGeoInfo();
            zakresy.DodajElement(null);
            Assert.Fail();
        }

        [TestMethod]
        public void test_czy_repozytorium_zawiera_zakres_z_podanym_operatem()
        {
            var zakresy = new MapaGeoInfo();
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            //C,KRG.n=473-414/12/1980
            var id = new AtrybutGeoInfo(nazwa: "_identifier", wartość: "1234");
            zakres.DodajAtrybut(atrybut: id);
            var numerOperatu = new AtrybutGeoInfo(nazwa: "KRG.n", wartość: "473-414/12/1980");
            zakres.DodajAtrybut(atrybut: numerOperatu);
            zakresy.DodajElement(zakres);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
            var znalezionyZakres = zakresy.Szukaj(numerOperatu: "473-414/12/1980");
            Assert.IsNotNull(znalezionyZakres);
            Assert.AreSame(expected: zakres, actual: znalezionyZakres);
            Assert.AreEqual(expected: zakres.Id, actual: znalezionyZakres.Id);
        }

        [TestMethod]
        public void test_czy_repozytorium_nie_zawiera_zakresu_dla_operatu()
        {
            var zakresy = new MapaGeoInfo();
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            //C,KRG.n=473-414/12/1980
            var id = new AtrybutGeoInfo(nazwa: "_identifier", wartość: "1234");
            zakres.DodajAtrybut(id);
            var numerOperatu = new AtrybutGeoInfo(nazwa: "KRG.n", wartość: "473-414/12/1980");
            zakres.DodajAtrybut(atrybut: numerOperatu);
            zakresy.DodajElement(zakres);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
            var znalezionyZakres = zakresy.Szukaj(numerOperatu: "473_1980");
            Assert.IsNull(znalezionyZakres);
        }

        [TestMethod]
        public void test_czy_repozytorium_zawiera_zakres_dla_podobnego_operatu()
        {
            var zakresy = new MapaGeoInfo();
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            //C,KRG.n=473-414/12/1980
            var id = new AtrybutGeoInfo(nazwa: "_identifier", wartość: "1234");
            zakres.DodajAtrybut(atrybut: id);
            var numerOperatu = new AtrybutGeoInfo(nazwa: "KRG.n", wartość: "473-414/12/1980");
            zakres.DodajAtrybut(atrybut: numerOperatu);
            zakresy.DodajElement(zakres);
            Assert.AreEqual(expected: 1, actual: zakresy.Count());
            var znalezionyZakres = zakresy.Szukaj(numerOperatu: "473_414/12_80");
            Assert.IsNotNull(znalezionyZakres);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void test_czy_repozytorium_zawiera_zakresy_z_powtórzonym_operatem()
        {
            var zakresy = new MapaGeoInfo();
            var zakres = new ElementGeoInfo(kod: "GOSZZG", typ: 1);
            //C,KRG.n=473-414/12/1980
            var numerOperatu = new AtrybutGeoInfo(nazwa: "KRG.n", wartość: "473-414/12/1980");
            zakres.DodajAtrybut(atrybut: numerOperatu);
            zakresy.DodajElement(zakres);
            zakresy.DodajElement(zakres);
            Assert.AreEqual(expected: 2, actual: zakresy.Count());
            zakresy.Szukaj(numerOperatu: "473-414/12/1980");
            Assert.Fail();
        }
    }
}
