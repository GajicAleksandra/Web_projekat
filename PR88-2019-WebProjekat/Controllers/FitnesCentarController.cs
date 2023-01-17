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
    public class FitnesCentarController : ApiController
    {
        public List<FitnesCentar> Get()
        {
            List<FitnesCentar> fitnesCentri = HttpContext.Current.Application["fitnesCentri"] as List<FitnesCentar>;
            return fitnesCentri;
        }

        public IHttpActionResult DodajFitnesCentar(FitnesCentar fitnesCentar)
        {
            List<FitnesCentar> fitnesCentri = HttpContext.Current.Application["fitnesCentri"] as List<FitnesCentar>;

            if(fitnesCentar == null)
            {
                return NotFound();
            }

            foreach (FitnesCentar f in fitnesCentri)
            {
                if(f.Vlasnik.Id == fitnesCentar.Vlasnik.Id)
                {
                    f.Vlasnik.FitnesCentri.Add(fitnesCentar.Id);
                }
            }

            fitnesCentar.Vlasnik.FitnesCentri.Add(fitnesCentar.Id);
            fitnesCentri.Add(fitnesCentar);
            SacuvajFitnesCentre();
            return Ok(fitnesCentar.Id);
        }

        [HttpPut]
        public IHttpActionResult AzurirajFitnesCentar([FromBody]FitnesCentar fitnesCentar, int id)
        {
            List<FitnesCentar> fitnesCentri = HttpContext.Current.Application["fitnesCentri"] as List<FitnesCentar>;

            foreach(FitnesCentar f in fitnesCentri)
            {
                if(f.Id == id)
                {
                    f.Naziv = fitnesCentar.Naziv;
                    f.Adresa = fitnesCentar.Adresa;
                    f.GodinaOtvaranja = fitnesCentar.GodinaOtvaranja;
                    f.MesecnaClanarina = fitnesCentar.MesecnaClanarina;
                    f.GodisnjaClanarina = fitnesCentar.GodisnjaClanarina;
                    f.CenaTreninga = fitnesCentar.CenaTreninga;
                    f.CenaGrupnogTreninga = fitnesCentar.CenaGrupnogTreninga;
                    f.CenaTreningaSaPTrenerom = fitnesCentar.CenaTreningaSaPTrenerom;

                    SacuvajFitnesCentre();
                    return Ok(fitnesCentar.Naziv);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult ObrisiFitnesCentar(int id)
        {
            List<FitnesCentar> fitnesCentri = HttpContext.Current.Application["fitnesCentri"] as List<FitnesCentar>;
            foreach(FitnesCentar f in fitnesCentri)
            {
                if(f.Id == id)
                {
                    f.Obrisan = true;
                    SacuvajFitnesCentre();
                    return Ok();
                }
            }
            return NotFound();
        }

        private void SacuvajFitnesCentre()
        {
            JsonSerializer serializer = new JsonSerializer();

            List<FitnesCentar> fitnesCentri = HttpContext.Current.Application["fitnesCentri"] as List<FitnesCentar>;

            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(@"~\App_Data\fitnes_centri.json")))
            {
                using (JsonWriter jr = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jr, fitnesCentri);
                }
            }
        }
    }
}
