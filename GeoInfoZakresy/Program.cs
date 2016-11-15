using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GeoInfoZakresy.Domain;
using GeoInfoZakresy.Domain.Rozszerzenia;

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
            var zakresy = new Zakresy();
            var indeksP = new IndeksP(zakresy);
            var zakresyPodobneDoP = zakresy.ZakresyBezP.Where(z => indeksP.Contains(z));
            var nachodząceZakresy = indeksP.ZakresyNachodząceNaP(zakresyPodobneDoP);
            var ośrodek = new RaportDowolny();
            var zakresyBezP = ośrodek
                .ZakresyNieWystępująWOśrodku(nachodząceZakresy)
                .Select(z => z.Identifier);
            Console.WriteLine(zakresyBezP.Count());
            File.WriteAllLines("Temp.txt", zakresyBezP);
            Process.Start("Temp.txt");
        }
    }
}
