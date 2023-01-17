using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class Komentar
    {
        public int Id { get; set; }
        public string Posetilac { get; set; }
        public string FitnesCentar { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
        public bool Odbijen { get; set; }
        public bool Odobren { get; set; }
        public int IdVlasnika { get; set; }

        public Komentar()
        {
            Id = Math.Abs(Guid.NewGuid().GetHashCode());
            Odbijen = false;
            Odobren = false;
        }
    }
}