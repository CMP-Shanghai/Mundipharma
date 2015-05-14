using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using MR.SP.DueDiligence.Framework;
using MR.SP.DueDiligence.Framework.Const;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.DueDiligence.WebPart.KeyContacts
{
    public partial class KeyContactsUserControl : UserControl
    {
        public KeyContacts webPart;
        private const string DefaultUserGroups = "Due Diligence Administrators;BD Lead;Commercial Head;R&D Executive Director;Participants";//source user group
        private const string DefaultViewFields = "Name;Title;Department;Work Email";//Display title for grid
        private const string DefaultPropertiesFields = "DisplayName;SPS-JobTitle;Department;WorkEmail";//User profile properties
        private const string DefaultEmailColumns = "Work Email";
        private const string GroupFieldName = "Group";
        //private const string ContactsDatasourceId = "KeyContactDataSource";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //GenerateDataSource();

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
            gvContacts.AutoGenerateColumns = false;
            gvContacts.EnableViewState = false;


            //bind data
            DataTable sourceTable = GenerateDataSource();
            if (sourceTable != null)
            {
                //add field
                GenerateGridViewColumns(sourceTable);

                gvContacts.AllowSorting = false;
                gvContacts.AllowFiltering = false;
                gvContacts.AllowGrouping = true;
                gvContacts.GroupField = GroupFieldName;

                this.gvContacts.DataSource = sourceTable;
                this.gvContacts.DataBind();
            }
        }

        /// <summary>
        /// Generate data source
        /// </summary>
        /// <returns></returns>
        private DataTable GenerateDataSource()
        {
            DataTable result = GenerateDataColumn();

            if (result != null && result.Columns != null && result.Columns.Count > 0)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            var upm = new UserProfileManager(SPServiceContext.Current);
                            string userGroupStrings = !string.IsNullOrEmpty(webPart.UserGroups) ? webPart.UserGroups : DefaultUserGroups;
                            List<string> groupList = userGroupStrings.Convert2List();
                            List<string> propertiesName = GetUserProfileProperties();
                            foreach (var userGroupName in groupList)
                            {
                                //Get Group first
                                SPGroup curGroup = GetUserGroup(web, userGroupName);
                                //Get group users and copy data into Data table
                                if (curGroup == null) continue;

                                //curGroup.Users
                                foreach (SPUser user in curGroup.Users)
                                {
                                    #region Generate User Row
                                    try
                                    {
                                        if (upm.UserExists(user.LoginName))
                                        {
                                            UserProfile up = upm.GetUserProfile(user.LoginName);
                                            if (up == null)
                                            {
                                                //simple mode
                                                GenerateUserInformatoinRow(result, propertiesName, user, userGroupName);
                                            }
                                            else
                                            {
                                                GenerateUserInformatoinRow(result, propertiesName, up, user, userGroupName);
                                            }
                                        }
                                        else
                                        {
                                            //simple mode
                                            GenerateUserInformatoinRow(result, propertiesName, user, userGroupName);

                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                        //throw;
                                    }
                                    #endregion
                                }

                            }
                        }
                    }
                });

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="propertiesName"></param>
        /// <param name="up"></param>
        private void GenerateUserInformatoinRow(DataTable result, List<string> propertiesName, UserProfile up, SPUser spUser, string groupName)
        {
            DataColumnCollection columnCollection = result.Columns;
            DataRow newRow = result.NewRow();
            bool isDirty = false;
            //Mapping properties
            for (int propertyIndex = 0; propertyIndex < (columnCollection.Count - 1) && propertyIndex < propertiesName.Count; propertyIndex++)
            {
                #region Set Row value based on properties value
                string propertyName = propertiesName[propertyIndex];
                string profileValue = string.Empty;
                switch (propertyName)
                {
                    case "DisplayName":
                        profileValue = up.DisplayName;
                        break;
                    case "AccountName":
                        profileValue = up.AccountName;
                        break;
                    case "ManagerDisplayName":
                        UserProfile mgUp = up.GetManager();
                        if (mgUp != null)
                            profileValue = mgUp.DisplayName;
                        break;
                    default:
                        profileValue = GetPropertyValue(propertiesName[propertyIndex], up);
                        break;
                }
                if (IsEmailField(columnCollection[propertyIndex].ColumnName) && !string.IsNullOrEmpty(profileValue))
                {
                    profileValue = string.Format("<a href='mailto:{0}'>{0}</a>", profileValue);
                }
                if (!string.IsNullOrEmpty(profileValue))
                {
                    newRow[propertyIndex] = profileValue;
                    newRow[columnCollection.Count - 1] = groupName;//last column
                    isDirty = true;
                }
                #endregion
            }

            if (isDirty) result.Rows.Add(newRow);
            else GenerateUserInformatoinRow(result, propertiesName, spUser, groupName);//to ensure data is generate
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="propertiesName"></param>
        /// <param name="spUser"></param>
        /// <param name="groupName"></param>
        private void GenerateUserInformatoinRow(DataTable result, List<string> propertiesName, SPUser spUser, string groupName)
        {
            DataColumnCollection columnCollection = result.Columns;
            DataRow newRow = result.NewRow();
            bool isDirty = false;
            //Mapping properties
            for (int propertyIndex = 0; propertyIndex < (columnCollection.Count - 1) && propertyIndex < propertiesName.Count; propertyIndex++)
            {
                #region Set Row value based on properties value
                string propertyName = propertiesName[propertyIndex];
                string profileValue = string.Empty;

                switch (propertyName)
                {
                    case "DisplayName":
                        profileValue = spUser.Name;
                        break;
                    case "AccountName":
                        profileValue = spUser.LoginName;
                        break;
                    case "WorkEmail":
                        profileValue = string.IsNullOrEmpty(spUser.Email) ? string.Empty : spUser.Email;
                        break;
                    default:
                        profileValue = string.Empty;
                        break;
                }

                if (IsEmailField(columnCollection[propertyIndex].ColumnName) && !string.IsNullOrEmpty(profileValue))
                {
                    profileValue = string.Format("<a href='mailto:{0}'>{0}</a>", profileValue);
                }
                if (!string.IsNullOrEmpty(profileValue))
                {
                    newRow[propertyIndex] = profileValue;
                    isDirty = true;
                }
                #endregion
            }
            newRow[columnCollection.Count - 1] = groupName;//last column

            if (isDirty) result.Rows.Add(newRow);
        }


        private SPGroup GetUserGroup(SPWeb web, string groupName)
        {
            SPGroup result = null;

            try
            {
                result = web.SiteGroups.GetByName(groupName);
            }
            catch (Exception inEx)
            {
                //Group is not existed
            }

            return result;
        }
        private DataTable GenerateDataColumn()
        {
            DataTable result = new DataTable();
            string userGroupStrings = !string.IsNullOrEmpty(webPart.UserGroups) ? webPart.UserGroups : DefaultUserGroups;
            List<string> viewFields = GetViewFields();

            if (!string.IsNullOrEmpty(userGroupStrings) && viewFields.Count > 0)
            {

                foreach (var field in viewFields)
                {
                    //UserProfileManager upf
                    result.Columns.Add(field);
                }
                result.Columns.Add(GroupFieldName);//Add Group at last column for group

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        private static string GetPropertyValue(string propertyName, UserProfile profile)
        {
            string result = string.Empty;
            if (null == profile || profile.Properties.GetPropertyByName(propertyName) == null || null == profile[propertyName] || null == profile[propertyName].Value) return string.Empty;

            result = profile[propertyName].Value.ToString();
            return result;
        }

        private List<string> GetViewFields()
        {
            List<string> result = new List<string>();
            string fieldsStrings = !string.IsNullOrEmpty(webPart.ViewFields) ? webPart.ViewFields : DefaultViewFields;

            result = fieldsStrings.Convert2List();
            return result;
        }
        private List<string> GetUserProfileProperties()
        {
            List<string> result = new List<string>();
            string propertiesStrings = !string.IsNullOrEmpty(webPart.UserProfilePrpoerties) ? webPart.UserProfilePrpoerties : DefaultPropertiesFields;
            result = propertiesStrings.Convert2List();
            return result;
        }

        private List<string> GetEmailColumns()
        {
            List<string> result = new List<string>();
            string emailColumsn = !string.IsNullOrEmpty(webPart.EmailColumns) ? webPart.EmailColumns : DefaultEmailColumns;
            result = emailColumsn.Convert2List();
            return result;
        }
        private void GenerateGridViewColumns(DataTable dt)
        {
            if (dt == null) return;
            DataColumnCollection dccs = dt.Columns;

            string filterFields = string.Empty;
            foreach (DataColumn col in dccs)
            {
                if (string.Compare(col.ColumnName, GroupFieldName) == 0) continue;
                if (IsDateField())
                {
                    BoundField bField = new BoundField();
                    bField.DataField = col.ColumnName;
                    bField.SortExpression = col.ColumnName;
                    bField.HeaderText = col.ColumnName;
                    bField.HtmlEncode = true;
                    bField.DataFormatString = @"{0:dd/MM/yyyy}";
                    gvContacts.Columns.Add(bField);
                    filterFields += col.ColumnName + ",";
                }
                else if (IsEmailField(col.ColumnName))
                {
                    //HyperLinkField linkField = new HyperLinkField();

                    //linkField.DataTextField = col.ColumnName;
                    //linkField.DataNavigateUrlFields = new string[] { "Work Email" };
                    //linkField.DataNavigateUrlFormatString = "test.aspx?id={0}";
                    //linkField.SortExpression = col.ColumnName;
                    //linkField.HeaderText = col.ColumnName;

                    BoundField linkField = new BoundField();
                    linkField.DataField = col.ColumnName;
                    linkField.HtmlEncode = false;
                    linkField.HeaderText = col.ColumnName;


                    gvContacts.Columns.Add(linkField);
                    filterFields += col.ColumnName + ",";
                }
                else
                {
                    SPBoundField spbField = new SPBoundField();
                    spbField.DataField = col.ColumnName;
                    spbField.SortExpression = col.ColumnName;
                    spbField.HeaderText = col.ColumnName;
                    gvContacts.Columns.Add(spbField);
                    filterFields += col.ColumnName + ",";
                }

            }

            filterFields = filterFields.TrimEnd(',');
            gvContacts.FilterDataFields = filterFields;


        }

        private static bool IsDateField()
        {
            return false;
        }

        private bool IsEmailField(string columnName)
        {
            List<string> emailColumsn = GetEmailColumns();
            if (emailColumsn.IndexOf(columnName) >= 0) return true;
            else return false;

            return false;
        }
    }

    public static class StringExtention
    {
        public static List<string> Convert2List(this string sourceString)
        {
            List<string> result = new List<string>();

            string[] splitArray = new string[] { ",", ";" };
            result = Convert2List(sourceString, splitArray);
            return result;
        }

        public static List<string> Convert2List(this string sourceString, string[] splitArray)
        {
            List<string> result = new List<string>();

            if (!string.IsNullOrEmpty(sourceString))
            {
                string[] fieldsArray = sourceString.Split(splitArray, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in fieldsArray)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
