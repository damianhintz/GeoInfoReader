using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader
{
    /// <summary>
    /// Atrybut opisowy obiektu.
    /// </summary>
    public class AtrybutGeoInfo
    {
        public string Nazwa;
        public string Wartość;

        public AtrybutGeoInfo(string nazwa, string wartość)
        {
            Nazwa = nazwa;
            Wartość = wartość;
        }
    }
}
