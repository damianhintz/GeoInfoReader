using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoInfoReader.Rozszerzenia;

namespace GeoInfoReader
{
    /// <summary>
    /// Mapa obiektów GEO-INFO.
    /// </summary>
    public class MapaGeoInfo : IEnumerable<ElementGeoInfo>
    {
        List<ElementGeoInfo> _zakresy = new List<ElementGeoInfo>();

        public void DodajZakres(ElementGeoInfo zakres)
        {
            if (zakres == null) throw new ArgumentNullException("Dodawany zakres nie może być null");
            _zakresy.Add(zakres);
        }

        public ElementGeoInfo Szukaj(string numerOperatu)
        {
            var normalnyOperat = numerOperatu.NormalizujNumerOperatu();
            return _zakresy.SingleOrDefault(z => normalnyOperat.Equals(z.Operat.NormalizujNumerOperatu()));
        }

        public IEnumerator<ElementGeoInfo> GetEnumerator() { return _zakresy.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
