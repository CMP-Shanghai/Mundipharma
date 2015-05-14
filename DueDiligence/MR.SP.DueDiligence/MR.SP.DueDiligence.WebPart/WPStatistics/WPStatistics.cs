using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using MR.SP.DueDiligence.Framework.Const;

namespace MR.SP.DueDiligence.WebPart.WPStatistics
{
    [ToolboxItemAttribute(false)]
    public class WPStatistics : System.Web.UI.WebControls.WebParts.WebPart
    {
        [Personalizable(), WebDisplayName("Discription"), WebBrowsable(true)]
        public string Discription
        {
            get;
            set;
        }
        protected override void CreateChildControls()
        {
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            base.RenderChildren(writer);
            SPSecurity.RunWithElevatedPrivileges(delegate
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                SPList projectList = web.Lists.TryGetList(ListName.Project);
                                int itemCount = 0;
                                if (projectList != null)
                                {
                                    itemCount = projectList.ItemCount;
                                }
                                //                                SPQuery query = new SPQuery();
                                //                                query.Query = @"<Where>
                                //                                      <Eq>
                                //                                         <FieldRef Name='" + projectList.Fields[Fields.Outcome].InternalName + @"' />
                                //                                         <Value Type='Choice'>Approved</Value>
                                //                                      </Eq>
                                //                                   </Where>";
                                //                                SPListItemCollection items = projectList.GetItems(query);
                                string displayString = "<div id='divListItemBox'><div id='divListItemCount'>" + itemCount + "</div><div id='divListItemDiscription'>" + Discription + "</div><div>";
                                writer.Write(displayString);
                            }
                        }
                    });

        }
    }
}
