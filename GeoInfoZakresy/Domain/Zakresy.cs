using GeoInfoReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoZakresy.Domain
{
    class Zakresy
    {
        /// <summary>
        /// Zakresy z nadanym numerem zasobu.
        /// </summary>
        public IEnumerable<ElementGeoInfo> ZakresyP => NaszeZakresy.Where(e => e.NumerZasobu.StartsWith("P.1607."));

        /// <summary>
        /// Zakresy bez numeru zasobu.
        /// </summary>
        public IEnumerable<ElementGeoInfo> ZakresyBezP => NaszeZakresy.Where(e => !e.NumerZasobu.StartsWith("P.1607."));

        /// <summary>
        /// Zakresy z naszych obrębów.
        /// </summary>
        public IEnumerable<ElementGeoInfo> NaszeZakresy => _map.Where(e => NaszeObręby.Contains(e.NumerObrębu));

        /// <summary>
        /// Lista naszych obrębów.
        /// </summary>
        public HashSet<string> NaszeObręby = new HashSet<string>
        {
            "0002", "0005", "0009", "0010", "0012", "0013",
            "0015", "0016", "0017", "0020", "0023", "0024"
        };

        private MapaGeoInfo _map = new MapaGeoInfo();

        public Zakresy()
        {
            var reader = new TangoReader(_map);
            var fileName = @"..\..\..\Samples\GOSZZG.giv";
            reader.Wczytaj(fileName);
        }
    }
}
