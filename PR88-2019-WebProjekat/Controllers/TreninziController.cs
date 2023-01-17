using Newtonsoft.Json;
using PR88_2019_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR88_2019_WebProjekat.Controllers
{
    public class TreninziController : ApiController
    {
        public List<GrupniTrening> Get()
        {
            List<GrupniTrening> treninzi = HttpContext.Current.Application["treninzi"] as List<GrupniTrening>;
            return treninzi;
        }

        public IHttpActionResult DodajTrening(GrupniTrening trening)
        {
            List<GrupniTrening> treninzi = HttpContext.Current.Application["treninzi"] as List<GrupniTrening>;
            if (trening == null)
            {
                return BadRequest();
            }
            else
            {
                treninzi.Add(trening);
                SacuvajTreninge();
                return Ok(trening.Id);
            }
        }

        [HttpPut]
        public IHttpActionResult AzuriranjeTreninga([FromBody]GrupniTrening podaci, int id)
        {
            List<GrupniTrening> treninzi = HttpContext.Current.Application["treninzi"] as List<GrupniTrening>;
            var korisnik = (Korisnik)HttpContext.Current.Session["korisnik"];

            foreach (GrupniTrening trening in treninzi)
            {
                foreach (var p in trening.posetioci)
                {
                    if (p.KorisnickoIme == korisnik.KorisnickoIme)
                    {
                        p.treninzi.Add(id);
                    }
                }

                if (trening.Id == id)
                {
                    if (korisnik.Uloga == Uloga.POSETILAC)
                    {
                        if (!trening.posetioci.Contains((Posetilac)korisnik))
                        {
                            trening.posetioci.Add((Posetilac)korisnik);
                            SacuvajTreninge();
                            return Ok(trening.Naziv);
                        }
                    }
                    else if (korisnik.Uloga == Uloga.TRENER)
                    {
                        trening.Naziv = podaci.Naziv;
                        trening.Tip = podaci.Tip;
                        trening.DatumIVreme = podaci.DatumIVreme;
                        trening.MaksPosetilaca = podaci.MaksPosetilaca;
                        trening.Trajanje = podaci.Trajanje;
                        SacuvajTreninge();
                        return Ok(trening.Naziv);
                    }
                    else
                    {
                        trening.FitnesCentar = podaci.FitnesCentar;
                        SacuvajTreninge();
                        return Ok(trening.Naziv);
                    }
                }
            }

            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult ObrisiTrening(int id)
        {
            List<GrupniTrening> treninzi = HttpContext.Current.Application["treninzi"] as List<GrupniTrening>;

            foreach(GrupniTrening trening in treninzi)
            {
                if(trening.Id == id)
                {
                    trening.Obrisan = true;
                    SacuvajTreninge();
                    return Ok(treninzi);
                }
            }
            return NotFound();
        }

        private void SacuvajTreninge()
        {
            JsonSerializer serializer = new JsonSerializer();

            List<GrupniTrening> treninzi = HttpContext.Current.Application["treninzi"] as List<GrupniTrening>;

            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(@"~\App_Data\grupni_treninzi.json")))
            {
                using (JsonWriter jr = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jr, treninzi);
                }
            }
        }
    }
}
