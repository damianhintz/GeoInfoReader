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
        List<ElementGeoInfo> _elementy = new List<ElementGeoInfo>();

        public void DodajElement(ElementGeoInfo element)
        {
            if (element == null) throw new ArgumentNullException("Dodawany element nie może być null");
            _elementy.Add(element);
        }

        public ElementGeoInfo Szukaj(string numerOperatu)
        {
            var normalnyOperat = numerOperatu.NormalizujNumerOperatu();
            return _elementy.SingleOrDefault(z => normalnyOperat.Equals(z.Operat.NormalizujNumerOperatu()));
        }

        public IEnumerator<ElementGeoInfo> GetEnumerator() { return _elementy.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
