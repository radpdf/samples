using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CS_MVC4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            // Add ignore route for RAD PDF HTTP Handler
            // (NOTE: MVC's default axd ignore route doesn't work for sub-directories) 
            // (NOTE: This must be added above "MapRoute")
            // (NOTE: If using an MVC Area, you must also ignore this route in its RegisterArea() method of its AreaRegistration class)
            routes.IgnoreRoute("{*radpdfaxd}", new { radpdfaxd = @".*/RadPdf\.axd(\?.*)?" });
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}