using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoInfoReader.Rozszerzenia
{
    public static class TangoRozszerzenia
    {
        public static string ToTangoA(this ElementGeoInfo zakres)
        {
            //A,GOSZZG,3,90000001,,
            return string.Format("A,{0},{1},{2},,", zakres.Kod, zakres.Typ, zakres.Id);
        }

        public static string ToTangoB(this PunktOparciaGeoInfo punkt)
        {
            //B,,5587860.97,6457834.4,,
            return string.Format("B,,{0},{1},,", punkt.X, punkt.Y);
        }

        public static string ToTangoC(this AtrybutGeoInfo atrybut)
        {
            //C,KRG.n=473-414/12/1980
            return string.Format("C,{0}={1}", atrybut.Nazwa, atrybut.Wartość);
        }
    }
}
