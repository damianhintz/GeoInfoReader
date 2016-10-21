using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader.Rozszerzenia
{
    public static class AtrybutParser
    {
        public static AtrybutGeoInfo ParsujAtrybut(this string record)
        {
            //C,KRG.n=GG-III.6640.1367.2014
            //C,_remarks=błąd transformacji = 0.25 m
            if (!record.StartsWith("C,"))
                throw new ArgumentException("Rekord atrybutu nie zaczyna się od C,: " + record);
            var pola = record.Split(new char[] { '=' }, count: 2);
            if (pola.Length != 2)
                throw new ArgumentException("Rekord atrybutu nie składa się z 2 pól: " + record);
            var nazwa = pola[0].Substring(2);
            var wartość = pola[1];
            var atrybut = new AtrybutGeoInfo(nazwa, wartość);
            return atrybut;
        }

    }
}
