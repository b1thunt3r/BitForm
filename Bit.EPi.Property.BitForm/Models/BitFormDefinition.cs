using System;
using System.Collections.Generic;
using System.Linq;

namespace Bit.EPi.Property.BitForm
{
    public enum FieldType
    {
        Text,
        Textarea,
        Dropdown,
        Radio,
        Checkbox,
        Button,
        Heading,
        Ruler
    }

    public enum TextValidate
    {
        Date,
        Email,
        Number,
        PositiveNumber
    }

    public enum ButtonActionType
    {
        Database,
        Email,
        DatabaseAndEmail,
        Url
    }

    public class BitForm
    {
        public virtual string FormId { get; set; }
        public virtual string FormName { get; set; }
    }

    public class BitFormDojo
    {
        public virtual string id { get; set; }
        public virtual string name { get; set; }
        public virtual string parent { get; set; }
        public virtual string type { get; set; }
    }

    public class Form : BitFormDynamicData<Form>
    {
        public string FormName { get; set; }

        private IList<Field> _Definition = new List<Field>();
        public IList<Field> Definition
        {
            get { return _Definition; }
            set { _Definition = value; }
        }
        public Folder Root { get; set; }

        public Form()
            : base()
        {
            //Root 
        }

        public override string ToString()
        {
            return string.Format("[{1}] {0}", FormName, Id.StoreId);
        }

    }

    public class Field
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
        public string CssClass { get; set; }
        public bool Required { get; set; }
        public int SortOrder { get; set; }
        public FieldType ElementType { get; set; }
        public string ElementName { get; set; }
        private IList<FieldChoice> _Choice = new List<FieldChoice>();
        public IList<FieldChoice> Choice
        {
            get { return _Choice; }
            set { _Choice = value; }
        }
        public ButtonAction Action { get; set; }
        public string Value { get; set; }
    }

    public class FieldChoice
    {
        public string Label { get; set; }
        public bool Selected { get; set; }
        public string ElementId { get; set; }
        public string Value { get; set; }
    }

    public class ButtonAction
    {
        public ButtonActionType ActionType { get; set; }
        public Uri CustomUrl { get; set; }
        public string EmailTemplate { get; set; }
    }

    public class Folder : BitFormDynamicData<Folder>
    {
        public string Name { get; set; }
        public Folder Root { get; set; }

        public static Folder GetRoot()
        {
            var root = Folder.Where(x => x.Name == "Root" && x.Root == null).FirstOrDefault();
            if (root == null)
            {
                root = new Folder()
                {
                    Name = "Root",
                    Root = null
                };

                root.Save();
            }

            return root;
        }

        public static Folder GetRoot(string id)
        {
            Folder root = null;

            if (!string.IsNullOrEmpty(id))
                root = Folder.Where(x => x.Id == EPiServer.Data.Identity.Parse(id)).FirstOrDefault();
            if (root == null)
                root = GetRoot();

            return root;
        }
    }

    public class Data : BitFormDynamicData<Data>
    {
        public Form FormData { get; set; }
    }
}
