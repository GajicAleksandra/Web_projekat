using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR88_2019_WebProjekat.Models
{
    public class GrupniTrening
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Tip { get; set; }
        public FitnesCentar FitnesCentar { get; set; }
        public int Trajanje { get; set; }
        public DateTime DatumIVreme { get; set; }
        public int MaksPosetilaca { get; set; }
        public bool Obrisan { get; set; }
        public List<Posetilac> posetioci { get; set; }

        public GrupniTrening()
        {
            Id = Math.Abs(Guid.NewGuid().GetHashCode());
            Obrisan = false;
            posetioci = new List<Posetilac>();
            FitnesCentar = new FitnesCentar();
        }

        public GrupniTrening(string naziv, string tip, FitnesCentar centar, int trajanje, DateTime datum, int maksPosetilaca)
        {
            this.Naziv = naziv;
            this.Tip = tip;
            this.FitnesCentar = centar;
            this.Trajanje = trajanje;
            this.DatumIVreme = datum;
            this.MaksPosetilaca = maksPosetilaca;
            Obrisan = false;
            posetioci = new List<Posetilac>();
        }
    }
}