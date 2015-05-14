using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.IdeaTracker.WebParts.IdeaViewWP
{
    [ToolboxItemAttribute(false)]
    public partial class IdeaViewWP : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public IdeaViewWP()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Page.Request.QueryString["ID"]);

            LoadIdeaView(itemID);
            LoadFeedsData(itemID);
            LoadMinutesData(itemID);
            LoadAttachmentsData(itemID);
            LoadActionsData(itemID);
        }

        //Load Idea Data
        private void LoadIdeaView(int ID)
        {

            try
            {
                using (SPSite currSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currWeb = currSite.OpenWeb())
                    {
                        SPList ideasList = currWeb.Lists.TryGetList("IdeaTrackerList");

                        if (ideasList != null)
                        {
                            SPListItem _item = ideasList.GetItemById(ID);
                            ltIdeaID.Text = "["+ID.ToString()+"]";                            
                            ltIdeaTitle.Text = _item["Title"].ToString();                      

                            ltStage.Text = _item["IStage"].ToString();
                            if (_item["IDescription"].ToString() != "<div></div>") ltDescription.Text = _item["IDescription"].ToString();                            
                            if (_item["ISummary"].ToString() != "<div></div>") ltSummary.Text = _item["ISummary"].ToString();

                            SPFieldUserValue _editor = new SPFieldUserValue(currWeb, _item["Editor"].ToString());
                            ltModifiedBy.Text = _editor.User.Name;

                            SPFieldUserValueCollection _users = (SPFieldUserValueCollection)_item["IOriginator"];
                            if (_users.Count > 0)
                            {
                                string strUsers="";
                                foreach (SPFieldUserValue _user in _users)
                                {
                                    strUsers += _user.LookupValue + ", ";
                                }
                                ltOriginator.Text = strUsers.Substring(0, strUsers.Length - 2);
                            }

                            if (_item["ITheraupeticArea"] != null)
                            {
                                string strArea = "";
                                SPFieldMultiChoiceValue choices = new SPFieldMultiChoiceValue(_item["ITheraupeticArea"].ToString());
                                for (int i = 0; i < choices.Count; i++)
                                {
                                    strArea += choices[i].ToString() + ", ";
                                }
                                ltAreas.Text = strArea.Substring(0, strArea.Length - 2);
                            }

                           
                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }

        }

        //Load Feeds Data
        private void LoadFeedsData(int ID)
        {

            try
            {
                using (SPSite _site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb _web = _site.OpenWeb())
                    {
                        SPList feedsList = _web.Lists.TryGetList("IdeaFeedsList");

                        if (feedsList != null)
                        {

                            Microsoft.SharePoint.WebPartPages.XsltListViewWebPart feedsWebPart = new Microsoft.SharePoint.WebPartPages.XsltListViewWebPart();
                            feedsWebPart.Title = "";
                            feedsWebPart.ListId = feedsList.ID;
                            //feedsWebPart.ViewGuid = feedsList.DefaultView.ID.ToString();

                            StringBuilder xml = new StringBuilder();
                            xml.Append("<View Name='" + feedsList.DefaultView.ID.ToString("B").ToString().ToUpper(CultureInfo.InvariantCulture) + "' MobileView='TRUE' Type='HTML'  Hidden='TRUE' DisplayName=''>");//  Url='" + Request.Url.ToString() + "'  Level='1' BaseViewID='1' ContentTypeID='0x' ImageUrl='/_layouts/images/generic.png'>");
                            xml.Append("<Query><OrderBy><FieldRef Name='Created' Ascending='FALSE'></FieldRef></OrderBy><Where><Eq><FieldRef Name='IdeaParent' LookupId='TRUE' /><Value Type='Lookup'>" + ID + "</Value></Eq></Where></Query>");
                            xml.Append("<ViewFields><FieldRef Name='Created'/><FieldRef Name='IOperator'/></ViewFields>");
                            xml.Append("<RowLimit Paged='TRUE'>20</RowLimit>");
                            xml.Append("<Aggregations Value='Off'/>");
                            xml.Append("<Toolbar Type='None'/></View>");
                            feedsWebPart.XmlDefinition = xml.ToString();//lstview.GetViewXml();
                            feedsWebPart.AllowClose = false;
                            feedsWebPart.AllowConnect = false;
                            feedsWebPart.AllowEdit = false;
                            feedsWebPart.AllowHide = false;
                            feedsWebPart.AllowMinimize = false;
                            feedsWebPart.AllowZoneChange = false;
                            feedsWebPart.ChromeType = PartChromeType.Default;
                            plFeeds.Controls.Add(feedsWebPart);

                        }

                    }
                }


            }
            catch (Exception e)
            {
                throw;
            }

        }

        //Load Minutes Data
        private void LoadMinutesData(int ID)
        {

            try
            {
                using (SPSite _site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb _web = _site.OpenWeb())
                    {
                        SPList minutesList = _web.Lists.TryGetList("IdeaMinutesList");

                        if (minutesList != null)
                        {

                            Microsoft.SharePoint.WebPartPages.XsltListViewWebPart _webPart = new Microsoft.SharePoint.WebPartPages.XsltListViewWebPart();
                            _webPart.Title = "";
                            _webPart.ListId = minutesList.ID;

                            StringBuilder xml = new StringBuilder();
                            xml.Append("<View Name='" + minutesList.DefaultView.ID.ToString("B").ToString().ToUpper(CultureInfo.InvariantCulture) + "' MobileView='TRUE' Type='HTML'  Hidden='TRUE' DisplayName=''>");
                            xml.Append("<Query><OrderBy><FieldRef Name='Created' Ascending='FALSE'></FieldRef></OrderBy><Where><Eq><FieldRef Name='IdeaParent' LookupId='TRUE' /><Value Type='Lookup'>" + ID + "</Value></Eq></Where></Query>");
                            xml.Append("<ViewFields><FieldRef Name='Created'/><FieldRef Name='IMeetingMinutes'/></ViewFields>");
                            xml.Append("<RowLimit Paged='TRUE'>20</RowLimit>");
                            xml.Append("<Aggregations Value='Off'/>");
                            xml.Append("<Toolbar Type='None'/></View>");
                            _webPart.XmlDefinition = xml.ToString();
                            _webPart.AllowClose = false;
                            _webPart.AllowConnect = false;
                            _webPart.AllowEdit = false;
                            _webPart.AllowHide = false;
                            _webPart.AllowMinimize = false;
                            _webPart.AllowZoneChange = false;
                            _webPart.ChromeType = PartChromeType.Default;
                            plMinutes.Controls.Add(_webPart);

                        }

                    }
                }


            }
            catch (Exception e)
            {
                throw;
            }

        }

        //Load Minutes Data
        private void LoadAttachmentsData(int ID)
        {

            try
            {
                using (SPSite _site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb _web = _site.OpenWeb())
                    {
                        SPList _list = _web.Lists.TryGetList("Idea Files");

                        if (_list != null)
                        {

                            Microsoft.SharePoint.WebPartPages.XsltListViewWebPart _webPart = new Microsoft.SharePoint.WebPartPages.XsltListViewWebPart();
                            _webPart.Title = "";
                            _webPart.ListId = _list.ID;

                            StringBuilder xml = new StringBuilder();
                            xml.Append("<View Name='" + _list.DefaultView.ID.ToString("B").ToString().ToUpper(CultureInfo.InvariantCulture) + "' MobileView='TRUE' Type='HTML'  Hidden='TRUE' DisplayName=''>");
                            xml.Append("<Query><OrderBy><FieldRef Name='Created' Ascending='FALSE'></FieldRef></OrderBy><Where><Eq><FieldRef Name='IdeaParent' LookupId='TRUE' /><Value Type='Lookup'>" + ID + "</Value></Eq></Where></Query>");
                            xml.Append("<ViewFields><FieldRef Name='DocIcon'/><FieldRef Name='Created'/><FieldRef Name='LinkFilename'/><FieldRef Name='Author'/><FieldRef Name='IDescription'/></ViewFields>");
                            xml.Append("<RowLimit Paged='TRUE'>20</RowLimit>");
                            xml.Append("<Aggregations Value='Off'/>");
                            xml.Append("<Toolbar Type='None'/></View>");
                            _webPart.XmlDefinition = xml.ToString();
                            _webPart.AllowClose = false;
                            _webPart.AllowConnect = false;
                            _webPart.AllowEdit = false;
                            _webPart.AllowHide = false;
                            _webPart.AllowMinimize = false;
                            _webPart.AllowZoneChange = false;
                            _webPart.ChromeType = PartChromeType.Default;
                            plAttachments.Controls.Add(_webPart);

                        }

                    }
                }


            }
            catch (Exception e)
            {
                throw;
            }

        }



        //Load Action Data
        private void LoadActionsData(int ID)
        {
            try
            {
                using (SPSite _site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb _web = _site.OpenWeb())
                    {
                        SPList _list = _web.Lists.TryGetList("IdeaActionsList");

                        if (_list != null)
                        {

                            Microsoft.SharePoint.WebPartPages.XsltListViewWebPart _webPart = new Microsoft.SharePoint.WebPartPages.XsltListViewWebPart();
                            _webPart.Title = "";
                            _webPart.ListId = _list.ID;

                            StringBuilder xml = new StringBuilder();
                            xml.Append("<View Name='" + _list.DefaultView.ID.ToString("B").ToString().ToUpper(CultureInfo.InvariantCulture) + "' MobileView='TRUE' Type='HTML'  Hidden='TRUE' DisplayName=''>");
                            xml.Append("<Query><OrderBy><FieldRef Name='Created' Ascending='FALSE'></FieldRef></OrderBy><Where><Eq><FieldRef Name='IdeaParent' LookupId='TRUE' /><Value Type='Lookup'>" + ID + "</Value></Eq></Where></Query>");
                            xml.Append("<ViewFields><FieldRef Name='Created'/><FieldRef Name='Author'/><FieldRef Name='IDescription'/><FieldRef Name='IAssigned'/></ViewFields>");
                            xml.Append("<RowLimit Paged='TRUE'>20</RowLimit>");
                            xml.Append("<Aggregations Value='Off'/>");
                            xml.Append("<Toolbar Type='None'/></View>");
                            _webPart.XmlDefinition = xml.ToString();
                            _webPart.AllowClose = false;
                            _webPart.AllowConnect = false;
                            _webPart.AllowEdit = false;
                            _webPart.AllowHide = false;
                            _webPart.AllowMinimize = false;
                            _webPart.AllowZoneChange = false;
                            _webPart.ChromeType = PartChromeType.Default;
                            plActions.Controls.Add(_webPart);

                        }

                    }
                }


            }
            catch (Exception e)
            {
                throw;
            }

        }




    }
}
