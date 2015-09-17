using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;
using System;

namespace Bit.EPi.Property.BitForm
{
    [EditorHint(BitFormUIHints.BitForm)]
    [PropertyDefinitionTypePlugIn(Description = BitFormUIHints.Description, DisplayName = BitFormUIHints.DisplayName)]
    public class BitFormProperty : PropertyLongString
    {

        public override Type PropertyValueType
        {
            get
            {
                return typeof(BitForm);
            }
        }

        public override object SaveData(PropertyDataCollection properties)
        {
            return LongString;
        }

        public override object Value
        {
            get
            {
                var value = base.Value as string;
                if (string.IsNullOrEmpty(value))
                    return null;

                var vl = value.Split(new string[] { "||#||" }, StringSplitOptions.RemoveEmptyEntries);
                return new BitForm()
                {
                    FormName = vl[0],
                    FormId = vl[1]
                };
            }
            set
            {
                if (value is BitForm)
                {
                    var vl = (BitForm)value;
                    base.Value = string.Format("{0}||#||{1}", vl.FormName, vl.FormId);
                }
                else
                    base.Value = value;
            }
        }

        public override IPropertyControl CreatePropertyControl()
        {
            //No support for legacy edit mode
            return null;
        }

        public bool RenderAsBlockElement
        {
            get { return true; }
        }
    }
}
