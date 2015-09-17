using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Mvc;

namespace Bit.EPi.Framework
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FrameworkInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized
            ViewEngines.Engines.Insert(0, new ViewEngine());
        }

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }
    }
}