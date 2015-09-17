using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace Bit.EPi.Property.BitForm
{
    [EditorDescriptorRegistration(TargetType = typeof(BitForm), UIHint = BitFormUIHints.BitForm)]
    public class BitFormDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(EPiServer.Shell.ObjectEditing.ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "bit.editors.BitForm";

            //var LivePath = "/BitForm";
            var LivePath = "/BitForm";

            this.EditorConfiguration["FormLivePath"] = LivePath;
            this.EditorConfiguration["FormSaveDef"] = string.Format("{0}/SaveDefinition/", LivePath);
            this.EditorConfiguration["FormGetDef"] = string.Format("{0}/GetDefinition/", LivePath);
            this.EditorConfiguration["FormSaveData"] = string.Format("{0}/SaveData/", LivePath);
            this.EditorConfiguration["FormGetData"] = string.Format("{0}/GetData/", LivePath);
            this.EditorConfiguration["FormViewData"] = string.Format("{0}/ViewData/", LivePath);

            base.ModifyMetadata(metadata, attributes);
        }
    }
}
