using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GeoInfoReader.Rozszerzenia;

namespace GeoInfoReader
{
    /// <summary>
    /// Czytnik obiektów z pliku Tango.
    /// </summary>
    public class TangoReader
    {
        MapaGeoInfo _zakresy;

        bool NieKoniec { get { return _index < _records.Length; } }
        bool RecordA { get { return CurrentRecord.StartsWith("A,"); } }
        bool RecordB { get { return CurrentRecord.StartsWith("B,"); } }
        bool RecordC { get { return CurrentRecord.StartsWith("C,"); } }
        bool RecordD { get { return CurrentRecord.StartsWith("D,"); } }
        bool RecordE { get { return CurrentRecord.StartsWith("E,"); } }
        bool RecordM { get { return CurrentRecord.StartsWith("M,"); } }
        bool RecordO { get { return CurrentRecord.StartsWith("O,"); } }
        bool RecordP { get { return CurrentRecord.StartsWith("P,"); } }
        bool RecordV { get { return CurrentRecord.StartsWith("V,"); } }
        bool RecordX { get { return CurrentRecord.StartsWith("X,"); } }
        bool RecordY { get { return CurrentRecord.StartsWith("Y,"); } }
        bool CommentOrEmpty
        {
            get
            {
                return
                    string.IsNullOrEmpty(CurrentRecord) ||
                    CurrentRecord.StartsWith(";");
            }
        }
        string CurrentRecord { get { return _records[_index]; } }
        string[] _records;
        int _index;

        public TangoReader(MapaGeoInfo zakresy)
        {
            _zakresy = zakresy;
        }

        public void Wczytaj(string fileName)
        {
            _records = File.ReadAllLines(fileName, Encoding.GetEncoding(1250));
            _index = 0;
            while (NieKoniec)
            {
                if (RecordA) ReadZakres();
                else NextRecord();
            }
        }

        void NextRecord() { _index++; }

        void ReadZakres()
        {
            var recordA = CurrentRecord;
            var zakres = recordA.ParsujElement();
            NextRecord();
            while (NieKoniec && !RecordA)
            {
                if (RecordB)
                {
                    var punkt = CurrentRecord.ParsujPunkt();
                    zakres.DodajPunkt(punkt);
                }
                else if (RecordC)
                {
                    var atrybut = CurrentRecord.ParsujAtrybut();
                    zakres.DodajAtrybut(atrybut);
                }
                else if (RecordD) CurrentRecord.ParsujEtykietę();
                else if (RecordE) CurrentRecord.ParsujRelację();
                else if (RecordM) CurrentRecord.ParsujM();
                else if (RecordO) CurrentRecord.ParsujO();
                else if (RecordP) CurrentRecord.ParsujP();
                else if (RecordV) CurrentRecord.ParsujV();
                else if (RecordX) CurrentRecord.ParsujDokument();
                else if (RecordY) CurrentRecord.ParsujSkalę();
                else
                {
                    if (!CommentOrEmpty)
                        throw new InvalidOperationException("Nieoczekiwany rekord w tym miejscu: " + CurrentRecord);
                }
                NextRecord();
            }
            _zakresy.DodajZakres(zakres);
        }

    }
}
