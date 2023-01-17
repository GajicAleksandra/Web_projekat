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
    public class TreneriController : ApiController
    {
        [HttpGet]
        public List<Trener> Treneri()
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;
            List<Trener> treneri = new List<Trener>();

            foreach (Korisnik korisnik in korisnici)
            {
                if (korisnik.Uloga == Uloga.TRENER)
                {
                    treneri.Add((Trener)korisnik);
                }
            }
            return treneri;
        }

        public IHttpActionResult DodajTrenera(Trener trener)
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;

            if(trener == null)
            {
                return BadRequest();
            }

            foreach(Korisnik k in korisnici)
            {
                if(k.KorisnickoIme == trener.KorisnickoIme)
                {
                    return BadRequest();
                }
            }

            trener.Id = Math.Abs(Guid.NewGuid().GetHashCode());
            korisnici.Add(trener);
            SacuvajKorisnike();
            return Ok(trener.KorisnickoIme);
        }

        [HttpPut]
        public IHttpActionResult AzurirajTrenera(Trener trener, int id)
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;
            bool postoji = false; 

            foreach(Korisnik k in korisnici)
            {
                if(k.Id == id && k.Uloga != Uloga.VLASNIK)
                {
                    if (trener.ZabranjenoPrijavljivanje)
                    {
                        //brisanje fitnes centra
                        if (trener.FitnesCentar.Id == -1)
                        {
                            ((Trener)k).ZabranjenoPrijavljivanje = true;
                            ((Trener)k).FitnesCentar.Obrisan = true;
                        }
                        //blokiranje trenera
                        else
                        {
                            ((Trener)k).ZabranjenoPrijavljivanje = true;
                        }
                    }

                    //dodavanje treninga
                    if (trener.treninzi.Count != 0 && !((Trener)k).treninzi.Contains(trener.treninzi[0]))
                    {
                        ((Trener)k).treninzi.Add(trener.treninzi[0]);
                    }

                    SacuvajKorisnike();
                    return Ok(k.KorisnickoIme);
                }

                //izmena fitnes centra treneru
                if (k.Uloga == Uloga.TRENER)
                {
                    
                    if (((Trener)k).FitnesCentar.Id == trener.FitnesCentar.Id)
                    {
                        ((Trener)k).FitnesCentar = trener.FitnesCentar;
                        postoji = true;
                    }
                }
            }

            if (postoji)
            {
                SacuvajKorisnike();
                return Ok(trener.KorisnickoIme);
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
