using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class Vlasnik : Korisnik
    {
        public List<int> FitnesCentri { get; set; }

        public Vlasnik()
        {
            FitnesCentri = new List<int>();
        }
    }
}