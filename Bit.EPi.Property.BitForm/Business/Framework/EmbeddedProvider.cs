using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Routing;

namespace Bit.EPi.Property.BitForm
{
    //http://www.danielroot.info/2013/07/reuse-mvc-views-using-virtual-path.html

    public class EmbeddedProvider
    {
        public static void RegisterProviders()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedVirtualPathProvider());
            RouteTable.Routes.Insert(0, new Route(
                EmbeddedResourceRouteHandler.Url,
                new EmbeddedResourceRouteHandler()
            ));
        }
    }

    public class EmbeddedVirtualPathProvider : VirtualPathProvider
    {
        private readonly Assembly assembly = typeof(EmbeddedVirtualPathProvider).Assembly;
        private readonly string[] resourceNames;

        public EmbeddedVirtualPathProvider()
        {
            this.resourceNames = assembly.GetManifestResourceNames();
        }

        private bool IsEmbeddedResourcePath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            var resourceName = this.GetType().Namespace + "." + checkPath.Replace("~/", "").Replace("/", ".");
            return this.resourceNames.Contains(resourceName);
        }

        public override bool FileExists(string virtualPath)
        {
            return IsEmbeddedResourcePath(virtualPath) || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsEmbeddedResourcePath(virtualPath))
            {
                return new EmbeddedVirtualFile(virtualPath);
            }

            return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsEmbeddedResourcePath(virtualPath))
            {
                return null;
            }

            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }

    public class EmbeddedVirtualFile : VirtualFile
    {
        private readonly string virtualPath;
        private readonly Assembly assembly;

        public EmbeddedVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            this.assembly = this.GetType().Assembly;
            this.virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override System.IO.Stream Open()
        {
            var resourceName = this.GetType().Namespace + "." + virtualPath.Replace("~/", "").Replace("/", ".");
            return assembly.GetManifestResourceStream(resourceName);
        }
    }

    public class EmbeddedResourceRouteHandler : IRouteHandler
    {
        public static string Url
        {
            get
            {
                return UrlPrefix + "/" + "{*url}";
            }
        }

        public static string UrlPrefix
        {
            get
            {
                return "Bit/Embedded";
            }
        }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new EmbeddedResourceHttpHandler(requestContext.RouteData);
        }
    }

    public class EmbeddedResourceHttpHandler : IHttpHandler
    {
        private string _File;
        private Assembly _Assembly;

        public EmbeddedResourceHttpHandler(RouteData routeData)
        {
            _File = routeData.Values["url"].ToString();
            _Assembly = typeof(EmbeddedResourceHttpHandler).Assembly;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var ext = Path.GetExtension(_File);
            var nameSpace = _Assembly.GetName().Name;
            var manifestResourceName = string.Format("{0}.{1}", nameSpace, _File.Replace("/", "."));

            context.Response.Clear();

            var stream = _Assembly.GetManifestResourceStream(manifestResourceName);
            if (stream == null)
            {
                context.Response.StatusCode = 404;
            }
            else
            {
                context.Response.ContentType = "text/css";
                if (ext == ".js")
                    context.Response.ContentType = "text/javascript";

                stream.CopyTo(context.Response.OutputStream);
            }
        }
    }
}