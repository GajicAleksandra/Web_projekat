using System.Web;
using System.Web.Mvc;

namespace PR88_2019_WebProjekat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
