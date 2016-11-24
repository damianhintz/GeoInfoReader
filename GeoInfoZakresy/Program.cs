using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using GeoInfoZakresy.Domain;
using GeoInfoZakresy.Domain.Rozszerzenia;
using GeoInfoReader;
using GeoInfoReader.Rozszerzenia;

namespace GeoInfoZakresy
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
            Console.WriteLine("Koniec.");
            Console.Read();
        }

        void Start()
        {
            Console.WriteLine("...");
            var zakresy = new Zakresy();
            var ośrodek = new RaportDowolny();
            Console.WriteLine("Ośrodek P: {0}", ośrodek.OperatyBezP.Count());
            Console.WriteLine("Ośrodek bez P: {0}", ośrodek.OperatyP.Count());
            //DD94AEBEE0144912B9C8EC54A60DEED9

            //var recordsId = File.ReadAllLines("Identifier_do_sprawdzenia.txt");
            //var idSprawdź = new HashSet<string>(recordsId);
            //Console.WriteLine("Dodatkowe: {0}", idSprawdź.Count);
            //var zakresySprawdź = new List<ElementGeoInfo>();
            //foreach(var z in zakresy.ZakresyBezP)
            //{
            //    if (idSprawdź.Contains(z.Identifier))
            //    {
            //        zakresySprawdź.Add(z);
            //    }
            //}
            //Console.WriteLine("Dodatkowe Olsztyn: {0}", zakresySprawdź.Count);
            //var dodatkowe = new List<ElementGeoInfo>();
            //var pg = zakresy.ZakresyP.GroupBy(z => z.NumerZasobu);
            //var p = pg.ToDictionary(g => g.Key);
            //foreach(var z in zakresySprawdź)
            //{
            //    //var op = z.Operat.NormalizujNumerOperatu();
            //    if (ośrodek.ContainsP(z) && !ośrodek.ContainsBezP(z))
            //    {
            //        var nri = ośrodek.GetP(z);
            //        if (p.ContainsKey(nri)) dodatkowe.Add(z);
            //    }
            //}
            //Console.WriteLine("Dodatkowe Olsztyn->Ośrodek: {0}", dodatkowe.Count);
            //var zakresyBezP = dodatkowe
            //    .Select(z => z.Identifier + "\t" +
            //        z.Operat + "\t" +
            //        z.NumerObrębu + "\t" +
            //        z.Atrybuty.SingleOrDefault(a => "_modification_date".Equals(a.Nazwa)).Wartość + "\t" +
            //        z.Atrybuty.SingleOrDefault(a => "_user_name".Equals(a.Nazwa)).Wartość);
            //Console.WriteLine("Koniec.");
            //Console.Read();

            //DD94AEBEE0144912B9C8EC54A60DEED9
            var indeksP = new IndeksP(zakresy);
            
            var zakresyPodobneDoP = zakresy.ZakresyBezP.Where(z => indeksP.Contains(z));
            Debug.Assert(zakresyPodobneDoP.Single(z => z.Identifier.Equals("DD94AEBEE0144912B9C8EC54A60DEED9")) != null);

            var nachodząceZakresy = indeksP.ZakresyNachodząceNaP(zakresyPodobneDoP);
            Debug.Assert(nachodząceZakresy.Single(z => z.Identifier.Equals("DD94AEBEE0144912B9C8EC54A60DEED9")) != null);

            var zakresyBezP = ośrodek
                .ZakresyNieWystępująWOśrodku(nachodząceZakresy)
                .Select(z => z.Identifier + "\t" +
                    z.Operat + "\t" +
                    z.NumerObrębu + "\t" +
                    z.Atrybuty.SingleOrDefault(a => "_modification_date".Equals(a.Nazwa)).Wartość);
            Console.WriteLine(zakresyBezP.Count());

            //var map = new MapaGeoInfo();
            //foreach (var z in dodatkowe) map.DodajElement(z);
            //var writer = new GeomediaWriter(map);
            //writer.ClearAttributes();
            //writer.IncludeAttribute("_identifier");
            //writer.IncludeAttribute("NRI");
            //writer.IncludeAttribute("KRG.n");
            //writer.IncludeAttribute("JSG");
            //writer.IncludeAttribute("_creation_date");
            //writer.IncludeAttribute("_modification_date");
            //writer.IncludeAttribute("_user_name");
            //writer.IncludeAttribute("_s.Obręb.n");
            //writer.Zapisz("Dodatkowe.txt");
            File.WriteAllLines("Temp.txt", zakresyBezP);
            Process.Start("Temp.txt");
            
        }
    }
}
