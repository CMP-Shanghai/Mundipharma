using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MR.SP.DueDiligence.WebPart.WPCopyButton
{
    [ToolboxItemAttribute(false)]
    public class WPCopyButton : WebPartBase
    {

        [Personalizable(), WebDisplayName("Destination Library Name"), WebBrowsable(true)]
        public string DestinatinListName
        {
            get;
            set;
        }

        [Personalizable(), WebDisplayName("Start Source Library Name"), WebBrowsable(true)]
        public string StartSourceLibrary
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

        private const string HTML_ClientControl =
            "<button onclick='InitCopyFile(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{6}\");' type='button' value='{5}'><button/>"
            + "<input id='{4}' type='text' style='display: none;'/> "
            +"<button id='{7}' style='display:none' onclick='return false;' />";

        protected override void CreateChildControls()
        {

            Label lbBtn = new Label();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    try
                    {
                        ScriptManager.RegisterClientScriptInclude(this.Page, GetType(), Guid.NewGuid().ToString(), site.RootWeb.Url + "/_layouts/15/AssetPickers.js");
                        ScriptManager.RegisterClientScriptInclude(this.Page, GetType(), Guid.NewGuid().ToString(), site.RootWeb.Url + "/style%20library/diligenceportal/scripts/MR.SP.DueDiligence.CopyFile.js");
                        
                        string clientHiddenControlId = "MR_CopyItemUrl";
                        string refeshButtonId = "MR_RefeshButton";
                        string id = this.Page.Request["ID"];
                        Label lbbtnupload = new Label();
                        string btnUpload = string.Format(HTML_ClientControl, DestinatinListName, id, clientHiddenControlId, StartSourceLibrary, clientHiddenControlId, DisplayName, refeshButtonId, refeshButtonId);
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
