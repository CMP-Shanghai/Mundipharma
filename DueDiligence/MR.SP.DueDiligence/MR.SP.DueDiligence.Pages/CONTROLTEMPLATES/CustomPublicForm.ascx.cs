using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.DueDiligence.Pages.CONTROLTEMPLATES
{
    public partial class CustomPublicForm : UserControl
    {
        public string itemID
        {
            get { return SPContext.Current.ItemId.ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ControlCollection controls = this.Page.Controls;
            //foreach (var item in controls)
            //{
            //    if (item is FormField)
            //    {
            //        ((FormField)item).ControlMode = SPControlMode.Edit;
            //    }
            //}
        }
    }
}
