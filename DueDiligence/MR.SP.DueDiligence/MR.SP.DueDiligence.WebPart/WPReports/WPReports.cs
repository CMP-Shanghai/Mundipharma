using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MR.SP.DueDiligence.WebPart.WPReports
{
    [ToolboxItemAttribute(false)]
    public class WPReports : System.Web.UI.WebControls.WebParts.WebPart
    {
        DropDownList selectArea = new DropDownList();
        DropDownList selectStatus = new DropDownList();
        Button btnSelect = new Button();
        ListViewByQuery lvbq = new ListViewByQuery();

        protected void Page_Load(object sender, EventArgs e)
        {


            
        }
        protected override void CreateChildControls()
        {

            ListItemCollection lis = new ListItemCollection();
            lis.Add("All...");
            lis.Add("Project Initiated");
            lis.Add("CFPA Complete");
            lis.Add("SWOT and TPP Complete");
            lis.Add("Questions and Documents request in progress");
            lis.Add("BC1 complete");
            lis.Add("Agenda complete");
            lis.Add("Questions and Documents request complete");
            lis.Add("On site minutes");
            lis.Add("Workbook complete");
            lis.Add("Due diligence report in review");
            lis.Add("Due diligence report  complet");
            lis.Add("BC2 Complete");
            lis.Add("Project Approved");
            lis.Add("Project Terminated");

            selectStatus.DataSource = lis;
            selectStatus.DataBind();

            selectAreaDataBind();

            Button btnSelect = new Button();
            btnSelect.Text = "Show";
            btnSelect.Click += btnSelect_Click;


            using (SPSite site = SPContext.Current.Site)
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["DueDiligenceProjects"];
                    BindListView(web, list);
                }
            }

            Panel pnlDiv = new Panel();

            Table tab = new Table();
            TableRow tr;
            tr = new TableRow();
            TableCell tc;
            tc = new TableCell();
            tc.Text = "Therapeutic area";
            tr.Cells.Add(tc);
            
            
            tc = new TableCell();
            tc.Controls.Add(selectArea);
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Status";
            tr.Cells.Add(tc);
            
            tc = new TableCell();
            tc.Controls.Add(selectStatus);
            tr.Cells.Add(tc);

            
            tc = new TableCell();
            tc.Controls.Add(btnSelect);
            tr.Cells.Add(tc);
            tab.Rows.Add(tr);

            tr = new TableRow();
            tc = new TableCell();
            tc.ColumnSpan = 5;
            tc.Controls.Add(lvbq);
            tr.Cells.Add(tc);   
            tab.Rows.Add(tr);

            pnlDiv.Controls.Add(tab);
            this.Controls.Add(pnlDiv);

        }

       private void selectAreaDataBind()
        {
            using (SPSite site=SPContext.Current.Site)
            {
                using (SPWeb web=site.OpenWeb())
                {
                    SPList listArea = web.Lists["Therapeutic Area"];
                    SPListItemCollection items = listArea.Items;
                    foreach (SPListItem item in items)
                    {
                        selectArea.Items.Add(item.Title);
                    }
                    selectArea.Items.Insert(0, "All...");
                }
            }
        }

        void btnSelect_Click(object sender, EventArgs e)
        {
            string query = GetQueryStr();
            using (SPSite site = SPContext.Current.Site)
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["DueDiligenceProjects"];
                    BindListView(web, list, query);

                }
            }
        }


        private string GetQueryStr()
        {
            string keyArea = selectArea.SelectedValue;
            string keyStatus = selectStatus.SelectedValue;

            string queryAnd = @"<Where>
				            <And>
					            <Eq>
						            <FieldRef Name='Therapeutic_x0020_Area'/>
						            <Value Type='Text'>" + keyArea + @"</Value>
					            </Eq>
					            <Eq>
						            <FieldRef Name='Project_x0020_Status'/>
						            <Value Type='Text'>" + keyStatus + @"</Value>
					            </Eq>
				            </And>
			            </Where>";

            string queryOne = @"<Where>
					            <Eq>
						            <FieldRef Name='{field}'/>
						            <Value Type='Text'>{value}</Value>
					            </Eq>
			            </Where>";
            string query = string.Empty;
            if (keyArea != "All..." && keyStatus != "All...")
            {
                query = queryAnd;
            }
            else if (keyArea != "All...")
            {
                query = queryOne.Replace("{field}", "Therapeutic_x0020_Area").Replace("{value}", keyArea);
            }
            else if (keyStatus != "All...")
            {
                query = queryOne.Replace("{field}", "Project_x0020_Status").Replace("{value}", keyStatus);
            }

            return query;
        }




        private void BindListView(SPWeb web, SPList list)
        {

            lvbq.List = list;
            lvbq.DisableSort = true;
            lvbq.DisableFilter = true;
            SPQuery query = new SPQuery(lvbq.List.Views["Projects by Date"]);
            query.RowLimit = 30;
            //query.ViewFields = "<FieldRef Name=\"ID\"/><FieldRef Name=\"LinkTitle\"/><FieldRef Name=\"_x007a_m13\"/><FieldRef Name=\"_x006c_d55\"/>";
            //query.Query = "<GroupBy Collapse=\"TRUE\"><FieldRef Name=\"Project_x0020_Status\"/></GroupBy>";
            lvbq.Query = query;
        }

        private void BindListView(SPWeb web, SPList list, string queryStr)
        {

            lvbq.List = list;
            lvbq.DisableSort = true;
            lvbq.DisableFilter = true;
            SPQuery query = new SPQuery(lvbq.List.Views["Projects by Date"]);
            query.RowLimit = 30;
            //query.ViewFields = "<FieldRef Name=\"ID\"/><FieldRef Name=\"LinkTitle\"/><FieldRef Name=\"_x007a_m13\"/><FieldRef Name=\"_x006c_d55\"/>";
            query.Query = queryStr;
            lvbq.Query = query;
        }




    }
}
