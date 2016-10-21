using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoInfoReader
{
    /// <summary>
    /// Obiekt mapy GEO-INFO.
    /// </summary>
    public class ElementGeoInfo
    {
        #region Nagłówek obiektu
        public string Kod;
        public int Typ;
        public string Id;
        public double? Obrót;
        public double? Szerokość;
        #endregion

        #region Lista punktów oparcia obiektu
        public IEnumerable<PunktOparciaGeoInfo> Punkty { get { return _punkty; } }
        List<PunktOparciaGeoInfo> _punkty = new List<PunktOparciaGeoInfo>();
        #endregion

        #region Lista atrybutów i ich wartości
        public string NumerJednostki
        {
            get
            {
                var atrybutNumerObrębu = Atrybuty.SingleOrDefault(a => "_s.Jednostka_ewidencyjna.n".Equals(a.Nazwa));
                var numerObrębu = atrybutNumerObrębu == null ? string.Empty : atrybutNumerObrębu.Wartość;
                return numerObrębu;
            }
        }
        public string NumerObrębu
        {
            get
            {
                var atrybutNumerObrębu = Atrybuty.SingleOrDefault(a => "_s.Obręb.n".Equals(a.Nazwa));
                var numerObrębu = atrybutNumerObrębu == null ? string.Empty : atrybutNumerObrębu.Wartość;
                return numerObrębu;
            }
        }
        public string NumerZasobu
        {
            get
            {
                var atrybutNumerZasobu = Atrybuty.SingleOrDefault(a => "NRI".Equals(a.Nazwa));
                var numerZasobu = atrybutNumerZasobu == null ? string.Empty : atrybutNumerZasobu.Wartość;
                return numerZasobu;
            }
        }

        public string Operat
        {
            get
            {
                //KERG
                var atrybutKERG = Atrybuty.SingleOrDefault(a => "KRG.n".Equals(a.Nazwa));
                var krg = atrybutKERG == null ? string.Empty : atrybutKERG.Wartość;
                //Jednostka segregująca może zawierać numer operatu: C,JSG=188/15/32/67
                var atrybutJRG = Atrybuty.SingleOrDefault(a => "JSG".Equals(a.Nazwa));
                var jrg = atrybutJRG == null ? string.Empty : atrybutJRG.Wartość;
                if (!string.IsNullOrEmpty(krg)) return krg;
                else return jrg;
            }
        }
        public IEnumerable<AtrybutGeoInfo> Atrybuty { get { return _atrybuty; } }
        List<AtrybutGeoInfo> _atrybuty = new List<AtrybutGeoInfo>();
        #endregion

        #region Etykieta i jej położenie
        public IEnumerable<EtykietaGeoInfo> Etykiety { get { return _etykiety; } }
        List<EtykietaGeoInfo> _etykiety = new List<EtykietaGeoInfo>();
        #endregion

        #region Wykaz identyfikatorów obiektów, dla których obiekt jest nadrzędny
        public IEnumerable<RelacjaGeoInfo> Obiekty { get { return _obiektyPodrzędne; } }
        List<RelacjaGeoInfo> _obiektyPodrzędne = new List<RelacjaGeoInfo>();
        #endregion

        public ElementGeoInfo(string kod, int typ)
        {
            if (string.IsNullOrEmpty(kod)) throw new ArgumentException("Kod obiektu poza zakresem: " + kod);
            Kod = kod;
            if (typ < 1 || typ > 5) throw new ArgumentException("Typ obiektu poza zakresem 1-5: " + typ);
            Typ = typ;
        }

        public void UsuńPunkty() { _punkty.Clear(); }

        public void DodajPunkt(PunktOparciaGeoInfo punkt)
        {
            _punkty.Add(punkt);
        }

        public void DodajAtrybut(AtrybutGeoInfo atrybut)
        {
            _atrybuty.Add(atrybut);
        }
    }
}
