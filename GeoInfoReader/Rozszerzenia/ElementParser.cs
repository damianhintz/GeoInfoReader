using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader.Rozszerzenia
{
    static class ElementParser
    {
        public static ElementGeoInfo ParsujElement(this string record)
        {
            //A,GOSZZG,3,90000001,,
            if (!record.StartsWith("A,")) throw new ArgumentException("Rekord nagłówkowy nie zaczyna się od A,: " + record);
            var pola = record.Split(',');
            if (pola.Length != 6) throw new ArgumentException("Rekord nagłówkowy nie składa się z 6 pól: " + record);
            var kod = pola[1];
            var typ = int.Parse(pola[2]);
            var id = pola[3];
            //var obrót = pola[4];
            //var szerokość = pola[5];
            var zakres = new ElementGeoInfo(kod, typ)
            {
                Id = id
            };
            return zakres;
        }
    }
}
