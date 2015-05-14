using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;
using MR.SP.DueDiligence.Framework.Const;
using System.Data;
using MR.SP.DueDiligence.Framework;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.Office.Server;
using System.Collections;

namespace MR.SP.DueDiligence.EventReceiver.EventReceiverCreateDoc
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiverCreateDoc : SPItemEventReceiver
    {
        private const string NoneSelectValue = "(None)";
        #region Item events
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            try
            {
                EventFiringEnabled = false;
                if (properties.List.Title == ListName.Project)
                {
                    //using (SPWeb web = properties.OpenWeb())
                    SPWeb web = properties.List.ParentWeb;

                    //copy files
                    SPList listTemp = web.Lists[ListName.DocumentTemplate];
                    SPDocumentLibrary dlTemp = (SPDocumentLibrary)listTemp;
                    SPFileCollection fTemps = dlTemp.RootFolder.Files;


                    SPList list = web.Lists[ListName.DocumentRepository];
                    SPDocumentLibrary dl = list as SPDocumentLibrary;
                    SPFolder folder = dl.RootFolder.SubFolders.Add(properties.ListItemId.ToString());

                    foreach (SPFile f in fTemps)
                    {
                        CopyDoc(f, properties, folder);
                    }
                    AddUserToGroup(properties);
                    
                    CountData(properties);


                }
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }
            finally
            {
                EventFiringEnabled = true;
            }

        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            try
            {
                base.ItemUpdated(properties);
                EventFiringEnabled = false;

                if (properties.List.Title == ListName.Project)
                {
                    AddUserToGroup(properties);

                    //Concanate Next step and Next deadline
                    UpdateActionField(properties);
                   
                    //Update Count list for chart web part
                    CountData(properties);

                    ReadOnlyDocument(properties);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }
            finally
            {
                EventFiringEnabled = true;
            }
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {

            try
            {
                if (properties.List.Title == ListName.Project)
                {
                    EventFiringEnabled = false;
                    //string TAField = properties.List.Fields[Fields.TherapeuticArea].InternalName;
                    //string StatusField = properties.List.Fields[Fields.ProjectStatusInProject].InternalName;
                    ////SPFieldLookupValue YourLookup = new SPFieldLookupValue(Properties.AfterProperties.LisItem["Your lookup"] as string);
                    //SPFieldLookupValue afterTAValue = new SPFieldLookupValue(properties.AfterProperties[TAField] as string);
                    //SPFieldLookupValue beforeTAValue = new SPFieldLookupValue(properties.ListItem[TAField] as string);
                    //SPFieldLookupValueCollection afterStatusValue = new SPFieldLookupValueCollection(properties.AfterProperties[StatusField] as string);
                    //SPFieldLookupValueCollection beforeStatusValue = new SPFieldLookupValueCollection(properties.ListItem[StatusField].ToString());
                    //bool isStatusChange = IsStatusChagne(beforeStatusValue, afterStatusValue);
                    //if (afterTAValue.LookupId != beforeTAValue.LookupId || isStatusChange)
                    //{
                    //    CountData(properties);
                    //}

                    EventFiringEnabled = true;
                }
                base.ItemUpdating(properties);
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }
        }
        #endregion

        #region participants ready only document


        public string ItemPermission(string SitePath)
        {
            string ReturnVal = "";
            try
            {
                SPSite WebApp = new SPSite(SitePath);
                SPWeb Site = WebApp.OpenWeb();
                SPList list = Site.Lists["TestDocLib"];
                SPListItem item = list.Items[0];
                SPRoleDefinition RoleDefinition = Site.RoleDefinitions.GetByType(SPRoleType.Contributor);
                SPRoleAssignment RoleAssignment = new SPRoleAssignment("<domain>\\<user>", "email", "name", "notes");
                RoleAssignment.RoleDefinitionBindings.Add(RoleDefinition);
                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(true);
                }
                item.RoleAssignments.Add(RoleAssignment);
                item.Update();
            }
            catch (Exception ex)
            {
                ReturnVal += "Permission not set, reason: " + ex.Message;
            }
            return ReturnVal;
        }
        private void ReadOnlyDocument(SPItemEventProperties properties)
        {
            SPList listProject = properties.List;
            int projectID = properties.ListItemId;
            SPListItem itemProject = listProject.GetItemById(projectID);

            SPWeb web = properties.Web;
            SPList listDocument = web.Lists[ListName.DocumentRepository];
            SPDocumentLibrary documentStore = listDocument as SPDocumentLibrary;
            SPFolder folder = documentStore.RootFolder.SubFolders[projectID.ToString()];
            SPListItem itemFolder = folder.Item;
            SPRoleDefinition RoleDefinition = web.RoleDefinitions.GetByType(SPRoleType.Reader);
            if ((string)itemProject[Fields.Outcome] != "Approved")
            {
                if (itemFolder.HasUniqueRoleAssignments)
                {
                    itemFolder.ResetRoleInheritance();
                }
                return;
            }


            if (!itemFolder.HasUniqueRoleAssignments)
            {
                itemFolder.BreakRoleInheritance(false);
            }

            //foreach (SPGroup group in web.SiteGroups)
            //{
            //    SPPrincipal principal = (SPPrincipal)group;

            //    //foreach(SPRole role in principal.Roles)
            //    //{
                    
            //    //}
            //    listDocument.RoleAssignments.Remove(principal);
            //}


            //SPRoleAssignmentCollection roleAssigns = listDocument.RoleAssignments;
            //foreach (SPRoleAssignment assign in roleAssigns)
            //{
            //    SPRoleDefinitionBindingCollection roleDefinitions = assign.RoleDefinitionBindings;
            //    roleDefinitions.RemoveAll();
            //    //assign.Member.Roles.
            //    roleDefinitions.Add(RoleDefinition);
                
            //}
            
            //System.Security.Principal.WellKnownSidType.WorldSid
            SPMember mem = web.SiteGroups[GroupName.AdminGroup];
            SPPrincipal sp = (SPPrincipal)mem;
            SPRoleAssignment ra = new SPRoleAssignment(sp);
            SPRoleDefinition RoleDefinitionAdmin = web.RoleDefinitions.GetByType(SPRoleType.Administrator);            
            ra.RoleDefinitionBindings.Add(RoleDefinitionAdmin);
            itemFolder.RoleAssignments.Add(ra);

            mem = web.EnsureUser("NT AUTHORITY\\authenticated users");
            sp = (SPPrincipal)mem;
            ra = new SPRoleAssignment(sp);
            SPRoleDefinition RoleDefinitionRead = web.RoleDefinitions.GetByType(SPRoleType.Reader);
            ra.RoleDefinitionBindings.Add(RoleDefinitionRead);
            itemFolder.RoleAssignments.Add(ra);
            itemFolder.Update();



        }

        #endregion
        #region Participant related methods


        public UserProfile GetUserProfileByLoginName(SPItemEventProperties properties, string loginName)
        {
            UserProfile userProfile = null;
            SPSite site = properties.Site;
            ServerContext sc = ServerContext.GetContext(site);
            UserProfileManager profileManager = new UserProfileManager(sc);
            if (profileManager.UserExists(loginName))
            {
                userProfile = profileManager.GetUserProfile(loginName);
            }

            return userProfile;
        }




        //add user to  projectuser group
        private void AddUserToGroup(SPItemEventProperties properties)
        {
            //add username and department to userdepartment field
            string userDepartment = string.Empty;


            string field = properties.List.Fields[Fields.Participants].InternalName;
            //SPUser user = properties.ListItem[field];
            //SPGroup group = properties.Web.SiteGroups[GroupName.ProjectUsers];
            string[] groupNames = properties.Web.AllProperties[PropertiesKey.WEBPROPERTIE_ADDUSERTOPARTICIPANT].ToString().Split(';');
            SPFieldUserValueCollection Participants = properties.ListItem[field] as SPFieldUserValueCollection;
            if (Participants != null)
            {
                //Participants count
                int externalParticipantsCount = 0;
                string externalParticipantsValue = (string)properties.ListItem[Fields.ExternalParticipants];
                if (!string.IsNullOrEmpty(externalParticipantsValue))
                {
                    externalParticipantsValue = externalParticipantsValue.TrimEnd(';');
                    if (externalParticipantsValue.IndexOf(';') != -1)
                    {
                        externalParticipantsCount = externalParticipantsValue.Split(';').Length;
                    }
                    else
                    {
                        externalParticipantsCount = 1;
                    }
                }
                properties.ListItem[Fields.NoOfParticipants] = (Participants.Count + externalParticipantsCount).ToString();

                foreach (SPFieldUserValue userValue in Participants)
                {
                    UserProfile u = GetUserProfileByLoginName(properties, userValue.User.LoginName);
                    if (u == null || u[PropertyConstants.Department] == null || u[PropertyConstants.Department].Value == null)
                    {
                        userDepartment += string.Format("{0};", userValue.User.Name);
                    }
                    else
                    {
                        userDepartment += string.Format("{0}({1});", userValue.User.Name, (string)u[PropertyConstants.Department].Value);
                    }


                    foreach (string g in groupNames)
                    {

                        try
                        {
                            SPGroup group = properties.Web.SiteGroups[g];
                            group.AddUser(userValue.User);
                        }
                        catch (Exception)
                        {

                        }
                    }



                }

                userDepartment = userDepartment.TrimEnd(';');
                properties.ListItem[Fields.UserDepartment] = userDepartment;
                properties.ListItem.Update();
            }
            //properties.Web.SiteGroups[GroupName.ProjectUsers].AddUser()
        }
        #endregion

        #region Document tempate auto generate related methods
        private void CopyDoc(SPFile file, SPItemEventProperties properties, SPFolder folder)
        {
            string fieldFullOrShortValue=(string)file.Item[Fields.FullOrShort];
            if (fieldFullOrShortValue == (string)properties.ListItem[Fields.Form] || string.IsNullOrEmpty(fieldFullOrShortValue) || fieldFullOrShortValue=="Both")
            {
                SPFile newF = folder.Files.Add(file.Name, file.OpenBinary());
                //newF.Item["itemID"] = properties.ListItemId.ToString();
                newF.Item[Fields.DocumentTypeInDocument] = file.Item[Fields.DocumentTypeInDocument];
                newF.Item[Fields.Opportunity] = (string)properties.ListItem[Fields.Opportunity];
                //newF.Item[Fields.ProjectStatusInDocument] = (string)properties.ListItem[Fields.ProjectStatusInProject];
                //newF.Item[Fields.SortNumInDocument] = Convert.ToInt32(file.Item[Fields.SortNumInDocument]);
                newF.Item.Update();
            }
        }
        #endregion

        #region Update Action Feild to concanate next step and next dead line
        private void UpdateActionField(SPItemEventProperties properties)
        {
            Hashtable htList = Fields.GetChecklist();
            string ActionValue = string.Empty;

            SPList projectList = properties.List;
            foreach (DictionaryEntry item in htList)
            {
                DateTime reviewDate = Convert.ToDateTime(properties.ListItem[item.Key.ToString()]);
                TimeSpan ts = reviewDate.Date - DateTime.Now.Date;
                if (ts.Days <= 15 && ts.Days >= 0)
                {
                    ActionValue += string.Format("Next Step: {0}  Next Deadline: {1} <br/>", item.Value, reviewDate.Date.ToString("MM/dd/yyyy"));
                }
            }
            properties.ListItem[Fields.Action] = ActionValue;

            properties.ListItem.Update();
        }
        #endregion

        #region Manipulate Chart Data source
        private void CountData(SPItemEventProperties properties)
        {
            //Count Data
            SPWeb web = properties.Web;
            //web.Site.ContentDatabase;
            SPList listReport = web.Lists[ListName.ReportForData];
            DDUtility.BatchDelete(listReport, listReport.Items);

            SPList listProjects = properties.List;

            SPQuery queryData = new SPQuery();
            queryData.ViewFields = string.Format("<FieldRef Name='Title' /><FieldRef Name='{0}' /><FieldRef Name='{1}' /><FieldRef Name='{2}' />",
                listProjects.Fields[Fields.TherapeuticArea].InternalName,
                listProjects.Fields[Fields.ProjectStatusInProject].InternalName,
                listProjects.Fields[Fields.Form].InternalName);

            queryData.RowLimit = 0;
            DataTable resultTable = listProjects.GetItems(queryData).GetDataTable();

            if (resultTable != null && resultTable.Rows != null && resultTable.Rows.Count > 0)
            {
                //Generate Hashtalbe for TA
                Dictionary<KeyValuePair<string, string>, int> taDict = GetCountDataByField(resultTable, listProjects.Fields[Fields.TherapeuticArea].InternalName, listProjects.Fields[Fields.Form].InternalName, false);
                //Generate Hashtable for Status
                Dictionary<KeyValuePair<string, string>, int> statusDict = GetCountDataByField(resultTable, listProjects.Fields[Fields.ProjectStatusInProject].InternalName, listProjects.Fields[Fields.Form].InternalName, true);

                GenerateListData(taDict, listReport, "Area");

                GenerateListData(statusDict, listReport, "Status");
            }
        }

        private Dictionary<string, int> GetCountDataByField(DataTable table, string fieldName, bool isMultiSelectLookup)
        {
            string[] fieldNames = null;
            Dictionary<string, int> result = new Dictionary<string, int>();

            if (IsTableNull(table)) return result;
            else
                if (fieldName.IndexOf("&") != -1)
                {
                    fieldNames = fieldName.Split('&');
                    foreach (string name in fieldNames)
                    {
                        if (!table.Columns.Contains(name))
                        {
                            return result;
                        }
                    }

                }
                else
                {
                    if (!table.Columns.Contains(fieldName))
                    {
                        return result;
                    }
                }



            foreach (DataRow dr in table.Rows)
            {
                string targetValue = null;
                if (fieldNames != null)
                {
                    //targetValue = isMultiSelectLookup ? ParseMultiSelectValue(dr[name]) : Convert.ToString(dr[name]);


                    foreach (string name in fieldNames)
                    {
                        if (isMultiSelectLookup)
                        {
                            targetValue += ParseMultiSelectValue(dr[name]) + "&";
                        }
                        else
                        {
                            targetValue += Convert.ToString(dr[name]) == null ? NoneSelectValue : Convert.ToString(dr[name]) + "&";
                        }
                    }
                    targetValue = targetValue.TrimEnd('&');

                }
                else { targetValue = isMultiSelectLookup ? ParseMultiSelectValue(dr[fieldName]) : Convert.ToString(dr[fieldName]); }

                if (string.IsNullOrEmpty(targetValue))
                {
                    targetValue = NoneSelectValue;
                }
                if (result.ContainsKey(targetValue))
                {
                    result[targetValue]++;
                }
                else
                {
                    result.Add(targetValue, 1);
                }

            }

            return result;
        }

        private Dictionary<KeyValuePair<string, string>, int> GetCountDataByField(DataTable table, string countFieldName, string formTypeFieldName, bool isMultiSelectLookup)
        {
            Dictionary<KeyValuePair<string, string>, int> result = new Dictionary<KeyValuePair<string, string>, int>();

            if (IsTableNull(table)) return result;
            else if (!table.Columns.Contains(countFieldName) || !table.Columns.Contains(formTypeFieldName))
                return result;

            //Loop all data rows
            foreach (DataRow dr in table.Rows)
            {
                string targetValue = string.Empty;
                string formTypeString = string.Empty;

                targetValue = isMultiSelectLookup ? ParseMultiSelectValue(dr[countFieldName]) : Convert.ToString(dr[countFieldName]);
                formTypeString = Convert.ToString(dr[formTypeFieldName]) == null ? NoneSelectValue : Convert.ToString(dr[formTypeFieldName]);

                if (string.IsNullOrEmpty(targetValue))
                {
                    targetValue = NoneSelectValue;
                }
                KeyValuePair<string, string> keyItem = new KeyValuePair<string, string>(targetValue, formTypeString);

                if (result.ContainsKey(keyItem))
                {
                    result[keyItem]++;
                }
                else
                {
                    result.Add(keyItem, 1);
                }

            }

            return result;
        }

        private string ParseMultiSelectValue(object sourceString)
        {
            if (null == sourceString || string.IsNullOrEmpty(sourceString.ToString())) return string.Empty;
            string tmpString = (string)sourceString;
            //TPP completion due;#2;#BC1 sign-off due;#12;#DD Report Final Draft due
            string[] valueArray = tmpString.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> valueList = new List<string>();
            for (int index = 0; index < valueArray.Length; index = index + 2)
            {
                var statusString = valueArray[index];
                valueList.Add(statusString);
            }

            valueList.Sort();
            string result = string.Empty;
            foreach (var valueItem in valueList)
            {
                result = result + ";" + valueItem;
            }
            if (result.StartsWith(";")) result = result.Substring(1);
            return result;
        }
        private bool IsTableNull(DataTable dt)
        {
            return (dt == null || dt.Rows == null || dt.Rows.Count <= 0);
        }
        /// <summary>
        /// Generate list items 
        /// </summary>
        /// <param name="countData"></param>
        /// <param name="dataList"></param>
        /// <param name="itemCategory"></param>
        private void GenerateListData(Dictionary<string, int> countData, SPList dataList, string itemCategory)
        {
            if (!(countData != null && countData.Count > 0 && dataList != null && !string.IsNullOrEmpty(itemCategory))) return;

            //TODO:Change to batch update
            //Generate item
            foreach (var item in countData)
            {
                SPListItem newItem = dataList.AddItem();
                if (item.Key.IndexOf("&") != -1)
                {
                    string title = item.Key.Split('&')[0];
                    string form = item.Key.Split('&')[1];
                    newItem[SPBuiltInFieldId.Title] = title;
                    newItem["Form"] = form;
                }
                else
                {
                    newItem[SPBuiltInFieldId.Title] = item.Key;
                }
                newItem["Count"] = item.Value;
                newItem["DataType"] = itemCategory;
                newItem.Update();
            }
        }

        /// <summary>
        /// Generate list items 
        /// </summary>
        /// <param name="countData"></param>
        /// <param name="dataList"></param>
        /// <param name="itemCategory"></param>
        private void GenerateListData(Dictionary<KeyValuePair<string, string>, int> countData, SPList dataList, string itemCategory)
        {
            if (!(countData != null && countData.Count > 0 && dataList != null && !string.IsNullOrEmpty(itemCategory))) return;

            //Generate item
            foreach (var item in countData)
            {
                SPListItem newItem = dataList.AddItem();
                KeyValuePair<string, string> keyItem = item.Key;
                string title = keyItem.Key;
                string formType = keyItem.Value;

                newItem[SPBuiltInFieldId.Title] = title;
                newItem["Form"] = formType;
                newItem["Count"] = item.Value;
                newItem["DataType"] = itemCategory;
                newItem.Update();
            }
        }
        #endregion

       
    }
}