using Bit.EPi.Models.Pages;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace Bit.EPi.Controllers.Pages
{
    public class TestPageController : PageController<TestPage>
    {
        public ActionResult Index(TestPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            ViewBag.Title = currentPage.Name;

            return View(currentPage);
        }
    }
}