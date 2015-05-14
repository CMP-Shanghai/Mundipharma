using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Specialized;
using System.Collections;
using MR.SP.DueDiligence.Framework.Const;

namespace MR.SP.DueDiligence.Pages.Layouts.MR.SP.DueDiligence.Pages.ProjectList.view
{
    public partial class OngoingProjects : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = curSite.OpenWeb(SPContext.Current.Web.ID))
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists[ListName.Project];
                        SPViewCollection views = list.Views;
                        

                        Hashtable ht = GetAllViewInfos();
                        foreach (DictionaryEntry h in ht)
                        {
                            string viewName = h.Key.ToString();
                            StringCollection viewFields = new StringCollection();
                            SPView view = views[h.Key.ToString()];
                            views.Delete(view.ID);
                            Hashtable htField = GetAllFields();
                            string query = h.Value.ToString();
                            viewFields=(StringCollection)htField[h.Key];
                            views.Add(viewName, viewFields, query, 5, true, false);
                           
                        }
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
            
            

        }



        private Hashtable GetAllFields()
        {
            
            StringCollection sc = new StringCollection();
            sc.Add("ID");
            sc.Add("Opportunity");
            sc.Add("Project Type");
            sc.Add("Start");
            sc.Add("Project Status");
            sc.Add("End");
            sc.Add("Actual End");
            sc.Add("Deviation (days)");
            sc.Add("Territories");
            sc.Add("Country");
            Hashtable ht = new Hashtable();
            ht.Add("Ongoing Projects", sc);
            ht.Add("Draft", sc);
            ht.Add("Terminated", sc);
            ht.Add("Completed Projects", sc);

            StringCollection sc1 = new StringCollection();
            sc1.Add("ID");
            sc1.Add("Opportunity");
            sc1.Add("Project Type");
            sc1.Add("Start");
            sc1.Add("Project Status");
            sc1.Add("End");
            sc1.Add("Actual End");
            sc1.Add("Deviation (days)");
            sc1.Add("Territories");
            sc1.Add("Country");
            sc1.Add("Action");
            sc1.Add("Therapeutic Area");
            ht.Add("All Items", sc1);
            ht.Add("Action Item", sc1);

            StringCollection sc2 = new StringCollection();
            sc2.Add("ID");
            sc2.Add("Opportunity");
            sc2.Add("Project Type");
            sc2.Add("Start");
            sc2.Add("Project Status");
            sc2.Add("End");
            sc2.Add("Actual End");
            sc2.Add("Deviation (days)");
            sc2.Add("Territories");
            sc2.Add("Country");
            sc2.Add("UpcomingEvents");
            ht.Add("Upcoming Events", sc2);
            return ht;
        }


        private Hashtable GetAllViewInfos()
        {
            Hashtable ht = new Hashtable();
            ht.Add("Upcoming Events","<GroupBy Collapse=\"TRUE\" GroupLimit=\"30\"><FieldRef Name=\"Therapeutic_x0020_Area\"/></GroupBy><OrderBy><FieldRef Name=\"ID\" Ascending=\"FALSE\"/></OrderBy><Where><And><Or><Or><Or><Contains><FieldRef Name=\"BD_x0020_Lead\"/><Value Type=\"Integer\"><UserID/></Value></Contains><Contains><FieldRef Name=\"Commercial_x0020_Head\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"R_x0026_D_x0020_Executive_x0020_\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"_x0069_yy7\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><IsNotNull><FieldRef Name=\"result\"/></IsNotNull></And></Where>");
            ht.Add("Ongoing Projects", "<GroupBy Collapse=\"TRUE\" GroupLimit=\"30\"><FieldRef Name=\"Therapeutic_x0020_Area\"/></GroupBy><OrderBy><FieldRef Name=\"ID\" Ascending=\"FALSE\"/></OrderBy><Where><And><And><And><Or><Or><Or><Contains><FieldRef Name=\"BD_x0020_Lead\"/><Value Type=\"Integer\"><UserID/></Value></Contains><Contains><FieldRef Name=\"Commercial_x0020_Head\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"R_x0026_D_x0020_Executive_x0020_\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"_x0069_yy7\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Neq><FieldRef Name=\"Project_x0020_Status\"/><Value Type=\"Choice\">Project Terminated</Value></Neq></And><Neq><FieldRef Name=\"Project_x0020_Status\"/><Value Type=\"Choice\">Project Approved</Value></Neq></And><IsNotNull><FieldRef Name=\"Project_x0020_Status\"/></IsNotNull></And></Where>");
            ht.Add("Completed Projects","<GroupBy Collapse=\"TRUE\" GroupLimit=\"30\"><FieldRef Name=\"Therapeutic_x0020_Area\"/></GroupBy><OrderBy><FieldRef Name=\"ID\" Ascending=\"FALSE\"/></OrderBy><Where><And><Or><Or><Or><Contains><FieldRef Name=\"BD_x0020_Lead\"/><Value Type=\"Integer\"><UserID/></Value></Contains><Contains><FieldRef Name=\"Commercial_x0020_Head\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"R_x0026_D_x0020_Executive_x0020_\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"_x0069_yy7\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Eq><FieldRef Name=\"Project_x0020_Status\"/><Value Type=\"Choice\">Project Approved</Value></Eq></And></Where>");
            ht.Add("Action Item","<GroupBy Collapse=\"TRUE\" GroupLimit=\"30\"><FieldRef Name=\"Therapeutic_x0020_Area\"/></GroupBy><OrderBy><FieldRef Name=\"ID\" Ascending=\"FALSE\"/></OrderBy><Where><And><Or><Or><Or><And><And><And><Neq><FieldRef Name=\"Project_x0020_Status\"/><Value Type=\"Choice\">Project Approved</Value></Neq><Neq><FieldRef Name=\"Project_x0020_Status\"/><Value Type=\"Choice\">Project Terminated</Value></Neq></And><IsNotNull><FieldRef Name=\"Project_x0020_Status\"/></IsNotNull></And><IsNotNull><FieldRef Name=\"vmsc\"/></IsNotNull></And><Contains><FieldRef Name=\"BD_x0020_Lead\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"Commercial_x0020_Head\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"R_x0026_D_x0020_Executive_x0020_\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"_x0069_yy7\"/><Value Type=\"Integer\"><UserID/></Value></Contains></And></Where>");
            ht.Add("Draft", "<OrderBy><FieldRef Name=\"ID\" Ascending=\"FALSE\"/></OrderBy><Where><And><Or><Or><Or><Contains><FieldRef Name='BD_x0020_Lead' /><Value Type='Integer'><UserID /></Value></Contains><Contains><FieldRef Name='Commercial_x0020_Head' /><Value Type='Integer'><UserID /></Value></Contains></Or><Contains><FieldRef Name='R_x0026_D_x0020_Executive_x0020_' /><Value Type='Integer'><UserID /></Value></Contains></Or><Contains><FieldRef Name='_x0069_yy7' /><Value Type='Integer'><UserID /></Value></Contains></Or><IsNull><FieldRef Name='Project_x0020_Status' /></IsNull></And></Where>");
            ht.Add("Terminated", "<GroupBy Collapse=\"TRUE\" GroupLimit=\"30\"><FieldRef Name=\"Therapeutic_x0020_Area\"/></GroupBy><Where><And><Or><Or><Or><Contains><FieldRef Name='BD_x0020_Lead' /><Value Type='Integer'><UserID /></Value></Contains><Contains><FieldRef Name='Commercial_x0020_Head' /><Value Type='Integer'><UserID /></Value></Contains></Or><Contains><FieldRef Name='R_x0026_D_x0020_Executive_x0020_' /><Value Type='Integer'><UserID /></Value></Contains></Or><Contains><FieldRef Name='_x0069_yy7' /><Value Type='Integer'><UserID /></Value></Contains></Or><Eq><FieldRef Name=\"Project_x0020_Status\"/><Value Type=\"Text\">Project Terminated</Value></Eq></And></Where>");
            ht.Add("All Items","<OrderBy><FieldRef Name=\"ID\" Ascending=\"FALSE\"/></OrderBy><Where><Or><Or><Or><Contains><FieldRef Name=\"BD_x0020_Lead\"/><Value Type=\"Integer\"><UserID/></Value></Contains><Contains><FieldRef Name=\"Commercial_x0020_Head\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"R_x0026_D_x0020_Executive_x0020_\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or><Contains><FieldRef Name=\"_x0069_yy7\"/><Value Type=\"Integer\"><UserID/></Value></Contains></Or></Where>");
            return ht;


        }


    }
}
