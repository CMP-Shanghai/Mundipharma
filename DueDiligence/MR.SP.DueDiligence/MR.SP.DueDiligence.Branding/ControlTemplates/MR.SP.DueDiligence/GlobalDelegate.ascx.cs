using Microsoft.SharePoint;
using MR.SP.DueDiligence.Framework.Const;
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.DueDiligence.Branding.ControlTemplates.MR.SP.DueDiligence.Branding
{
    public partial class GlobalDelegate : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (!IsPostBack)
            {
                //register script into page head
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("var {0} = \"{1}\";", ScriptVariableName.GlobalVariableSiteId, SPContext.Current.Site.ID));//Add Site Id
                sb.Append(string.Format("var {0} = \"{1}\";", ScriptVariableName.GlobalVariableWebId, SPContext.Current.Web.ID));//Add Web Id
                sb.Append(string.Format("var {0} = \"{1}\";", ScriptVariableName.GlobalVariableUserAccount, SPContext.Current.Web.CurrentUser.LoginName));//Add Web Id
                sb.Append(string.Format("var {0} = \"{1}\";", ScriptVariableName.GlobalVariableWebUrl, SPContext.Current.Web.ServerRelativeUrl));//Add Web Id
                sb.Append(string.Format("var {0} = \"{1}\";", ScriptVariableName.GlobalVariableFullUrl, HttpContext.Current.Request.Url.AbsoluteUri));//Add Web Id
                
                string scriptSnippet = sb.ToString();
                string scriptContent = string.Format("<script type='text/javascript'>{0}</script>", scriptSnippet);

                const string scriptKey = "DD_Global_Variables";
                if (!Page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), scriptKey, scriptContent);
                }
            }
        }
    }
}
