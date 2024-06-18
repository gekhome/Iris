using System.Web;
using System.Web.Mvc;
using Iris.Filters;

namespace Iris
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Custom Iris error handler attribute
            filters.Add(new ErrorHandlerFilter());
        }
    }
}
