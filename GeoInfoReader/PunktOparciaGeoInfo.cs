using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader
{
    /// <summary>
    /// Punkt oparcia.
    /// </summary>
    public class PunktOparciaGeoInfo
    {
        public double X;
        public double Y;
        public string Układ;

        public PunktOparciaGeoInfo(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
