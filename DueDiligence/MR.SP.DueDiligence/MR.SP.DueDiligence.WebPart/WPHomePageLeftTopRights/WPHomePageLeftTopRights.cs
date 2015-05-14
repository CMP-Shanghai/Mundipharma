using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MR.SP.DueDiligence.WebPart.WPHomePageLeftTopRights
{
    [ToolboxItemAttribute(false)]
    public class WPHomePageLeftTopRights : System.Web.UI.WebControls.WebParts.WebPart
    {
        protected override void CreateChildControls()
        {
            using (SPSite site=SPContext.Current.Site)
            {
                using (SPWeb web=site.OpenWeb())
                {
                    
                    if (SPContext.Current.Web.CurrentUser.IsSiteAdmin||IsAdminGroup(web))
                    {
                        
                    }
                }
            }
           
        }


        private bool IsAdminGroup(SPWeb web)
        {
            bool bl = false;
            foreach (SPGroup g in web.CurrentUser.Groups)
            {
                if (g.Name.ToLower() == "duediligenceadmin")
                {
                    bl = true;
                    break;
                }
            }
            return bl;
        }

    }
}
