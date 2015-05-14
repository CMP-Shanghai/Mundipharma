using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using MR.SP.DueDiligence.Framework.Const;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;
using MR.SP.DueDiligence.Framework;

namespace MR.SP.DueDiligence.WebPart.WPRedirct
{
    [ToolboxItemAttribute(false)]
    public class WPRedirct : WebPartBase
    {
        private string userLandingDefaultValue = "/Pages/NormalUserLandingPage.aspx";
        protected override void CreateChildControls()
        {
            try
            {
                if (DDContext.Current.LoginUser.IsNormalUser)
                {
                    string redirectUrl = string.Format("{0}{1}", SPContext.Current.Web.Url, DDUtility.GetPropertyValue(PropertiesKey.NormalUserLandingUrl, userLandingDefaultValue));
                    this.Page.Response.Redirect(redirectUrl);
                }
            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(ex.Message);
            }
        }
    }
}
