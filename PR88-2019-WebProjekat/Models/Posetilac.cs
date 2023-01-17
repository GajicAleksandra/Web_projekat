using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class Posetilac : Korisnik
    {
        public List<int> treninzi { get; set; }

        public Posetilac()
        {
            treninzi = new List<int>();
        }
    }
}