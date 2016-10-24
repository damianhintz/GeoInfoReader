using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoInfoReader
{
    /// <summary>
    /// Eksporter obiektów GEO-INFO do pliku tekstowego Geomedia.
    /// </summary>
    public class GeomediaWriter
    {
        MapaGeoInfo _mapa;
        List<string> _records = new List<string>();

        public GeomediaWriter(MapaGeoInfo zakresy)
        {
            _mapa = zakresy;
        }

        public void Zapisz(string fileName)
        {
            _records.Clear();
            foreach (var zakres in _mapa)
            {
                //gm.serock_cz1 5823840.73 7498257.55 5823765.90 7498269.65
                var punkty = from p in zakres.Punkty select p.X + " " + p.Y;
                var punktyJoin = string.Join(" ", punkty);
                var nazwa = zakres.NumerZasobu.Replace(" ", "_"); //nie może być spacji
                AddRecord(nazwa + " " + punktyJoin);
            }
            File.WriteAllLines(fileName, _records, Encoding.GetEncoding(1250));
        }

        void AddRecord(string record) { _records.Add(record); }
    }
}
