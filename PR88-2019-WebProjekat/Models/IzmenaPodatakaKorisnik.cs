using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class IzmenaPodatakaKorisnik
    {
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public int IdTreninga { get; set; }
        public int IdFitnesCentra { get; set; }
        public bool ObrisanFitnesCentar { get; set; }
        public FitnesCentar FitnesCentar { get; set; }

        public IzmenaPodatakaKorisnik()
        {
            KorisnickoIme = "";
            Ime = "";
            Prezime = "";
            Email = "";
            Lozinka = "";
            IdTreninga = 0;
            IdFitnesCentra = 0;
            ObrisanFitnesCentar = false;
            FitnesCentar = null;
        }
    }
}