using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoInfoReader.Rozszerzenia;

namespace GeoInfoReader
{
    /// <summary>
    /// Eksporter obiektów GEO-INFO do pliku TANGO.
    /// </summary>
    public class TangoWriter
    {
        MapaGeoInfo _mapa;
        List<string> _records = new List<string>();

        public TangoWriter(MapaGeoInfo zakresy)
        {
            _mapa = zakresy;
        }

        public void Zapisz(string fileName)
        {
            _records.Clear();
            AddRecord("[OPCJE]");
            AddRecord("WersjaFormatu=1.00");
            AddRecord("System=GEO-INFO V");
            AddRecord("Skala=500");
            AddRecord("Układ=2000_18");
            AddRecord(string.Empty);
            AddRecord("[OBIEKTY]");
            foreach (var zakres in _mapa)
            {
                AddRecord(zakres.ToTangoA());
                foreach (var punkt in zakres.Punkty) AddRecord(punkt.ToTangoB());
                foreach (var atrybut in zakres.Atrybuty)
                {
                    var record = atrybut.ToTangoC();
                    AddRecord(record);
                }
                AddRecord(string.Empty);
            }
            File.WriteAllLines(fileName, _records, Encoding.GetEncoding(1250));
        }

        void AddRecord(string record) { _records.Add(record); }
    }
}
