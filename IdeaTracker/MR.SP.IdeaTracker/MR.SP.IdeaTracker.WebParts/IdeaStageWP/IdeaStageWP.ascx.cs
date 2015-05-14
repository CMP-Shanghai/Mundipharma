using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.IdeaTracker.WebParts.IdeaStageWP
{
    /// <summary>
    /// Author:	Young Liu
    /// Create date: 2014-11-12
    /// Description: Right Stage Panel in Idea view form Page
    /// </summary>
    [ToolboxItemAttribute(false)]
    public partial class IdeaStageWP : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public IdeaStageWP()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindParti();
            }
        }

        void BindParti()
        {
            SPListItem item = SPContext.Current.List.GetItemById(SPContext.Current.ItemId);
            SPFieldUserValueCollection uvs = new SPFieldUserValueCollection(SPContext.Current.Web, item["IParticipants"].ToString());
            string partiStr = "Empty";
            if (uvs.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var uv in uvs)
                {
                    sb.AppendFormat("<li>{0}</li>", uv.User.Name);
                }
                partiStr = string.Format("<ul>{0}</ul>", sb.ToString());
            }
            lbParti.Text = partiStr;
        }
    }
}
