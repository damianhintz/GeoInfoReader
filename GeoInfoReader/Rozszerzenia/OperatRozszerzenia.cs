using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoInfoReader.Rozszerzenia
{
    public static class OperatRozszerzenia
    {
        public static string NormalizujNumerOperatu(this string numerOperatu)
        {
            if (string.IsNullOrEmpty(numerOperatu)) return numerOperatu;
            numerOperatu = numerOperatu
                .Replace("KERG ", string.Empty)
                .Replace("/", "_") //Slash
                .Replace(".", "_") //Kropka
                .Replace("_", "_") //Podkreślenie
                .Replace("-", "_"); //Myślnik
            var pola = numerOperatu.Split('_');
            var pierwsze = pola.First();
            if (char.IsDigit(pierwsze.First()) == false) return numerOperatu;
            var ostatnie = pola.Last();
            int rok = -1;
            if (int.TryParse(ostatnie, out rok) == false) return numerOperatu;
            if (rok < 16) rok = 2000 + rok;
            else if (rok < 100) rok = 1900 + rok;
            if (rok < 1 || rok > 99999) throw new ArgumentException("Numer operatu zawiera datę poza zakresem (1900-2016): " + numerOperatu);
            var początek = string.Join("/", pola, 0, pola.Length - 1);
            if (pola.Length > 2) początek = pierwsze + "-" + string.Join("/", pola, 1, pola.Length - 2);
            return początek + "/" + rok.ToString();
        }


    }
}
