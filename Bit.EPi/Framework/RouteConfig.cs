using System.Web.Mvc;
using System.Web.Routing;

namespace Bit.EPi.Framework
{
    // http://www.codeproject.com/Articles/624181/Routing-Basics-in-ASP-NET-MVC
    // http://www.codeproject.com/Articles/641783/Customizing-Routes-in-ASP-NET-MVC
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
#if DEBUG
            routes.IgnoreRoute("{*browserlink}", new { browserlink = @".*__browserLink.*" });
#endif
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                "defaultMvcRoute",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}