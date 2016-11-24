using GeoInfoReader;
using GeoInfoReader.Rozszerzenia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoZakresy.Domain.Rozszerzenia
{
    static class ZakresyRozszerzenia
    {
        public static IEnumerable<ElementGeoInfo> ZakresyNieWystępująWOśrodku(this RaportDowolny ośrodek,
            IEnumerable<ElementGeoInfo> zakresy)
        {
            foreach (var zakres in zakresy)
            {
                var op = zakres.Operat.NormalizujNumerOperatu();
                if (!ośrodek.ContainsP(zakres)) continue;
                if (ośrodek.ContainsBezP(zakres)) continue;
                yield return zakres;
            }
        }
        public static IEnumerable<ElementGeoInfo> ZakresyNachodząceNaP(this IndeksP indeksP,
            IEnumerable<ElementGeoInfo> zakresy)
        {
            //Debug.Assert(zakresy.Single(z => z.Identifier.Equals("DD94AEBEE0144912B9C8EC54A60DEED9")) != null);
            foreach (var bezP in zakresy)
            {
                var otoczka = bezP.ToOtoczka();
                if (otoczka == null) continue; //bez geometrii
                var podobneZakresy = indeksP[bezP];
                var id = bezP.Identifier;
                if (id.Equals("DD94AEBEE0144912B9C8EC54A60DEED9")) Console.WriteLine("DD94AEBEE0144912B9C8EC54A60DEED9");
                foreach (var zakresP in podobneZakresy)
                {
                    var otoczkaP = zakresP.ToOtoczka();
                    if (id.Equals("DD94AEBEE0144912B9C8EC54A60DEED9"))
                        Console.WriteLine("id: " + zakresP.NumerZasobu);
                    if (otoczkaP.Overlaps(otoczka) || otoczka.Intersects(otoczka))
                    {
                        if (id.Equals("DD94AEBEE0144912B9C8EC54A60DEED9"))
                            Console.WriteLine("int: " + zakresP.NumerZasobu);
                        yield return bezP;
                    }
                }
            }
        }
    }
}
