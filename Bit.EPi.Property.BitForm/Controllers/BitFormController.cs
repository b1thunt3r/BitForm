using System.Web.Mvc;

namespace Bit.EPi.Property.BitForm
{
    [Authorize]
    [RoutePrefix("reviews")]
    public class BitFormController : Controller
    {
        BitFormFactory _Factory = BitFormFactory.Instance;

        #region Folder
        public JsonResult AddFolder(string name, string rootId = null)
        {
            _Factory.AddFolder(name, rootId);

            return this.Json(new
            {
                success = true,
                message = string.Format("Folder Created")
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFolder(string id)
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched folder",
                data = _Factory.GetFolder(id)
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRootFolder()
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched root folder",
                data = _Factory.GetRootFolder()
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFolders()
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched all folders",
                data = _Factory.GetFolders()
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFolder(string id)
        {
            object data;
            if (_Factory.DeleteFolder(id))
                data = new
                {
                    success = true,
                    message = string.Format("Folder Deleted")
                };
            else
                data = new
                {
                    success = false,
                    message = string.Format("Couldn't delete folder. Check if folder is empty.")
                };
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateFolder(string id, string name, string rootId)
        {
            _Factory.UpdateFolder(id, name, rootId);

            return this.Json(new
            {
                success = true,
                message = string.Format("Folder Updates")
            },
            JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Form
        public JsonResult AddForm(string name, string rootId = null)
        {
            _Factory.AddForm(name, rootId);

            return this.Json(new
            {
                success = true,
                message = string.Format("Form Created")
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetForm(string id)
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched form",
                data = _Factory.GetForm(id)
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetForms()
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched all forms",
                data = _Factory.GetForms()
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteForm(string id)
        {
            object data;
            if (_Factory.DeleteForm(id))
                data = new
                {
                    success = true,
                    message = string.Format("Form Deleted")
                };
            else
                data = new
                {
                    success = false,
                    message = string.Format("Couldn't delete form. Check if form is used somewhere.")
                };
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateForm(string id, string name)
        {
            _Factory.UpdateForm(id, name);

            return this.Json(new
            {
                success = true,
                message = string.Format("Form Updates")
            },
            JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Tree
        public JsonResult GetTree()
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched tree",
                data = _Factory.GetTree()
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDojoTree()
        {
            return this.Json(new
            {
                success = true,
                message = "Fetched tree",
                data = _Factory.GetDojoTree()
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Designer

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Test()
        {
            return this.View();
        }

        #endregion
    }
}
