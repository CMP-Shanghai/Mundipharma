using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using MR.SP.DueDiligence.Framework.Const;

namespace MR.SP.DueDiligence.WebPart.WPUPloadButton
{
    [ToolboxItemAttribute(false)]
    public class WPUPloadButton : WebPartBase
    {
        [Personalizable(),WebDisplayName("ListName"), WebBrowsable(true)]
        public string ListName
        {
            get;
            set;
        }

        [Personalizable(), WebDisplayName("Button display name"), WebBrowsable(true)]
        public string DisplayName
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {

            Label lbBtn = new Label();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    try
                    {
                        SPList list = web.Lists.TryGetList(ListName);
                        SPDocumentLibrary dl = list as SPDocumentLibrary;
                        string id = this.Page.Request["ID"];
                        string webUrl = string.Compare(web.ServerRelativeUrl, "/") == 0 ? string.Empty : web.ServerRelativeUrl;
                        Label lbbtnupload = new Label();
                        string btnUpload = JSString.BtnUpLoad.Replace("_webUrl_", webUrl).Replace("_listID_", list.ID.ToString()).Replace("_listName_", dl.RootFolder.Url).Replace("_ID_", id).Replace("_DisplayName_",DisplayName);
                        lbbtnupload.Text = btnUpload;
                        this.Controls.Add(lbbtnupload);
                        this.Page.Controls.Add(lbBtn);
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
            }


        }
    }
}
