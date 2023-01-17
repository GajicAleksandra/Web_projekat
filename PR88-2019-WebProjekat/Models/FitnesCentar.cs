using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class FitnesCentar
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int GodinaOtvaranja { get; set; }
        public Vlasnik Vlasnik { get; set; }
        public double MesecnaClanarina { get; set; }
        public double GodisnjaClanarina { get; set; }
        public double CenaTreninga { get; set; }
        public double CenaGrupnogTreninga { get; set; }
        public double CenaTreningaSaPTrenerom { get; set; }
        public bool Obrisan { get; set; }

        public FitnesCentar()
        {
            Id = Math.Abs(Guid.NewGuid().GetHashCode());
            Vlasnik = new Vlasnik();
            Obrisan = false;
        }
    }
}