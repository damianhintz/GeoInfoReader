using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoInfoReader.Rozszerzenia;
using GeoInfoReader;

namespace GeoInfoZakresy.Domain
{
    class RaportDowolny
    {
        HashSet<string> operatyP = new HashSet<string>();
        HashSet<string> operatyBezP = new HashSet<string>();

        public RaportDowolny()
        {
            var operatyZasobu = File.ReadAllLines(@"C:\Users\dhintz\Downloads\Operaty zasobu.csv");
            foreach (var line in operatyZasobu)
            {
                var pola = line.Split('\t');
                var id = pola[0];
                var kerg = pola[4].NormalizujNumerOperatu();
                var innaNazwa = pola[5].NormalizujNumerOperatu();
                if (id.StartsWith("P.1607."))
                {
                    operatyP.Add(id);
                    operatyP.Add(kerg);
                    operatyP.Add(innaNazwa);
                }
                else
                {
                    operatyBezP.Add(id.NormalizujNumerOperatu());
                    operatyP.Add(kerg);
                    operatyP.Add(innaNazwa);
                }
            }
        }

        public bool ContainsP(string op)
        {
            return operatyP.Contains(op);
        }

        public bool ContainsBezP(string op)
        {
            return operatyBezP.Contains(op);
        }
    }
}
