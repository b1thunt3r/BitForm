using Bit.EPi.Property.BitForm;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.XForms;
using System.ComponentModel.DataAnnotations;

namespace Bit.EPi.Models.Pages
{
    [ContentType(DisplayName = "TestPage", GUID = "6ddd0885-c13e-4efa-8dab-7cfc1891fb69", Description = "")]
    public class TestPage : PageData
    {
        [Display(
            Name = "Bit Form",
            Description = "A good alternative to EPiServer XForm.",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual BitForm Form { get; set; }

        [Display(Order = 2)]
        public virtual XForm XForm { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Main body",
            Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual XhtmlString MainBody { get; set; }
    }
}