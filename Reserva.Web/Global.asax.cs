using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Reserva.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // Use the container and the NinjectDependencyResolver as
            // application's resolver
            var resolver = new NinjectDependencyResolver(NinjectWebCommon.Kernel);

            //Register Resolver for MVC
            DependencyResolver.SetResolver(resolver);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;    

            GlobalConfiguration.Configure(WebApiConfig.Register);//WEB API 1st
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);//MVC 2nd
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}