using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;
using MR.SP.DueDiligence.Framework;
using MR.SP.DueDiligence.Framework.Const;
//using MR.SP.DueDiligence.Framework.
using MR.SP.DueDiligence.WebPart;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace MR.SP.DueDiligence.Branding.Features.MR.SP.DueDiligence.CreateView
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("9e0ce6d3-726b-40ec-9b14-c806f2ee9379")]
    public class MRSPDueDiligenceEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        private const string AdminViewPrefix = "Admin";

        private const string WebPartToken = "[WebPartToken]";
        private const string DefaultContent = "<table><tr><td width=\"66%\" valign=\"top\">"
            + WebPartToken
            + "</td></tr></table>";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            bool isContextNull = false;
            SPSite curSite;
            SPWeb curWeb;
            try
            {
                #region Variable init
                if (properties.Feature.Parent is SPSite)
                {
                    curSite = properties.Feature.Parent as SPSite;
                    curWeb = curSite.RootWeb;
                }
                else if (properties.Feature.Parent is SPWeb)
                {
                    curWeb = properties.Feature.Parent as SPWeb;
                    curSite = curWeb.Site;
                }
                else
                {
                    //error 
                    throw new Exception("Feature scope is incorrect");
                }
                #endregion
                //read properties from configuration file,
                //import initial data while creating list
                SPFeaturePropertyCollection pCollection = properties.Feature.Properties;
                if (pCollection != null && pCollection.Count > 0)
                {

                    #region Init Context if null
                    try
                    {
                        if (HttpContext.Current == null)
                        {
                            isContextNull = true;

                            var request = new HttpRequest("", curWeb.Url, "");
                            HttpContext.Current = new HttpContext(request, new HttpResponse(new StringWriter()));
                            HttpContext.Current.Items["HttpHandlerSPWeb"] = curWeb;
                        }
                    }
                    catch (Exception inEx)
                    {
                        //SPUtility.LogCustomAppError(inEx.ToString());
                    }
                    #endregion


                    UpdateListViews(curWeb);
                    //Site Page Provision
                    SPSecurity.RunWithElevatedPrivileges(delegate
                    {
                        using (SPSite adminSite = new SPSite(curWeb.Site.ID))
                        {
                            using (SPWeb adminWeb = adminSite.OpenWeb(curWeb.ID))
                            {
                                adminWeb.AllowUnsafeUpdates = true;

                                SPList sourceList = adminWeb.Lists.TryGetList(ListName.Project);
                                SPList wikiPageList = adminWeb.Lists.TryGetList(ListName.SitePages);
                                //SPList wikiPageList = 
                                #region Site Page Provision
                                foreach (SPFeatureProperty p in pCollection)
                                {
                                    string keyName = p.Name;
                                    string propertyValue = p.Value;

                                    if (string.IsNullOrEmpty(keyName)) continue;
                                    string[] viewKeyProperties = propertyValue.Split(new string[] { ";" }, StringSplitOptions.None);
                                    //Create Wiki Page
                                    ListViewWithRole xlfWp = new ListViewWithRole
                                    {
                                        AdminViewKey = viewKeyProperties[0],
                                        ParticipantViewKey = viewKeyProperties[1],
                                        NormalUserViewKey = viewKeyProperties[2],
                                        ChromeType = PartChromeType.None
                                    };

                                    //string content = string.Format(DefaultContent, keyName);
                                    string content = string.Format(DefaultContent);
                                    List<System.Web.UI.WebControls.WebParts.WebPart> webPartCollection = new List<System.Web.UI.WebControls.WebParts.WebPart>() { xlfWp };
                                    Framework.WikiPageCreator.EnsurePage(curWeb, keyName, content, webPartCollection, sourceList, wikiPageList);
                                }

                                #endregion

                                adminWeb.AllowUnsafeUpdates = false;
                            }
                        }
                    });


                }

            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(ex.ToString());
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }
            finally
            {
                if (isContextNull) HttpContext.Current = null;
                base.FeatureActivated(properties);
            }

        }


        /// <summary>
        /// Update list view
        /// </summary>
        /// <param name="publishingWeb"></param>
        private static void UpdateListViews(SPWeb sourceWeb)
        {


            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite curSite = new SPSite(sourceWeb.Site.ID))
                {
                    using (SPWeb curweb = curSite.OpenWeb(sourceWeb.ID))
                    {
                        curweb.AllowUnsafeUpdates = true;

                        SPList list = curweb.Lists.TryGetList(ListName.Project);
                        #region List View Provision
                        SPList listViewConfiguration = curweb.Lists.TryGetList(ListName.ProjectViewConfiguration);
                        if (list != null && listViewConfiguration != null)
                        {
                            SPViewCollection views = list.Views;
                            SPListItemCollection listitemViewConfigurations = listViewConfiguration.Items;
                            if (listitemViewConfigurations != null && listitemViewConfigurations.Count > 0)
                            {
                                #region For each fields add to view
                                foreach (SPListItem item in listitemViewConfigurations)
                                {

                                    string ViewTitle = (string)item[Fields.ViewTitle];
                                    string DisplayFields = (string)item[Fields.DisplayFields];
                                    uint RowLimit = Convert.ToUInt32(item[Fields.RowLimit]);
                                    string ViewQuery = (string)item[Fields.ViewQuery];
                                    StringCollection ViewFields = new StringCollection();
                                    ViewFields.AddRange(DisplayFields.Split(','));
                                    try
                                    {
                                        SPView view = list.Views[ViewTitle];
                                        views.Delete(view.ID);
                                    }
                                    catch (Exception)
                                    {

                                    }
                                    SPView newView = views.Add(ViewTitle, ViewFields, ViewQuery, RowLimit, true, false);
                                    int shadedStyleId = 17;//Shaded
                                    newView.ApplyStyle(curweb.ViewStyles.StyleByID(shadedStyleId));
                                    newView.Update();
                                    //Update List View Web Part title and related properties
                                    #region Update List View Web Part Properties
                                    //Get Item for list view
                                    SPFile file = curweb.GetFile(newView.Url);

                                    UpdateWebPartProperties(file, newView.Title, newView.Url);
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        #endregion

                    }
                }
            });

        }

        /// <summary>
        /// 
        /// </summary>
        private static void UpdateWebPartProperties(SPFile fileItem, string viewName, string viewUrl)
        {
            #region Update List View Web Part Properties


            if (fileItem == null) return;

            SPLimitedWebPartManager webPartManager = null;

            try
            {
                #region Enum all the ListView

                webPartManager = fileItem.GetLimitedWebPartManager(PersonalizationScope.Shared);
                SPLimitedWebPartCollection webParts = webPartManager.WebParts;

                for (int curIndex = 0; curIndex < webParts.Count; curIndex++)
                {
                    try
                    {
                        var wp = webParts[curIndex];
                        if (wp is XsltListViewWebPart)
                        {
                            var lvwp = ((XsltListViewWebPart)wp);

                            #region Update Title Properties
                            lvwp.Title = viewName.StartsWith(AdminViewPrefix) ? viewName.Substring(AdminViewPrefix.Length) : viewName;
                            lvwp.ChromeType = PartChromeType.None;
                            lvwp.TitleUrl = fileItem.ServerRelativeUrl;
                            webPartManager.SaveChanges(lvwp);

                            //Find the ToolBar control and set visible to False 
                            SetToolbarType(lvwp);
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Exception from update LVWP: {0} \nSource: {1}", ex.Message, ex.Source);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }
            finally
            {
                if (webPartManager != null)
                {
                    webPartManager.Dispose();
                }
            }


            #endregion
        }

        private static void SetToolbarType(XsltListViewWebPart lvwp)
        {
            MethodInfo ensureViewMethod = lvwp.GetType().GetMethod("EnsureView", BindingFlags.Instance | BindingFlags.NonPublic);
            object[] ensureViewParams = { };
            ensureViewMethod.Invoke(lvwp, ensureViewParams);
            FieldInfo viewFieldInfo = lvwp.GetType().GetField("view", BindingFlags.NonPublic | BindingFlags.Instance);
            SPView spView = viewFieldInfo.GetValue(lvwp) as SPView;
            Type[] toolbarMethodParamTypes = { Type.GetType("System.String") };
            MethodInfo setToolbarTypeMethod = spView.GetType().GetMethod("SetToolbarType", BindingFlags.Instance | BindingFlags.NonPublic, null, toolbarMethodParamTypes, null);
            object[] setToolbarParam = { "None" };
            setToolbarTypeMethod.Invoke(spView, setToolbarParam);
            spView.Update();
        }

        //public static void SetToolbarType(this SPView view, string type)
        //{
        //    if (view == null)
        //        throw new NullReferenceException("SPView.SetToolbarType(string) cannot be called on NULL-Values.");

        //    var xDoc = new System.Xml.XmlDocument();
        //    xDoc.LoadXml(view.GetViewXml());

        //    var toolbarNodeQuery = from System.Xml.XmlNode n in xDoc.DocumentElement.ChildNodes
        //                           where n.Name == "Toolbar"
        //                           select n;


        //    System.Xml.XmlElement toolbarNode = null;
        //    if (toolbarNodeQuery.Count() == 0)
        //    {
        //        toolbarNode = xDoc.CreateElement("Toolbar");
        //        xDoc.DocumentElement.AppendChild(toolbarNode);
        //    }
        //    else
        //    {
        //        toolbarNode = xDoc.DocumentElement["Toolbar"];
        //    }
        //    toolbarNode.SetAttribute("Type", type);
        //    view.SetViewXml(xDoc.OuterXml);
        //    view.Update();
        //}
    }
}
