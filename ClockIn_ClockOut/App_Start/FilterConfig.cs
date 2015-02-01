using System.Web;
using System.Web.Mvc;

namespace ClockIn_ClockOut
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
