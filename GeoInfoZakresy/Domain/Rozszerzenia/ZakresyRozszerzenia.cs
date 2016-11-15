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
                if (!ośrodek.ContainsP(op)) continue;
                if (ośrodek.ContainsBezP(op)) continue;
                yield return zakres;
            }
        }
        public static IEnumerable<ElementGeoInfo> ZakresyNachodząceNaP(this IndeksP indeksP,
            IEnumerable<ElementGeoInfo> zakresy)
        {
            foreach (var bezP in zakresy)
            {
                var otoczka = bezP.ToOtoczka();
                if (otoczka == null) continue; //bez geometrii
                var podobneZakresy = indeksP[bezP];
                foreach (var zakresP in podobneZakresy)
                {
                    var otoczkaP = zakresP.ToOtoczka();
                    if (otoczkaP.Overlaps(otoczka)) yield return bezP;
                }
            }
        }
    }
}
