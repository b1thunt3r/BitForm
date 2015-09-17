using Bit.EPi.Framework;
using Bit.EPi.Property.BitForm;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bit.EPi
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            EmbeddedProvider.RegisterProviders();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
        }
    }
}