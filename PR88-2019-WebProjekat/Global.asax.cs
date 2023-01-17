using Newtonsoft.Json;
using PR88_2019_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace PR88_2019_WebProjekat
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UcitajKorisnike();
            UcitajFitnesCentre();
            UcitajTreninge();
            UcitajKomentare();
        }

        private void UcitajKorisnike()
        {
            JsonConverter[] converters = { new KorisnikConverter() };
            List<Korisnik> korisnici;
            using (StreamReader sr = new StreamReader(Server.MapPath(@".\App_Data\korisnici.json")))
            {
                var podaci = sr.ReadToEnd();
                korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(podaci, new JsonSerializerSettings() { Converters = converters });
            }

            HttpContext.Current.Application["korisnici"] = korisnici;
        }

        private void UcitajFitnesCentre()
        {
            List<FitnesCentar> fitnesCentri;
            using (StreamReader sr = new StreamReader(Server.MapPath(@".\App_Data\fitnes_centri.json")))
            {
                string podaci = sr.ReadToEnd();
                fitnesCentri = JsonConvert.DeserializeObject<List<FitnesCentar>>(podaci);
            }

            HttpContext.Current.Application["fitnesCentri"] = fitnesCentri;
        }

        private void UcitajTreninge()
        {
            List<GrupniTrening> treninzi;
            using (StreamReader sr = new StreamReader(Server.MapPath(@".\App_Data\grupni_treninzi.json")))
            {
                string podaci = sr.ReadToEnd();
                treninzi = JsonConvert.DeserializeObject<List<GrupniTrening>>(podaci);
            }

            HttpContext.Current.Application["treninzi"] = treninzi;

        }

        private void UcitajKomentare()
        {
            List<Komentar> komentari;
            using (StreamReader sr = new StreamReader(Server.MapPath(@".\App_Data\komentari.json")))
            {
                string podaci = sr.ReadToEnd();
                komentari = JsonConvert.DeserializeObject<List<Komentar>>(podaci);
            }

            HttpContext.Current.Application["komentari"] = komentari;
        }

        private void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}
