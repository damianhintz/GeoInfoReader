using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfoReader.Rozszerzenia
{
    static class DokumentParser
    {
        public static DokumentGeoInfo ParsujDokument(this string record)
        {
            ////X,"1","","szkic polowy/zbiór szkiców polowych","szkic polowy/zbiór szkiców polowych","160706_5.0004.032_1976.001.tif","SZKICE"
            if (!record.StartsWith("X,"))
                throw new ArgumentException("Rekord dokumentu nie zaczyna się od X,: " + record);
            var recordBezX = record.Substring(2);
            var pola = recordBezX.Split(new string[] { "\",\"" }, StringSplitOptions.None);
            if (pola.Length != 6)
                throw new ArgumentException("Rekord dokumentu nie składa się z 7 pól: " + record);
            for (int i = 0; i < pola.Length; i++) pola[i] = pola[i].Replace("\"", string.Empty);
            var nazwa = pola[0];
            var plik = pola[4];
            var katalog = pola[5];
            return new DokumentGeoInfo { Nazwa = nazwa, Plik = plik, Folder = katalog };
        }
    }
}
