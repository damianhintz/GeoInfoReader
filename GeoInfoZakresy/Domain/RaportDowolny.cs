using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoInfoReader.Rozszerzenia;
using GeoInfoReader;
using GeoInfoZakresy.Domain.Rozszerzenia;

namespace GeoInfoZakresy.Domain
{
    class RaportDowolny
    {
        public IEnumerable<string> OperatyP => operatyP;
        HashSet<string> operatyP = new HashSet<string>();

        public IEnumerable<string> OperatyBezP => operatyBezP;
        HashSet<string> operatyBezP = new HashSet<string>();
        Dictionary<string, string> _p = new Dictionary<string, string>();

        public RaportDowolny()
        {
            var operatyZasobu = File.ReadAllLines(@"Operaty zasobu.csv");
            foreach (var line in operatyZasobu)
            {
                var pola = line.Split('\t');
                var id = pola[0];
                var obr = pola[2].Replace("<o> ", "");
                var kerg = pola[4].NormalizujNumerOperatu();
                var innaNazwa = pola[5].NormalizujNumerOperatu();
                if (id.StartsWith("P.1607."))
                {
                    operatyP.Add(id);
                    var kergId = kerg.ToIdWithObr(obr);
                    operatyP.Add(kergId);
                    var innyId = innaNazwa.ToIdWithObr(obr);
                    operatyP.Add(innyId);
                    if (!string.IsNullOrEmpty(kerg))
                    {
                        if (!_p.ContainsKey(kergId)) _p.Add(kergId, id);
                    }
                    if (!innyId.Equals(kergId) && !string.IsNullOrEmpty(innaNazwa))
                    {
                        if (!_p.ContainsKey(innyId)) _p.Add(innyId, id);
                    }
                }
                else
                {
                    operatyBezP.Add(id.NormalizujNumerOperatu().ToIdWithObr(obr));
                    operatyBezP.Add(kerg.ToIdWithObr(obr));
                    operatyBezP.Add(innaNazwa.ToIdWithObr(obr));
                }
            }
        }

        public string GetP(ElementGeoInfo z)
        {
            var id = z.Operat.NormalizujNumerOperatu().ToIdWithObr(z.NumerObrębu);
            return _p[id];
        }

        public bool ContainsP(ElementGeoInfo z)
        {
            var op = z.Operat.NormalizujNumerOperatu();
            var id = op.ToIdWithObr(z.NumerObrębu);
            return operatyP.Contains(id);
        }

        public bool ContainsBezP(ElementGeoInfo z)
        {
            var op = z.Operat.NormalizujNumerOperatu();
            var id = op.ToIdWithObr(z.NumerObrębu);
            return operatyBezP.Contains(id);
        }
    }
}
