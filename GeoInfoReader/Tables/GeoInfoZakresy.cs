using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoInfoReader.Tables
{
    /// <summary>
    /// Reprezentuje zasięgi w bazie GEO-INFO.
    /// </summary>
    public class GeoInfoZakresy : IEnumerable<string>
    {
        public int Count { get { return _indeksId.Count; } }

        Dictionary<string, int> _indeksZasobu = new Dictionary<string, int>();
        Dictionary<int, string> _indeksId = new Dictionary<int, string>();

        public void Dodaj(int id, string numerZasobu)
        {
            if (id < 1) throw new ArgumentException("Id obiektu musi być dodatni: " + id);
            if (string.IsNullOrEmpty(numerZasobu)) throw new ArgumentNullException("Numer zasobu nie może być pusty");
            if (!numerZasobu.StartsWith("P.1607.")) throw new ArgumentException("Numer zasosbu musi zaczynać się od 'P.1607.': " + numerZasobu);
            if (_indeksId.ContainsKey(id)) throw new InvalidOperationException("Id obiektu nie może się powtarzać: " + id);
            if (_indeksZasobu.ContainsKey(numerZasobu)) throw new InvalidOperationException("Numer zasobu nie może się powtarzać: " + numerZasobu);
            _indeksId.Add(id, numerZasobu);
            _indeksZasobu.Add(numerZasobu, id);
        }

        public int? Szukaj(string numerZasobu)
        {
            if (_indeksZasobu.ContainsKey(numerZasobu)) return _indeksZasobu[numerZasobu];
            else return null;
        }

        public IEnumerator<string> GetEnumerator() { return _indeksZasobu.Keys.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
