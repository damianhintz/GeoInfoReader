using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace GeoInfoReader.Rozszerzenia
{
    /// <summary>
    /// Parser punktu oparcia obiektu.
    /// </summary>
    public static class PunktParser
    {
        public static PunktOparciaGeoInfo ParsujPunkt(this string record)
        {
            //B,,5489855.83,3755386.5,,,,1965_4
            //B,,5587860.97,6457834.4,,
            if (!record.StartsWith("B,")) throw new ArgumentException("Rekord punktu nie zaczyna się od B,: " + record);
            var pola = record.Split(',');
            if (pola.Length < 6) throw new ArgumentException("Rekord punktu nie składa się przynajmniej z 6 pól: " + record);
            var x = double.Parse(pola[2], NumberFormatInfo.InvariantInfo);
            var y = double.Parse(pola[3], NumberFormatInfo.InvariantInfo);
            var punkt = new PunktOparciaGeoInfo(x, y);
            if (pola.Length == 8)
            {
                var układ = pola.Last();
                punkt.Układ = układ;
            }
            return punkt;
        }

    }
}
