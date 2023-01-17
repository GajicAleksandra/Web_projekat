using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PR88_2019_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR88_2019_WebProjekat.Controllers
{
    public class KorisniciController : ApiController
    {
        public IHttpActionResult RegistracijaPosetioca(Posetilac korisnik)
        {
            if (korisnik == null)
            {
                return BadRequest();
            }
            var korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;
            foreach (var k in korisnici)
            {
                if (k.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    return BadRequest(korisnik.KorisnickoIme);
                }
            }
            
            korisnik.Id = Math.Abs(Guid.NewGuid().GetHashCode());
            korisnici.Add(korisnik);
            SacuvajKorisnike();

            return Ok(korisnik);
        }

        public Korisnik Get()
        {
            var korisnik = (Korisnik)HttpContext.Current.Session["korisnik"];
            if (korisnik != null)
            {
                if (korisnik.Uloga == Uloga.TRENER)
                {
                    if (((Trener)korisnik).ZabranjenoPrijavljivanje)
                    {
                        return null;
                    }
                }
            }
            return korisnik;
        }

        [HttpPut]
        public IHttpActionResult AzuriranjeKorisnika([FromBody]IzmenaPodatakaKorisnik podaci, int id)
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;
            foreach(Korisnik k in korisnici)
            {
                if(k.Id == id)
                {
                    if (podaci.Ime != null && podaci.Ime != "")
                    {
                        k.Ime = podaci.Ime;
                    }

                    if (podaci.Prezime != null && podaci.Prezime != "")
                    {
                        k.Prezime = podaci.Prezime;
                    }

                    if (podaci.Email != null && podaci.Email != "")
                    {
                        k.Email = podaci.Email;
                    }

                    if (podaci.Lozinka != null && podaci.Lozinka != "")
                    {
                        k.Lozinka = podaci.Lozinka;
                    }

                    if(k.Uloga == Uloga.POSETILAC && podaci.IdTreninga != 0)
                    {
                        if (!((Posetilac)k).treninzi.Contains(podaci.IdTreninga))
                        {
                            ((Posetilac)k).treninzi.Add(podaci.IdTreninga);
                        }
                    }
                    if(k.Uloga == Uloga.VLASNIK && podaci.IdFitnesCentra != 0)
                    {
                        if (((Vlasnik)k).FitnesCentri.Contains(podaci.IdFitnesCentra))
                        {
                            ((Vlasnik)k).FitnesCentri.Add(podaci.IdFitnesCentra);
                        }
                    }

                    SacuvajKorisnike();
                    return Ok(k);
                }
            }

            return NotFound();
        }

        private void SacuvajKorisnike()
        {
            JsonSerializer serializer = new JsonSerializer();

            List<Korisnik> korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;

            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(@"~\App_Data\korisnici.json")))
            {
                using (JsonWriter jr = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jr, korisnici);
                }
            }
        }
    }
}
