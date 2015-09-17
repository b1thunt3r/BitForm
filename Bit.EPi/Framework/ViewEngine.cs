using System.Web.Mvc;

namespace Bit.EPi.Framework
{
    public class ViewEngine : RazorViewEngine
    {
        public ViewEngine()
        {
            string[] LocationFormats =
            { 
                "~/Views/Blocks/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/PagePartials/{0}.cshtml",
                "~/Views/Pages/{1}/{0}.cshtml",
			    "~/Views/Mvc/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml"
		    };

            PartialViewLocationFormats = LocationFormats;
            ViewLocationFormats = LocationFormats;
        }
    }
}