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
        MapaGeoInfo _zakresy;
        List<string> _records = new List<string>();
        int _id = 1;
        int _numbers = 2147480000 / 2;

        public TangoWriter(MapaGeoInfo zakresy)
        {
            _zakresy = zakresy;
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
            foreach (var zakres in _zakresy)
            {
                AddRecord(FixId(zakres.ToTangoA()));
                foreach (var punkt in zakres.Punkty) AddRecord(punkt.ToTangoB());
                foreach (var atrybut in zakres.Atrybuty)
                {
                    var record = atrybut.ToTangoC();
                    AddRecord(FixAtrybutes(record));
                }
                AddRecord(string.Empty);
            }
            File.WriteAllLines(fileName, _records, Encoding.GetEncoding(1250));
        }

        void AddRecord(string record) { _records.Add(record); }

        string FixAtrybutes(string record)
        {
            if (record.StartsWith("C,_number=")) return "C,_number=" + _numbers++;
            else if (record.Equals("C,TZG.n=")) return "C,TZG.n=28";
            else if (record.Equals("C,TZG.d=")) return "C,TZG.d=operat techniczny";
            else return record;
        }

        string FixId(string record)
        {
            if (record.StartsWith("A,GOSZZG,3,")) return "A,GOSZZG,3," + (_id++) + ",,";
            else return record;
        }
    }
}
