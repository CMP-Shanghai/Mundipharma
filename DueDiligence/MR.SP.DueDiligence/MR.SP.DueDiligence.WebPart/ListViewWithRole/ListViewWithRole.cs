using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MR.SP.DueDiligence.WebPart
{
    [ToolboxItemAttribute(false)]
    public class ListViewWithRole : WebPartBase
    {
        private const string KeyGroupName = "Keys Setting";
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/MR.SP.DueDiligence.WebPart/ListViewWithRole/ListViewWithRoleUserControl.ascx";

        #region Properties
        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("Admin View Key"),
        WebDescription("View Key name for admin, key value can be updated using SharePoint Designer. Leave it as blank if current role should not see this view")]
        public string AdminViewKey { get; set; }
        
        [Personalizable(PersonalizationScope.Shared),
                WebBrowsable, Category(KeyGroupName),
                WebDisplayName("Default Value of Admin View Name"),
                WebDescription("If value of key is not existed, the value provide in this field will apply to web part. Please ensure the view name is exactly the same as what they are in the list.")]
        public string AdminViewDefault { get; set; }


        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("Participant View Key"),
        WebDescription("View Key name for admin, key value can be updated using SharePoint Designer. Leave it as blank if current role should not see this view")]
        public string ParticipantViewKey { get; set; }


        [Personalizable(PersonalizationScope.Shared),
                WebBrowsable, Category(KeyGroupName),
                WebDisplayName("Default Value of Participant View Name"),
                WebDescription("If value of key is not existed, the value provide in this field will apply to web part. Please ensure the view name is exactly the same as what they are in the list.")]
        public string ParticipantViewDefault { get; set; }
        
        
        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable, Category(KeyGroupName),
        WebDisplayName("Normal User View Key"),
        WebDescription("View Key name for admin, key value can be updated using SharePoint Designer. Leave it as blank if current role should not see this view")]
        public string NormalUserViewKey { get; set; }

        [Personalizable(PersonalizationScope.Shared),
                WebBrowsable, Category(KeyGroupName),
                WebDisplayName("Default Value of Normal User View Name"),
                WebDescription("If value of key is not existed, the value provide in this field will apply to web part. Please ensure the view name is exactly the same as what they are in the list.")]
        public string NormalUserViewDefault { get; set; }
        
        #endregion

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            control.ID = Guid.NewGuid().ToString();
            if (control != null)
            {
                ((ListViewWithRoleUserControl)control).webPart = this;
                Controls.Add(control);
            }
        }
    }
}
