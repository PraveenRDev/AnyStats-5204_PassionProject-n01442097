using System.Web;
using System.Web.Mvc;

namespace AnyStats___5204_PassionProject_n01442097
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
