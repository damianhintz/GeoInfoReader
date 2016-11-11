using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfoReader;
using Shouldly;

namespace NysaZakresyTesty
{
    [TestClass]
    public class GeomediaWriterTest
    {
        [TestMethod]
        public void TestGeomediaWriter_ShouldWriteAllObjects()
        {
            var map = new MapaGeoInfo();
            var reader = new TangoReader(map);
            var fileName = @"c:\Users\dhintz\Downloads\160705_05_GOSZZG.giv";
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
            writer.Zapisz(@"c:\Users\dhintz\Downloads\160705_05_GOSZZG.txt");
            
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
