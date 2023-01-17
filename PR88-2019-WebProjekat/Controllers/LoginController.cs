using PR88_2019_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR88_2019_WebProjekat.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(LoginPodaci podaci)
        {
            var korisnici = HttpContext.Current.Application["korisnici"] as List<Korisnik>;
            foreach (Korisnik k in korisnici)
            {
                if (k.KorisnickoIme == podaci.KorisnickoIme && k.Lozinka == podaci.Lozinka)
                {
                    if (k.Uloga == Uloga.POSETILAC)
                    {
                        HttpContext.Current.Session["korisnik"] = (Posetilac)k;
                    }
                    else if (k.Uloga == Uloga.TRENER)
                    {
                        if (((Trener)k).ZabranjenoPrijavljivanje)
                        {
                            HttpContext.Current.Session["korisnik"] = null;
                            return BadRequest(((Trener)k).FitnesCentar.Naziv);
                        }
                        else
                        {
                            HttpContext.Current.Session["korisnik"] = (Trener)k;
                        }
                    }
                    else
                    {
                        HttpContext.Current.Session["korisnik"] = (Vlasnik)k;
                    }

                    return Ok(k.Uloga);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.Session["korisnik"] = null;
            return Ok();
        }
    }
}
