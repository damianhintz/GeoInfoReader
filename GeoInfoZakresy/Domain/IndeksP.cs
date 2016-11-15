using GeoInfoReader;
using GeoInfoReader.Rozszerzenia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoZakresy.Domain
{
    class IndeksP
    {
        public IEnumerable<ElementGeoInfo> this[ElementGeoInfo z]
        {
            get
            {
                var key = z.NumerObrębu + "#" + z.Operat.NormalizujNumerOperatu();
                return pByOperat[key];
            }
        }

        Dictionary<string, List<ElementGeoInfo>> pByOperat = new Dictionary<string, List<ElementGeoInfo>>();
        
        public IndeksP(Zakresy zakresy)
        {
            var pGroupByOperat = zakresy.ZakresyP.GroupBy(z => z.NumerObrębu + "#" + z.Operat.NormalizujNumerOperatu());
            foreach (var g in pGroupByOperat) pByOperat.Add(g.Key, new List<ElementGeoInfo>(g));
        }

        public bool Contains(ElementGeoInfo z)
        {
            var key = z.NumerObrębu + "#" + z.Operat.NormalizujNumerOperatu();
            return pByOperat.ContainsKey(key);
        }
    }
}
