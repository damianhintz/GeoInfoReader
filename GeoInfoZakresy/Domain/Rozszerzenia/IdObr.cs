using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoZakresy.Domain.Rozszerzenia
{
    public static class IdObr
    {
        public static string ToIdWithObr(this string id, string obr)
        {
            return obr + "#" + id;
        }
    }
}
