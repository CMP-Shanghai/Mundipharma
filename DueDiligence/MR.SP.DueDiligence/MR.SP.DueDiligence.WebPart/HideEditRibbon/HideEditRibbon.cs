using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MR.SP.DueDiligence.WebPart.HideEditRibbon
{
    [ToolboxItemAttribute(false)]
    public class HideEditRibbon : System.Web.UI.WebControls.WebParts.WebPart
    {
        private const string ScriptKey = "";
        protected override void CreateChildControls()
        {
            string scriptSnippet = GetScriptString();
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), ScriptKey))
            {
                Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), ScriptKey);
            }
        }

        private string GetScriptString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("function hideEditRibbon() {");
            builder.Append("var ribbon = SP.Ribbon.PageManager.get_instance().get_ribbon();");
            builder.Append("SelectRibbonTab(\"Ribbon.Read\", true);");
            if (SPContext.Current.FormContext.FormMode == SPControlMode.Display)
            {
                builder.Append("ribbon.removeChild('Ribbon.ListForm.Display'); }");
            }
            else
            {
                builder.Append("ribbon.removeChild('Ribbon.ListForm.Edit'); }");
                if (SPContext.Current.FormContext.FormMode == SPControlMode.New)
                {
                    builder.Append("ribbon.removeChild('Ribbon.WebPartPage'); }");
                }
            }
            builder.Append("SP.SOD.executeOrDelayUntilScriptLoaded(function() {");
            builder.Append("var pm = SP.Ribbon.PageManager.get_instance();");
            builder.Append("pm.add_ribbonInited(function() {hideEditRibbon();});");
            builder.Append("var ribbon = null;try {ribbon = pm.get_ribbon();}catch (e) { }");
            builder.Append(
                "if (!ribbon) {if (typeof(_ribbonStartInit) == \"function\")_ribbonStartInit(_ribbon.initialTabId, false, null);}");
            builder.Append("else {hideEditRibbon();}");
            builder.Append("   }, \"sp.ribbon.js\");");
            return builder.ToString();
        }
    }
}
