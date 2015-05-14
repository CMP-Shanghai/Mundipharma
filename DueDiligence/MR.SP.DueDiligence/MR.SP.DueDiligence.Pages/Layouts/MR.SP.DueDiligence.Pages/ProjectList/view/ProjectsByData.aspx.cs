using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using MR.SP.DueDiligence.Framework;
using MR.SP.DueDiligence.Framework.Const;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace MR.SP.DueDiligence.Pages.Layouts.MR.SP.DueDiligence.Pages.ProjectList.view
{
    public partial class ProjectsByData : LayoutsPageBase
    {
        #region Property
        private string _internalTAFieldName = "Therapeutic Area";
        private string InternalTAFieldName
        {
            get { return _internalTAFieldName; }
            set { _internalTAFieldName = value; }
        }

        private string _internalTitleFieldName = "Opportunity";
        private string InternalTitleFieldName
        {
            get { return _internalTitleFieldName; }
            set { _internalTitleFieldName = value; }
        }


        private string _internalStartFieldName = "Start";
        private string InternalStartFieldName
        {
            get { return _internalStartFieldName; }
            set { _internalStartFieldName = value; }
        }


        private string _internalEndFieldName = "Planned End";
        private string InternalEndFieldName
        {
            get { return _internalEndFieldName; }
            set { _internalEndFieldName = value; }
        }

        private string _internalAEFieldName = "Actual End";
        private string InternalAEFieldName
        {
            get { return _internalAEFieldName; }
            set { _internalAEFieldName = value; }
        }

        private string _internalDeviationFieldName = "Deviation (days)";
        private string InternalDeviationFieldName
        {
            get { return _internalDeviationFieldName; }
            set { _internalDeviationFieldName = value; }
        }

        private string _internalPOFieldName = "Outcome";
        private string InternalPOFieldName
        {
            get { return _internalPOFieldName; }
            set { _internalPOFieldName = value; }
        }

        #endregion

        private const string ObjectDataSourcedId = "odsGridView";

        private const string AllItemText = "All...";
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Get Internal Field name 
                EnsureIntenralFieldName();

                //ddl bind
                if (!IsPostBack)
                {
                    BindTherapeuticAreaItems();

                    BindOutomeItems();
                }

                GenerateDataSource();

                SetGridViewProperties();
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.Message);
                SPUtility.TransferToErrorPage(ex.Message);
            }


        }

        private void SetGridViewProperties()
        {
            gvReport.AutoGenerateColumns = false;
            gvReport.EnableViewState = false;
            //add field
            GenerateGridViewColumns();
            gvReport.AllowSorting = false;
            gvReport.AllowFiltering = false;

            //bind data
            this.gvReport.DataSourceID = ObjectDataSourcedId;
            this.gvReport.DataBind();
        }

        private void GenerateDataSource()
        {
            ObjectDataSource odsDataSource = new ObjectDataSource();
            odsDataSource.ID = ObjectDataSourcedId;
            odsDataSource.TypeName = this.GetType().FullName + "," + this.GetType().Assembly.FullName;
            odsDataSource.SelectMethod = "GetDataTableQuery";
            odsDataSource.SelectParameters.Add("queryStr", GetQueryStr());
            Controls.Add(odsDataSource);

        }

        private void EnsureIntenralFieldName()
        {
            //Refresh all internal field name for current environment
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb curWeb = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPList projectList = curWeb.Lists.TryGetList(ListName.Project);

                        if (projectList == null) return;
                        //Refresh all internal name for fields
                        InternalTAFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, Fields.TherapeuticArea, InternalTAFieldName);
                        InternalTitleFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, InternalTitleFieldName, InternalTitleFieldName);
                        InternalStartFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, InternalStartFieldName, InternalStartFieldName);
                        InternalEndFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, InternalEndFieldName, InternalEndFieldName);
                        InternalAEFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, InternalAEFieldName, InternalAEFieldName);
                        InternalDeviationFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, InternalDeviationFieldName, InternalDeviationFieldName);
                        InternalPOFieldName = FieldsHelper.GetFieldInternalNameByDisplayName(projectList, Fields.Outcome, InternalPOFieldName);
                        //....
                    }

                }
            });
        }

        protected void SPGridViewPager1_ClickNext(object sender, EventArgs e)
        {
            try
            {

                this.gvReport.DataSourceID = ObjectDataSourcedId;
                this.gvReport.DataBind();
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
                //throw;
                SPUtility.TransferToErrorPage(ex.Message);
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Internal methods

        private void GenerateGridViewColumns()
        {
            DataTable dt = GetDataTable();
            if (dt == null) return;
            DataColumnCollection dccs = dt.Columns;
            dccs.Remove("Created");
            //dccs.Remove(InternalTitleFieldName);
            dccs.Remove("Modified");
            //dccs.Remove(InternalTAFieldName);
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {

                        SPList list = web.Lists[ListName.Project];
                        SPFieldCollection fields = list.Fields;

                        string filterFields = string.Empty;
                        foreach (DataColumn dcc in dccs)
                        {
                            foreach (SPField field in fields)
                            {
                                if (field.InternalName == dcc.ColumnName)
                                {
                                    if (IsDateField(field.InternalName))
                                    {
                                        BoundField col = new BoundField();
                                        col.DataField = dcc.ColumnName;
                                        col.SortExpression = dcc.ColumnName;
                                        col.HeaderText = field.Title;
                                        col.HtmlEncode = true;
                                        col.DataFormatString = @"{0:dd/MM/yyyy}";
                                        gvReport.Columns.Add(col);
                                        filterFields += dcc.ColumnName + ",";
                                    }
                                    else
                                    {
                                        SPBoundField col = new SPBoundField();
                                        col.DataField = dcc.ColumnName;
                                        col.SortExpression = dcc.ColumnName;
                                        col.HeaderText = field.Title;
                                        gvReport.Columns.Add(col);
                                        filterFields += dcc.ColumnName + ",";
                                    }
                                }
                            }
                        }
                        filterFields = filterFields.TrimEnd(',');
                        gvReport.FilterDataFields = filterFields;
                    }
                }
            });

        }
        public void BindTherapeuticAreaItems()
        {

            //StringBuilder sb = new StringBuilder();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPList list = web.Lists[ListName.TherapeuticArea];
                        SPListItemCollection items = list.Items;
                        foreach (SPListItem item in items)
                        {
                            selectArea.Items.Add(item.Title);
                        }
                        selectArea.Items.Insert(0, AllItemText);
                    }
                }
            });

        }

        public void BindOutomeItems()
        {

            //StringBuilder sb = new StringBuilder();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPList list = web.Lists[ListName.Project];
                        
                        if (list.Fields.ContainsField(InternalPOFieldName) && list.Fields[InternalPOFieldName] is SPFieldChoice)
                        {
                            SPFieldChoice outcomeField = (SPFieldChoice)list.Fields[InternalPOFieldName];

                            foreach (var outcomeValue in outcomeField.Choices)
                            {
                                selectOutcome.Items.Add(new ListItem(outcomeValue, outcomeValue));
                            }
                        }
                        selectOutcome.Items.Insert(0, new ListItem(AllItemText, AllItemText));

                    }
                }
            });

        }

        private string GetQueryStr()
        {
            string keyArea = selectArea.SelectedValue;
            string keyOutcome = selectOutcome.SelectedItem.Text;
            string queryForStatus = string.Empty, queryForTA = string.Empty;
            if (string.Compare(keyArea, AllItemText, true) != 0)
            {
                queryForTA = @"<Eq><FieldRef Name='" + InternalTAFieldName + @"'/>
						            <Value Type='Lookup'>" + keyArea + @"</Value>
					            </Eq>";
            }
            if (string.Compare(keyOutcome, AllItemText, true) != 0)
            {
                queryForStatus = @"<Eq>
                                    <FieldRef Name='" + InternalPOFieldName + @"' />
                                    <Value Type='Choice'>" + keyOutcome + @"</Value>
                                </Eq>";
            }

            string query = string.Empty;
            if (!string.IsNullOrEmpty(queryForTA) && !string.IsNullOrEmpty(queryForStatus))
            {
                query = "<Where><And>{0}{1}</And></Where>";
            }
            else if (string.IsNullOrEmpty(queryForTA) && string.IsNullOrEmpty(queryForStatus))
            {
                query = "{0}{1}";
            }
            else
            {
                query = "<Where>{0}{1}</Where>";
            }
            query = string.Format(query, queryForStatus, queryForTA);

            return query;
        }

        public DataTable GetDataTableQuery(string queryStr)
        {
            EnsureIntenralFieldName();
            DataTable tblData = new DataTable();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPList list = web.Lists[ListName.Project];
                        SPQuery query = new SPQuery();
                        query.Query = queryStr + "<OrderBy><FieldRef Name ='ID' Ascending='FALSE'/></OrderBy>";
                        query.ViewFields = "<FieldRef Name=\"ID\"/><FieldRef Name=\"" + InternalTitleFieldName + "\"/><FieldRef Name=\"" + InternalStartFieldName + "\"/><FieldRef Name=\"" + InternalEndFieldName + "\"/><FieldRef Name=\"" + InternalAEFieldName + "\"/><FieldRef Name=\"" + InternalTAFieldName + "\"/><FieldRef Name=\"" + InternalDeviationFieldName + "\"/>";

                        tblData = list.GetItems(query).GetDataTable();
                    }
                }
            });

            return tblData;

        }

        public DataTable GetDataTable()
        {
            DataTable tblData = new DataTable();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {
                        SPList list = web.Lists[ListName.Project];
                        SPQuery query = new SPQuery();
                        query.Query = "<OrderBy><FieldRef Name ='ID' Ascending='FALSE'/></OrderBy>";
                        query.ViewFields = "<FieldRef Name=\"ID\"/><FieldRef Name=\"" + InternalTitleFieldName + "\"/><FieldRef Name=\"" + InternalStartFieldName + "\"/><FieldRef Name=\"" + InternalEndFieldName + "\"/><FieldRef Name=\"" + InternalAEFieldName + "\"/><FieldRef Name=\"" + InternalTAFieldName + "\"/><FieldRef Name=\"" + InternalDeviationFieldName + "\"/>";
                        tblData = list.GetItems(query).GetDataTable();

                    }
                }
            });
            return tblData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool IsDateField(string fieldName)
        {
            bool result = false;
            List<string> fieldList = new List<string> { InternalAEFieldName, InternalEndFieldName, InternalStartFieldName };
            foreach (var item in fieldList)
            {
                if (string.Compare(fieldName, item, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        #endregion
    }
}
