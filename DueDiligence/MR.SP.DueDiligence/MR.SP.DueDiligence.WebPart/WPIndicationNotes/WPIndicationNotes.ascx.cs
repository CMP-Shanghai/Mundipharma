using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.DueDiligence.WebPart.WPIndicationNotes
{


    [ToolboxItemAttribute(false)]
    public partial class WPIndicationNotes : WebPartBase
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]



        
        private string listName;
        [Personalizable(), WebDisplayName("List Name"), WebBrowsable(true)]
        public string ListName
        {
            get {
            return listName;
            }
            set { listName = value; }
            
        }


        public WPIndicationNotes()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        SPWeb web;
        SPList list;
        protected void Page_Load(object sender, EventArgs e)
        {
            web = SPContext.Current.Web;
            web.AllowUnsafeUpdates = true;
            //SPFile filePage = web.GetFile(this.Page.Request.Url.ToString());
            if (string.IsNullOrEmpty(ListName))
            {
                return;
            }
            list = web.Lists[ListName];
            if (!this.Page.IsPostBack)
            {

                if (list.RootFolder.Properties.ContainsKey("Notes"))
                {
                    string Notes = (string)list.RootFolder.GetProperty("Notes");
                    txtNotes.Text = Notes;
                }
                else
                {
                    list.RootFolder.AddProperty("Notes", "");
                }

                list.Update();
                list.RootFolder.Update();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Page.Response.Redirect(SPContext.Current.Site.ServerRelativeUrl);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ListName))
            {
                return;
            }
            list.RootFolder.SetProperty("Notes", txtNotes.Text);
            list.Update();
            list.RootFolder.Update();
        }
    }
}
