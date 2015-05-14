using Microsoft.Office.Server.Audience;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using MR.SP.DueDiligence.Framework;

namespace MR.SP.DueDiligence.Branding.Features.MR.SP.DueDiligence.CreateLeftNavigation
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("4bbe5f66-531c-4b5d-9c3d-c409fd40dc97")]
    public class MRSPDueDiligenceEventReceiver : SPFeatureReceiver
    {
        #region Receiver Methods
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                // Get the website where the feature is activated.
                SPWeb web = properties.Feature.Parent as SPWeb;
                if (web == null)
                    return;
                DeletCurrentNodes(web);
                CreateQuickLaunchLinks(web);

                UpdateCurrNavigationSettings(web);
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }

        }

        private const string StyleLibraryName = "Style Library";
        private const string NavigationConfigFolderName = "CustomSiteNavigation";
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                // Get the website where the feature is deactivating.
                SPWeb web = properties.Feature.Parent as SPWeb;
                if (web == null)
                    return;

                //Delete Folder
                #region Remove folder
                SPList oList = web.Lists.TryGetList(StyleLibraryName);
                if (oList != null)
                {
                    var folders = GetListFoders(oList);
                    foreach (SPFolder folder in folders)
                    {
                        if (folder.Name == NavigationConfigFolderName)
                        {
                            Console.WriteLine("Folder {0} is deleted on current web {1} ({2}). ", folder.Name, web.Url, web.Title);
                            folder.Delete();
                            break;
                        }
                    }
                }
                oList.Update();
                #endregion

                DeletCurrentNodes(web);
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
            }
        }

        #endregion

        #region Internal Method
        /// <summary>
        /// Delete Current Links From Quick Launch Links
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private void DeletCurrentNodes(SPWeb web)
        {
            List<int> currentNodesID = new List<int>();
            SPNavigationNodeCollection spNodesCollection = web.Navigation.QuickLaunch;
            foreach (SPNavigationNode node in spNodesCollection)
            {
                currentNodesID.Add(node.Id);
            }

            foreach (int currentNodeID in currentNodesID)
            {
                SPNavigationNode node = web.Navigation.GetNodeById(currentNodeID);
                spNodesCollection.Delete(node);
            }

        }
        /// <summary>
        /// Get Config File
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private XDocument GetConfigFile(SPWeb web)
        {
            XDocument xdoc = new XDocument();
            string sitUrl = web.Site.Url;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(sitUrl))
                {
                    using (SPWeb openweb = site.OpenWeb())
                    {
                        Uri xmlpath = new Uri(openweb.Url + "/Style%20Library/CustomSiteNavigation/CreateSiteNavigation.xml");
                        SPFile file = openweb.GetFile(xmlpath.AbsoluteUri);
                        MemoryStream mStream = new MemoryStream(file.OpenBinary());
                        StreamReader reader = new StreamReader(mStream, true);
                        xdoc = XDocument.Load(reader, LoadOptions.None);
                    }
                }
            });
            return xdoc;
        }
        /// <summary>
        /// Create Quick Launch Links
        /// </summary>
        private void CreateQuickLaunchLinks(SPWeb web)
        {

            SPNavigationNodeCollection spNodesCollection = web.Navigation.QuickLaunch;

            XDocument xdoc = GetConfigFile(web);

            var headingNodes = (from e in xdoc.Descendants("Heading")
                                select new
                                {
                                    title = e.Attribute("Title").Value,
                                    url = e.Attribute("Url").Value,
                                    spAudienceTitles = e.Attribute("spAudienceTitles").Value,
                                    spGroupNames = e.Attribute("spGroupNames").Value,
                                    adGroupNames = e.Attribute("adGroupNames").Value,
                                    description = e.Attribute("Description").Value,
                                    target = e.Attribute("Target").Value,
                                    NavLinks = (
                                        from n in e.Elements("NavLink")
                                        select new
                                        {
                                            navTitle = n.Attribute("Title").Value,
                                            navUrl = n.Attribute("Url").Value,
                                            navspAudienceTitles = n.Attribute("spAudienceTitles").Value,
                                            navspGroupNames = n.Attribute("spGroupNames").Value,
                                            navadGroupNames = n.Attribute("adGroupNames").Value,
                                            navdescription = n.Attribute("Description").Value,
                                            navtarget = n.Attribute("Target").Value
                                        }
                                    ).ToArray()
                                }).ToList();



            foreach (var headingNode in headingNodes)
            {

                string navigationUrl = EnsureCurrentWebUrl(web, headingNode.url);
                SPNavigationNode parentNode = new SPNavigationNode(headingNode.title, navigationUrl, true);
                spNodesCollection.AddAsLast(parentNode);
                
                UpdateNavigationNodeProperties(parentNode, headingNode.description, headingNode.target, headingNode.spAudienceTitles, headingNode.spGroupNames, headingNode.adGroupNames);



                foreach (var navNode in headingNode.NavLinks)
                {
                    string childNaviagionUrl = EnsureCurrentWebUrl(web, navNode.navUrl);
                    SPNavigationNode childNode = new SPNavigationNode(navNode.navTitle, childNaviagionUrl, true);

                    //childNode.TargetSecurityScopeId
                    parentNode.Children.AddAsLast(childNode);
                    
                    UpdateNavigationNodeProperties(childNode, navNode.navdescription, navNode.navtarget, navNode.navspAudienceTitles, navNode.navspGroupNames, navNode.navadGroupNames);
                }

            }

        }

        private string EnsureCurrentWebUrl(SPWeb web, string urlString)
        {
            string webUrlToken = "{WebUrl}";
            string result = urlString;
            string webUrl = web.Url;

            if (string.Compare(webUrl, "/", true) == 0)
            {
                result = urlString.Replace(webUrlToken, string.Empty);
            }
            else
            {
                result = urlString.Replace(webUrlToken, webUrl);
            }

            return result;
        }

        /// <summary>
        /// Update navigation node properties
        /// </summary>
        /// <param name="node"></param>
        /// <param name="description"></param>
        /// <param name="target"></param>
        private void UpdateNavigationNodeProperties(SPNavigationNode node, string description, string target, string spAudienceIDs, string spGroupNames, string adGroupNames)
        {

            if (node.Properties.ContainsKey("Description"))
            {
                node.Properties["Description"] = description;
            }
            else
            {
                node.Properties.Add("Description", description);
            }
            if (node.Properties.ContainsKey("Target"))
            {
                node.Properties["Target"] = target;
            }
            else
            {
                node.Properties.Add("Target", target);
            }
            var audienceValue = GetNavigationNodeAudience(spAudienceIDs, spGroupNames, adGroupNames);
            if (node.Properties.ContainsKey("Audience"))
            {
                node.Properties["Audience"] = audienceValue;
            }
            else
            {
                node.Properties.Add("Audience", audienceValue);
            }
            node.Update();
        }
        /// <summary>
        /// Update navigation node audience /*Give permissions to the link*/
        /// </summary>
        /// <param name="web"></param>
        /// <param name="node"></param>
        /// <param name="spAudienceIDs">SharePoint Audience ID. The delimiter , is used to specify multiple values</param>
        /// <param name="spGroupNames">SP Group Name. The delimiter , is used to specify multiple values</param>
        /// <param name="adGroupNames">AD Group Name. The delimiter \n is used to specify multiple values</param>
        private string GetNavigationNodeAudience(string spAudienceIDs, string spGroupNames, string adGroupNames)
        {
            var audienceProps = new Dictionary<string, string>();
            audienceProps["SharepointAudienceID"] = spAudienceIDs;
            audienceProps["SharepointGroup"] = spGroupNames;
            audienceProps["ActiveDirectoryGroupObjectLDAPPath"] = adGroupNames;
            var audienceValue = string.Format("{0};;{1};;{2}", audienceProps["SharepointAudienceID"], audienceProps["ActiveDirectoryGroupObjectLDAPPath"], audienceProps["SharepointGroup"]);
            return audienceValue;
        }
        /// <summary>
        /// Get Audience GUid Base On Display Name
        /// </summary>
        /// <param name="spSite"></param>
        /// <param name="audienceTitle"></param>
        /// <returns></returns>
        private string GetAudienceGuidBasedOnTitle(SPSite spSite, string audienceTitle)
        {
            SPServiceContext context = SPServiceContext.GetContext(spSite);
            AudienceManager audManager = new AudienceManager(context);
            Audience audience = audManager.Audiences[audienceTitle];
            return audience.AudienceID.ToString();
        }
        /// <summary>
        /// Get Audience GUids Base On multiple Display Name
        /// </summary>
        /// <param name="spSite"></param>
        /// <param name="audienceTitle"></param>
        /// <returns></returns>
        private string GetAudienceGuidsBasedOnTitle(SPSite spSite, string audienceTitles)
        {
            string[] audienceTitleArray = audienceTitles.Split(';');
            SPServiceContext context = SPServiceContext.GetContext(spSite);
            AudienceManager audManager = new AudienceManager(context);
            string audienceIDs = string.Empty;
            foreach (string audienceTitle in audienceTitleArray)
            {
                Audience audience = audManager.Audiences[audienceTitle];
                audienceIDs = audience.AudienceID.ToString() + ",";
            }
            return audienceIDs;
        }
        /// <summary>
        ///updating current navigation settings
        /// </summary>
        /// <param name="web"></param>
        private void UpdateCurrNavigationSettings(SPWeb web)
        {
            PublishingWeb pubWeb = PublishingWeb.GetPublishingWeb(web);
            PortalNavigation portalNav = pubWeb.Navigation;
            XDocument xdoc = GetConfigFile(web); ;
            var options = from o in xdoc.Descendants("Navigation")
                          select new
                          {
                              InheritCurrent = o.Attribute("InheritCurrent").Value,
                              ShowSiblings = o.Attribute("ShowSiblings").Value,
                              CurrentIncludeSubSites = o.Attribute("CurrentIncludeSubSites").Value,
                              CurrentIncludePages = o.Attribute("CurrentIncludePages").Value,
                              CurrentDynamicChildLimit = o.Attribute("CurrentDynamicChildLimit").Value,
                              OrderingMethod = o.Attribute("OrderingMethod").Value
                          };
            foreach (var option in options)
            {
                portalNav.InheritCurrent = Boolean.Parse(option.InheritCurrent);
                portalNav.ShowSiblings = Boolean.Parse(option.ShowSiblings);
                portalNav.CurrentIncludeSubSites = Boolean.Parse(option.CurrentIncludeSubSites);
                portalNav.CurrentIncludePages = Boolean.Parse(option.CurrentIncludePages);
                portalNav.CurrentDynamicChildLimit = int.Parse(option.CurrentDynamicChildLimit);
                portalNav.OrderingMethod = (OrderingMethod)Enum.Parse(typeof(OrderingMethod), option.OrderingMethod);
            }
            pubWeb.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private IEnumerable<SPFolder> GetListFoders(SPList list)
        {
            return list.Folders.OfType<SPListItem>().Select(item => item.Folder);
        }
        #endregion
    }
}
