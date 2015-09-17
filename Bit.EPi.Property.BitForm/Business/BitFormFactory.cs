using System;
using System.Collections.Generic;
using System.Linq;

namespace Bit.EPi.Property.BitForm
{
    public class BitFormFactory
    {
        #region Instance
        private static BitFormFactory _Instance;

        public static BitFormFactory Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BitFormFactory();

                return _Instance;
            }
        }
        #endregion

        #region Folder

        public Folder AddFolder(string name, string rootId = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid parameter", "name");

            var f = new Folder()
            {
                Name = name,
                Root = Folder.GetRoot(rootId)
            };

            f.Save();

            return f;
        }

        public Folder GetFolder(string id)
        {
            return Folder.Get(id);
        }

        public IEnumerable<Folder> GetFolders()
        {
            return Folder.GetAll();
        }

        public bool DeleteFolder(string id)
        {
            var f = Folder.Get(id);
            var sub = Folder.Where(x => x.Root == f).ToList();
            if (sub.Count > 0)
            {
                return false;
            }

            f.Delete();
            return true;
        }

        public Folder UpdateFolder(string id, string name = "", string rootId = "")
        {
            var f = Folder.Get(id);

            if (!string.IsNullOrEmpty(name))
                f.Name = name;
            if (!string.IsNullOrEmpty(rootId))
                f.Root = Folder.GetRoot(rootId);

            f.Save();

            return f;
        }

        public Folder GetRootFolder()
        {
            return Folder.GetRoot();
        }

        #endregion

        #region Tree
        public object GetTree()
        {
            var root = Folder.GetRoot();

            return new
            {
                root = root,
                folders = Folder.Where(x => x.Id.ExternalId != root.Id.ExternalId),
                docs = Form.GetAll().Select(x => new Form()
                {
                    FormName = x.FormName,
                    Id = x.Id,
                    Root = x.Root
                }).OrderBy(x => x.FormName)
            };
        }

        public IEnumerable<BitFormDojo> GetDojoTree()
        {
            var dojo = new List<BitFormDojo>();

            foreach (var item in Folder.GetAll())
            {
                dojo.Add(new BitFormDojo()
                {
                    id = "_" + item.Id.StoreId,
                    name = item.Name,
                    parent = item.Root != null ? "_" + item.Root.Id.StoreId : "",
                    type = item.Root != null ? "folder" : "root"
                });
            }
            foreach (var item in Form.GetAll())
            {
                dojo.Add(new BitFormDojo()
                {
                    id = "_" + item.Id.StoreId,
                    name = item.FormName,
                    parent = "_" + item.Root.Id.StoreId,
                    type = "form"
                });
            }

            return dojo.OrderBy(x => x.name);
        }
        #endregion

        #region From
        public Form AddForm(string name, string rootId = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid parameter", "name");

            var f = new Form()
            {
                FormName = name,
                Root = Folder.GetRoot(rootId)
            };

            f.Save();

            return f;
        }

        public Form GetForm(string id)
        {
            return Form.Get(id);
        }

        public IEnumerable<Form> GetForms()
        {
            return Form.GetAll();
        }

        public bool DeleteForm(string id)
        {
            var f = Form.Get(id);
            // TODO Need to check if the form is used somewhere.

            //var sub = Form.Where(x => x.Root == f).ToList();
            //if (sub.Count > 0)
            //{
            //    return false;
            //}

            f.Delete();
            return true;
        }

        public Form UpdateForm(string id, string name)
        {
            var f = Form.Get(id);
            f.FormName = name;

            f.Save();

            return f;
        }
        #endregion
    }
}
