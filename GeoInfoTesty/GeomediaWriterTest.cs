using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;
using GeoInfoReader.Rozszerzenia;
using Shouldly;

namespace NysaZakresyTesty
{
    [TestClass]
    public class GeomediaWriterTest
    {
        [TestMethod]
        public void GeomediaWriter_ShouldWriteAllObjects()
        {
            var map = new MapaGeoInfo();
            var reader = new TangoReader(map);
            var fileName = @"..\..\..\Samples\GOSZZG.giv";
            reader.Wczytaj(fileName);
            map.Count().ShouldBe(26890);
            var writer = new GeomediaWriter(map);
            writer.ClearAttributes();
            writer.IncludeAttribute("_identifier");
            writer.IncludeAttribute("NRI");
            writer.IncludeAttribute("KRG.n");
            writer.IncludeAttribute("JSG");
            writer.IncludeAttribute("_creation_date");
            writer.IncludeAttribute("_modification_date");
            writer.IncludeAttribute("_user_name");
            writer.IncludeAttribute("_s.Obręb.n");
            writer.IncludeDynamicAttribute("Pliki", e => e.Dokumenty.Count().ToString());
            writer.IncludeDynamicAttribute("Dokumenty", e =>
            {
                var pliki = new HashSet<string>();
                foreach (var d in e.Dokumenty)
                {
                    var plik = d.Plik;
                    var katalog = plik;
                    var index = plik.LastIndexOf('\\');
                    if (index > 0) katalog = plik.Substring(0, index);
                    katalog = katalog.Split('\\').Last();
                    //var nazwa = plik.Substring(index);
                    pliki.Add(katalog.Replace(" ", "_"));
                }
                if (pliki.Any()) return string.Join(",", pliki);
                else return "_";
            });
            writer.Zapisz(@"GOSZZG.txt");

            //writer.AttachAttributesByIndexOrById(custom);
            //KRG.n
            //_identifier
            //C,_identifier = 273000600000541732
            //C,_creation_date = 2016 - 11 - 09 13:12:24
            //C,_modification_date = 2016 - 11 - 09 13:12:24
            //C,_user_name = OPEGIEKA
            //C,_s.Obręb.n=0004
            //Files
        }

    }
}
