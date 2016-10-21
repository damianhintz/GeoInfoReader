using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoInfoReader.Tables
{
    /// <summary>
    /// Czytnik zasięgów z bazy GEO-INFO.
    /// </summary>
    public class GeoInfoZakresyReader
    {
        GeoInfoZakresy _zakresy;

        public GeoInfoZakresyReader(GeoInfoZakresy zakresy) { _zakresy = zakresy; }

        public void WczytajPlik(string fileName)
        {
            var records = File.ReadAllLines(fileName, Encoding.GetEncoding(1250));
            //"c_object_ID","NRI","JSG","NZA","ROZ","TZG","NWN","KRG","OPT"
            var header = records.First();
            if (!header.Equals("\"c_object_ID\",\"NRI\",\"JSG\",\"NZA\",\"ROZ\",\"TZG\",\"NWN\",\"KRG\",\"OPT\""))
                throw new ArgumentException("Nieprawidłowy nagłówek pliku z zakresami: " + header);
            foreach (var record in records.Skip(1)) ParsujZakres(record);
        }

        void ParsujZakres(string record)
        {
            if (string.IsNullOrEmpty(record)) return; //pomiń pusty wiersz
            //"2","P.1607.1980.64","","","","","","471",""
            var cleanRecord = record.Replace("\"", string.Empty);
            var pola = cleanRecord.Split(',');
            if (pola.Length != 9) throw new ArgumentException("Record zakresu nie zawiera 9 pól: " + record);
            var id = int.Parse(pola[0]);
            var numerZasobu = pola[1];
            _zakresy.Dodaj(id, numerZasobu);
        }
    }
}
