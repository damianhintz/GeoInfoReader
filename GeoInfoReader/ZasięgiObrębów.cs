using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GeoInfoReader
{
    /// <summary>
    /// Ekstraktor zasięgów obrębów z pliku GEO-INFO V.
    /// </summary>
    public class ZasięgiObrębów
    {
        public int Count { get { return _mapa.Count(); } }
        MapaGeoInfo _mapa = new MapaGeoInfo();
        MapaGeoInfo _mapaOgólna = new MapaGeoInfo();

        public void Wczytaj(string fileName)
        {
            var linie = File.ReadAllLines(fileName, Encoding.GetEncoding(1250));
            var typ = string.Empty;
            var punkty = new List<string>();
            var operat = string.Empty;
            var jednostkaEwidencyjna = string.Empty;
            var numerObrębu = string.Empty;
            foreach (var linia in linie)
            {
                if (linia.StartsWith("A,"))
                {
                    typ = linia.Substring(0, 10);
                    punkty.Clear();
                }
                if (linia.StartsWith("B,")) punkty.Add(linia);
                if (linia.StartsWith("C,_s.Jednostka_ewidencyjna.n=")) jednostkaEwidencyjna = linia.Substring(29);
                if (linia.StartsWith("C,_s.Obręb.n="))
                {
                    numerObrębu = linia.Substring(13);
                    if (typ.StartsWith("A,GESJOB,3"))
                    {
                        var zasięg = new ElementGeoInfo(kod: "GESJOB", typ: 3);
                        zasięg.DodajAtrybut(new AtrybutGeoInfo("NRI", jednostkaEwidencyjna + "_" + numerObrębu));
                        zasięg.DodajAtrybut(new AtrybutGeoInfo("_s.Jednostka_ewidencyjna.n", jednostkaEwidencyjna));
                        zasięg.DodajAtrybut(new AtrybutGeoInfo("_s.Obręb.n", numerObrębu));
                        _mapa.DodajElement(zasięg);
                        var minX = double.MaxValue;
                        var minY = double.MaxValue;
                        var maxX = 0.0;
                        var maxY = 0.0;
                        foreach (var punkt in punkty)
                        {
                            var pola = punkt.Split(',');
                            var x = double.Parse(pola[2]);
                            if (x < minX) minX = x;
                            if (x > maxX) maxX = x;
                            var y = double.Parse(pola[3]);
                            if (y < minY) minY = y;
                            if (y > maxY) maxY = y;
                            zasięg.DodajPunkt(new PunktOparciaGeoInfo(x, y));
                        }
                        var ogólnyZasięg = new ElementGeoInfo(kod: "GESJOB", typ: 3);
                        ogólnyZasięg.DodajAtrybut(new AtrybutGeoInfo("NRI", jednostkaEwidencyjna + "_" + numerObrębu));
                        ogólnyZasięg.DodajAtrybut(new AtrybutGeoInfo("_s.Jednostka_ewidencyjna.n", jednostkaEwidencyjna));
                        ogólnyZasięg.DodajAtrybut(new AtrybutGeoInfo("_s.Obręb.n", numerObrębu));
                        ogólnyZasięg.DodajPunkt(new PunktOparciaGeoInfo(minX, minY));
                        ogólnyZasięg.DodajPunkt(new PunktOparciaGeoInfo(minX, maxY));
                        ogólnyZasięg.DodajPunkt(new PunktOparciaGeoInfo(maxX, minY));
                        ogólnyZasięg.DodajPunkt(new PunktOparciaGeoInfo(minX, minY));
                        _mapaOgólna.DodajElement(ogólnyZasięg);
                    }
                }
            }
        }

        public void EksportujDoGeomedia(string fileName, bool generalizuj = false)
        {
            var writer = new GeomediaWriter(_mapa);
            writer.Zapisz(fileName);
        }

        public void EksportujDoTango(string fileName, bool generalizuj = false)
        {
            var writer = new TangoWriter(_mapa);
            writer.Zapisz(fileName);
        }
    }
}
