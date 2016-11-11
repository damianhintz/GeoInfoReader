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
        HashSet<string> _atrybuty = new HashSet<string> { "NRI" };

        public GeomediaWriter(MapaGeoInfo zakresy, params string[] atrybuty)
        {
            _mapa = zakresy;
        }

        public void IncludeAttribute(string atr)
        {
            if (string.IsNullOrWhiteSpace(atr)) throw new ArgumentException("atr != null");
            _atrybuty.Add(atr);
        }

        public void ClearAttributes() { _atrybuty.Clear(); }

        public void Zapisz(string fileName)
        {
            _records.Clear();
            
            foreach (var obj in _mapa)
            {
                var values = new List<string>();
                foreach(var atr in _atrybuty)
                {
                    var atrFound = obj.Atrybuty.SingleOrDefault(a => atr.Equals(a.Nazwa));
                    var atrValue = atrFound == null ? "_" + atr : atrFound.Wartość;
                    values.Add(atrValue.Replace(" ", "_")); //bez spacji
                }
                var valuesJoin = string.Join(" ", values);
                //gm.serock_cz1 5823840.73 7498257.55 5823765.90 7498269.65
                var punkty = from p in obj.Punkty select p.X + " " + p.Y;
                var punktyJoin = string.Join(" ", punkty);
                //var nazwa = obj.NumerZasobu.Replace(" ", "_"); //nie może być spacji
                AddRecord(valuesJoin + " " + punktyJoin);
            }
            File.WriteAllLines(fileName, _records, Encoding.GetEncoding(1250));
        }

        void AddRecord(string record) { _records.Add(record); }
    }
}
