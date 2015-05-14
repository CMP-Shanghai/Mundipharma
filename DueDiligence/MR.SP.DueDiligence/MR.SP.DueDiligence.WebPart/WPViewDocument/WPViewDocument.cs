using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using System.Data;
using MR.SP.DueDiligence.Framework.Const;
using Microsoft.SharePoint.Utilities;

namespace MR.SP.DueDiligence.WebPart.WPViewDocument
{
    [ToolboxItemAttribute(false)]
    public class WPViewDocument : WebPartBase
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


        [Personalizable(), WebDisplayName("CopyButton display name"), WebBrowsable(true)]
        public string CopyButtonText
        {
            get;
            set;
        }

        [Personalizable(), WebDisplayName("UploadButton display name"), WebBrowsable(true)]
        public string UploadButtonText
        {
            get;
            set;
        }

        private string coustomDocumentType;
         [Personalizable(), WebDisplayName("Document type name,if empty"), WebBrowsable(true)]
        public string CoustomDocumentType
        {
            get
            {
                if (string.IsNullOrEmpty(coustomDocumentType))
                {
                    return "(Null)";
                }
                return coustomDocumentType;
            }
            set { coustomDocumentType = value; }
        }

        private const string HTML_ClientControl =
            "<input onclick='InitCopyFile(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{6}\");' type='button' value='{5}' />"
            + "<input id='{4}' type='text' style='display: none;'/> "
            + "<button id='{7}' style='display:none;' click='return false;' ></button> ";

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

        }

        protected override void CreateChildControls()
        {
            try
            {
                #region Render Table
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                    {

                        #region Get Project Item
                        string id = this.Page.Request[RequestParameter.ParameterID];

                        SPList listProject = web.Lists[ListName.Project];
                        SPListItem itemProject = listProject.GetItemById(int.Parse(id));
                        #endregion

                        #region Get Types
                        // current user rights
                        SPGroup adminGroup = web.SiteGroups.GetByName(GroupName.AdminGroup);

                        bool bl = (adminGroup != null && HasRights(web, itemProject, adminGroup));
                        if (!bl)
                        {
                            this.Page.Response.Write("<script>alert('You do not have permission to enter');window.location.href='" + web.Url + "'</script>");
                            return;

                        }
                        // The current user is included in the admin group ?

                        #endregion

                        #region Generate Table
                        //show view
                        SPList list = web.Lists[ListName.DocumentRepository];
                        SPDocumentLibrary dl = list as SPDocumentLibrary;
                        SPFolder folder=null;
                        try
                        {
                            folder = dl.RootFolder.SubFolders[id];
                        }
                        catch (Exception)
                        {
                            
                            return;
                        }
                        SPFileCollection RepositoryFiles = folder.Files;
                        Table tab;
                        List<string> listGroupNames = new List<string>();
                        SPList listArtefacts = web.Lists[ListName.DocumentTemplate];
                        string FullOrShortField = listArtefacts.Fields[Fields.FullOrShort].InternalName;
                        SPQuery query = new SPQuery();
                        query.Query = @"
                                       <OrderBy>
                                          <FieldRef Name='" + listArtefacts.Fields[Fields.SortNumInDocument].InternalName + @"' />
                                       </OrderBy>";
                        SPListItemCollection listItemArtefacts = listArtefacts.GetItems(query);
                        tab = CreateTable(listItemArtefacts, RepositoryFiles);
                        #endregion

                        #region Add panel and table


                        //upload button div
                        if (SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
                        {
                            string webUrl = string.Compare(web.ServerRelativeUrl, "/") == 0 ? string.Empty : web.ServerRelativeUrl;
                            Label lbbtnupload = new Label();
                            string btnUpload = JSString.BtnUpLoad.Replace("_webUrl_", webUrl).Replace("_listID_", list.ID.ToString()).Replace("_listName_", dl.RootFolder.Url).Replace("_ID_", id).Replace
                                ("_DisplayName_",UploadButtonText);
                            lbbtnupload.Text = btnUpload;
                            this.Controls.Add(lbbtnupload);

                            //Copy from SharePoint buton
                            string rootWebUrl = string.Compare(site.RootWeb.Url, "/") == 0 ? string.Empty : site.RootWeb.Url;
                            GenerateButton(rootWebUrl);
                        }
                        
                        //external div
                        Panel pnlDiv = new Panel();
                        pnlDiv.ID = "pnlDiv";
                        pnlDiv.CssClass = "ms-authoringcontrols";
                        pnlDiv.Controls.Add(tab);

                        // upload button
                        this.Controls.Add(pnlDiv);
                        #endregion
                    }
                }
                #endregion
                base.CreateChildControls();
            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(ex.ToString());
            }
        }

        private void GenerateButton(string rootWebUrl)
        {
            try
            {
                ScriptManager.RegisterClientScriptInclude(this.Page, GetType(), Guid.NewGuid().ToString(), rootWebUrl + "/_layouts/15/AssetPickers.js");
                ScriptManager.RegisterClientScriptInclude(this.Page, GetType(), Guid.NewGuid().ToString(), rootWebUrl + "/style%20library/diligenceportal/scripts/MR.SP.DueDiligence.CopyFile.js");

                string clientHiddenControlId = "MR_CopyItemUrl";
                string refeshButtonId = "MR_RefeshButton";
                string id = this.Page.Request[RequestParameter.ParameterID];
                Label lbbtnupload = new Label();
                string btnUpload = string.Format(HTML_ClientControl, DestinatinListName, id, clientHiddenControlId, StartSourceLibrary, clientHiddenControlId, CopyButtonText, refeshButtonId, refeshButtonId);
                lbbtnupload.Text = btnUpload;
                this.Controls.Add(lbbtnupload);
            }
            catch (Exception)
            {

            }
        }

        

        #region Internal Method
        /// <summary>
        /// Check whether current user has rights to access current item
        /// </summary>
        /// <param name="web"></param>
        /// <param name="itemProject"></param>
        /// <param name="adminGroup"></param>
        /// <returns></returns>
        private bool HasRights(SPWeb web, SPListItem itemProject, SPGroup adminGroup)
        {
            bool hasRights = false;
            try
            {

                if (web.CurrentUser.IsSiteAdmin || adminGroup.ContainsCurrentUser || ((string)itemProject[Fields.Outcome] == "Approved"))
                {
                    hasRights = true;
                }
                else
                {
                    foreach (string sfield in GroupName.GroupsHasRightsOnProject)
                    {
                        SPFieldUserValueCollection participants = itemProject[itemProject.ParentList.Fields[sfield].InternalName] as SPFieldUserValueCollection;
                        if (participants != null)
                        {
                            foreach (SPFieldUserValue userValue in participants)
                            {
                                if (userValue.User.ID == web.CurrentUser.ID)
                                {
                                    hasRights = true;
                                    return hasRights;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                hasRights = false;
            }

            return hasRights;
        }


        /// <summary>
        /// Create Table tag
        /// </summary>
        /// <param name="files"></param>
        /// <param name="listGroupNames"></param>
        /// <returns></returns>
        private Table CreateTable(SPListItemCollection tempItems, SPFileCollection RepositoryFiles)
        {
            Table tab = new Table();

            //drow table
            if (RepositoryFiles == null || RepositoryFiles.Count == 0)
            {
                return tab;
            }

            #region Render Table
            tab.CssClass = "TypeTable";
            //table tr td root
            TableRow trRoot = new TableRow();
            trRoot.CssClass = "titletr";
            TableCell tcRoot;
            tcRoot = new TableCell();
            tcRoot.CssClass = "titletd";
            tcRoot.Text = "File Name";
            trRoot.Cells.Add(tcRoot);
            tcRoot = new TableCell();
            tcRoot.Text = "Version";
            trRoot.Cells.Add(tcRoot);
            tcRoot = new TableCell();
            tcRoot.Text = "Submission Date";
            trRoot.Cells.Add(tcRoot);

            tab.Rows.Add(trRoot);
            TableRow tr;
            TableCell tc;
            List<string> flagGroupNames = new List<string>();
            List<string> listGroupNames = new List<string>();
            foreach (SPListItem tItem in tempItems)
            {
                listGroupNames.Add((string)tItem.File.Item[Fields.DocumentTypeInDocument]);
                
            }
            foreach (SPFile file in RepositoryFiles)
            {
                if (listGroupNames.Contains((string)file.Item[Fields.DocumentTypeInDocument]) == false)
                { listGroupNames.Add((string)file.Item[Fields.DocumentTypeInDocument]); }
            }
            listGroupNames.Add(CoustomDocumentType);

            
            foreach (string groupName in listGroupNames)
            {
                
                foreach (SPFile file in RepositoryFiles)
                {
                    string documentType = string.IsNullOrEmpty((string)file.Item[Fields.DocumentTypeInDocument]) == true ? CoustomDocumentType : (string)file.Item[Fields.DocumentTypeInDocument];
                    if (string.Compare(documentType, groupName, StringComparison.CurrentCultureIgnoreCase) == 0
                        && file.CheckOutType == SPFile.SPCheckOutType.None)
                    {
                        if (!flagGroupNames.Contains(groupName))
                        {
                            flagGroupNames.Add(groupName);
                            tr = new TableRow();
                            tc = new TableCell();
                            tc.CssClass = "Type";
                            tc.Text = "<b class=\"typename\">" + groupName + "</b>    <hr />";
                            tc.ColumnSpan = 3;
                            tr.Cells.Add(tc);
                            tab.Rows.Add(tr);
                        }

                        tr = new TableRow();
                        tr.CssClass = "contenttr";
                        tc = new TableCell();
                        HyperLink fileLink = new HyperLink();
                        fileLink.ID = file.Item.ID.ToString();
                        fileLink.Text = file.Item.Name;
                        //fileLink.NavigateUrl = file.Item[SPBuiltInFieldId.EncodedAbsUrl].ToString();
                        string listID =  file.ParentFolder.ParentListId.ToString();
                        string fileName = file.Name;
                        string absUrl = file.Item[SPBuiltInFieldId.EncodedAbsUrl].ToString();
                        string linkMethod = string.Format("OpenDDLibraryFile('{0}','{1}','{2}')", listID, fileName, absUrl);
                        fileLink.Attributes.Add("href", "#");
                        fileLink.Attributes.Add("onclick", linkMethod);
                        tc.Controls.Add(fileLink);
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        tc.Text = "<a onclick=\"var options = {title: 'Versions',width: 500,height: 300,url: '" + SPContext.Current.Site.Url + "/_layouts/15/Versions.aspx?list={" + file.Item.ParentList.ID + "}&ID=" + file.Item.ID + "&FileName=" + SPContext.Current.Site.Url + "/" + file.Url + "&IsDlg=1' };SP.UI.ModalDialog.showModalDialog(options);\" href='#'>Version History</a>";
                        //tc.Text = GenerateURL(file);
                        tr.Cells.Add(tc);
                        tc = new TableCell();
                        tc.Text = file.Item[Fields.ReviewDateInDocument] != null ? Convert.ToDateTime(file.Item[Fields.ReviewDateInDocument]).ToString("dd/MM/yyyy") : string.Empty;
                        tr.Cells.Add(tc);
                        tab.Rows.Add(tr);
                    }
                }

            }
            #endregion

            return tab;

        }

        /// <summary>
        /// Generate document link
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string GenerateURL(SPFile file)
        {
            const string urlSnippet = "<a onclick=\"var options = {title: 'Versions',width: 500,height: 300,url: '{0}/_layouts/15/Versions.aspx?list={{{1}}}&ID={2}&FileName={3}/{4}&IsDlg=1' };SP.UI.ModalDialog.showModalDialog(options);\" href='#'>Version History</a>";
            string result = string.Empty;
            if (file != null)
            {
                result = string.Format(urlSnippet, file.Web.Site.Url, file.Item.ParentList.ID, file.Item.ID, file.Web.Site.Url, file.Url);
            }
            return result;
        }
        #endregion
    }
}
