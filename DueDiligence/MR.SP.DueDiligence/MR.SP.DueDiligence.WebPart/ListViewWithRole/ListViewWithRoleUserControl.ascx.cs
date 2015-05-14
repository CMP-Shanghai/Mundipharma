using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using MR.SP.DueDiligence.Framework;
using MR.SP.DueDiligence.Framework.Const;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.DueDiligence.WebPart
{
    public partial class ListViewWithRoleUserControl : UserControl
    {
        public ListViewWithRole webPart;
        private Microsoft.SharePoint.WebControls.ListView lvControl;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!IsPostBack)
                {
                    List<string> viewNameKeyList = new List<string> {
                                                            webPart.AdminViewKey,
                                                            webPart.ParticipantViewKey, 
                                                            webPart.NormalUserViewKey};

                    List<string> defaultViewNameList = new List<string> { 
                                                            webPart.AdminViewDefault, 
                                                            webPart.ParticipantViewDefault,
                                                            webPart.NormalUserViewDefault};
                    if (IsEmptyList(viewNameKeyList) && IsEmptyList(defaultViewNameList))
                    {
                        if (lvControl != null)
                            Controls.Remove(lvControl);
                    }
                    else
                    {
                        lvControl = new Microsoft.SharePoint.WebControls.ListView();
                        //Switch user based on permission
                        SwitchViews(viewNameKeyList, defaultViewNameList);
                        Controls.Add(lvControl);
                    }
                }
            }
            catch (Exception ex)
            {
                if (lvControl != null) lvControl.Visible = false;
                lbError.Visible = true;
                lbError.Text = ex.Message;
                //SPUtility.LogCustomAppError(ex.ToString());
                //SPUtility.TransferToErrorPage(ex.Message);

            }
        }


        /// <summary>
        /// Switch views based on different role
        /// </summary>
        private void SwitchViews(List<string> viewNameKeyList, List<string> defaultViewNameList)
        {
            Guid listId = Guid.Empty;
            Guid viewId = Guid.Empty;

            #region Get View Id and List Id


            List<string> viewNameList = MR.SP.DueDiligence.Framework.DDUtility.GetPropertyValues(viewNameKeyList, defaultViewNameList);

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPList projectList = web.Lists.TryGetList(ListName.Project);
                        if (projectList != null)
                        {
                            listId = projectList.ID;
                            if (DDContext.Current.LoginUser.IsAdmin)
                            {
                                if (!string.IsNullOrEmpty(viewNameList[0]))
                                    viewId = GetViewIdByName(projectList, viewNameList[0]);
                            }
                            else if (DDContext.Current.LoginUser.IsParticipant)
                            {
                                if (!string.IsNullOrEmpty(viewNameList[1]))
                                    viewId = GetViewIdByName(projectList, viewNameList[1]);
                            }
                            else if (DDContext.Current.LoginUser.IsNormalUser)
                            {
                                if (!string.IsNullOrEmpty(viewNameList[2]))
                                    viewId = GetViewIdByName(projectList, viewNameList[2]);
                            }
                            else
                            {
                                //error occur
                            }

                        }
                    }
                }
            });
            #endregion

            if (listId != Guid.Empty && viewId != Guid.Empty)
                SetListViewProperty(listId, viewId);
            else
            {
                //throw new Exception("List or view does not exist");
            }
        }

        private Guid GetViewIdByName(SPList list, string viewName)
        {
            Guid viewId = Guid.Empty;
            foreach (SPView view in list.Views)
            {
                if (string.Compare(view.Title, viewName, true) == 0)
                {
                    viewId = view.ID;
                    break;
                }
            }
            return viewId;
        }
        private void SetListViewProperty(Guid listId, Guid viewId)
        {
            lvControl.ListId = listId.ToString("B").ToUpperInvariant();
            lvControl.ViewId = viewId.ToString("B").ToUpperInvariant();
            lvControl.DataBind();
        }

        private bool IsEmptyList(List<string> list)
        {
            bool result = true;

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
