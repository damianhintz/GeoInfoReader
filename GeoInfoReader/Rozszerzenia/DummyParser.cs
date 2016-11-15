using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader.Rozszerzenia
{
    static class DummyParser
    {
        public static void ParsujEtykietę(this string record) { }
        public static void ParsujRelację(this string record) { }
        public static void ParsujSkalę(this string record) { }
        public static void ParsujM(this string record) { }
        public static void ParsujO(this string record) { }
        public static void ParsujP(this string record) { }
        public static void ParsujV(this string record) { }
    }
}
