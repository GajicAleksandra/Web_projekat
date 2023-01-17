using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class Trener : Korisnik
    {
        public List<int> treninzi { get; set; }
        public FitnesCentar FitnesCentar { get; set; }
        public bool ZabranjenoPrijavljivanje { get; set; }

        public Trener()
        {
            treninzi = new List<int>();
            ZabranjenoPrijavljivanje = false;
            FitnesCentar = new FitnesCentar() { Id = -1 };
        }
    }
}