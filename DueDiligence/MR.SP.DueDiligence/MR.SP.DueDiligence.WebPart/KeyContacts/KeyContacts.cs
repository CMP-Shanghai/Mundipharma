using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MR.SP.DueDiligence.WebPart.KeyContacts
{
    [ToolboxItemAttribute(false)]
    public class KeyContacts : WebPartBase
    {
        private const string KeyGroupName = "Key Contacts";
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/MR.SP.DueDiligence.WebPart/KeyContacts/KeyContactsUserControl.ascx";

        #region Property
        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("Groups"),
        WebDescription("Group names which need to display")]
        public string UserGroups { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("View Fields"),
        WebDescription("Fields to display")]
        public string ViewFields { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("UserProfile Properties"),
        WebDescription("Properties in user profile. They should be match the sequence listed in view fields property")]
        public string UserProfilePrpoerties { get; set; }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("Email column"),
        WebDescription("Email column anme, split with semi-comma. such as 'Work Email;Email'")]
        public string EmailColumns { get; set; }
        #endregion


        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            control.ID = Guid.NewGuid().ToString();
            if (control != null)
            {
                ((KeyContactsUserControl)control).webPart = this;
                Controls.Add(control);
            }
        }
    }
}
