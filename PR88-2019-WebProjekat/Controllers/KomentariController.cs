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
    public class KomentariController : ApiController
    {
        public List<Komentar> Get()
        {
            List<Komentar> komentari = HttpContext.Current.Application["komentari"] as List<Komentar>;
            return komentari;
        }

        public IHttpActionResult DodajKomentar(Komentar komentar)
        {
            List<Komentar> komentari = HttpContext.Current.Application["komentari"] as List<Komentar>;
            if (komentar == null)
            {
                return BadRequest();
            }
            else
            {
                komentari.Add(komentar);
                SacuvajKomentare();
                return Ok(komentar.Posetilac);
            }
        }

        [HttpPut]
        public IHttpActionResult AzurirajKomentar(IzmenaKomentara komentar, int id)
        {
            List<Komentar> komentari = HttpContext.Current.Application["komentari"] as List<Komentar>;
            bool postoji = false;

            foreach(Komentar k in komentari)
            {
                if(k.FitnesCentar == komentar.StariNazivFC && komentar.IdVlasnika == -1)
                {
                    k.FitnesCentar = komentar.NoviNazivFC;
                    postoji = true;
                }

                if(komentar.IdVlasnika != -1 && k.Id == id)
                {
                    k.Odobren = komentar.Odobren;
                    k.Odbijen = komentar.Odbijen;
                    SacuvajKomentare();
                    return Ok(k.FitnesCentar);
                }
            }

            if (postoji)
            {
                SacuvajKomentare();
            }

            return Ok(komentar.StariNazivFC);
        }

        private void SacuvajKomentare()
        {
            JsonSerializer serializer = new JsonSerializer();

            List<Komentar> komentari = HttpContext.Current.Application["komentari"] as List<Komentar>;

            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(@"~\App_Data\komentari.json")))
            {
                using (JsonWriter jr = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jr, komentari);
                }
            }
        }
    }

    public class IzmenaKomentara
    {
        public string StariNazivFC { get; set; }
        public string NoviNazivFC { get; set; }
        public int IdVlasnika { get; set; }
        public bool Odobren { get; set; }
        public bool Odbijen { get; set; }

        public IzmenaKomentara()
        {
            StariNazivFC = "";
            NoviNazivFC = "";
            IdVlasnika = -1;
        }
    }
}
