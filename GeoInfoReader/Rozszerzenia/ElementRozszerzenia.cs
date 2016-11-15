using GeoAPI.Geometries;
using GeoAPI.Operation.Buffer;
using NetTopologySuite.IO;
using NetTopologySuite.Operation.Buffer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader.Rozszerzenia
{
    public static class ElementRozszerzenia
    {
        static WKTReader _reader = new WKTReader();

        public static IGeometry ToOtoczka(this ElementGeoInfo element)
        {
            if (!element.Punkty.Any()) return null;
            var xyPunkty =
                from pkt in element.Punkty
                select string.Format(
                    NumberFormatInfo.InvariantInfo,
                    "{0:F2} {1:F2}", pkt.X, pkt.Y);
            var xyJoin = string.Join(separator: ", ", values: xyPunkty);
            var wkt = "MULTIPOINT (" + xyJoin + ")"; //MULTIPOINT (10 40, 40 30, 20 20, 30 10)
            var multipoint = _reader.Read(wkt);
            var otoczka = multipoint.ConvexHull();
            var points = otoczka.Coordinates.Count();
            if (points < 3) return null;
            //var param = new BufferParameters(0, EndCapStyle.Flat);
            //else if (points < 4) buffer = BufferOp.Buffer(otoczka, 15, param).Envelope;
            return otoczka;
        }

    }
}
