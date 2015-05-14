using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Administration;

using Microsoft.SharePoint.Utilities;

namespace MR.SP.IdeaTracker.Branding
{
    public class ITBrandingFeatureReceiverBase : SPFeatureReceiver
    {
        //protected readonly FeatureHelper Helper = new FeatureHelper();
        private const string DefaultWelcomePage = "Pages/Default.aspx";
        private const string ITWelcomePageDefaultValue = "Pages/IdeaPortal.aspx";
        private const string ITMasterPageUrl = "_catalogs/masterpage/IdeaTrackerMasterPage.master";
        private const string DefaultMasterPageUrl = "_catalogs/masterpage/oslo.master";

        #region Override Methods
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            if (properties == null) return;

            SPSite site = null;
            SPWeb web = null;
            try
            {
                if (properties.Feature.Parent is SPSite)
                {
                    site = properties.Feature.Parent as SPSite;
                    web = site.RootWeb;
                }
                else if (properties.Feature.Parent is SPWeb)
                {
                    web = properties.Feature.Parent as SPWeb;
                }
                //
                if (web != null && PublishingWeb.IsPublishingWeb(web))
                {
                    PublishingWeb publishingWeb = PublishingWeb.GetPublishingWeb(web);

                    //Set Master Page
                    string masterPageUrl = GetMasterPageUrl(web, ITMasterPageUrl);
                    web.CustomMasterUrl = masterPageUrl;
                    web.Update();

                    string welcomePageUrl = ITWelcomePageDefaultValue;
                    //Set IdeaTracker Welcome Page
                    SetWelcomePage(publishingWeb, welcomePageUrl);
                }
            }
            catch (Exception ex)
            {
                //MRITLogger.LogToOperations(ex, string.Format("try to activate branding feature"), 100, EventSeverity.Error);
                SPUtility.TransferToErrorPage(ex.Message);
            }
            base.FeatureActivated(properties);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            //System.Diagnostics.Debugger.Launch();
            SPSite site = null;
            SPWeb web = null;
            try
            {
                if (properties.Feature.Parent is SPSite)
                {
                    site = properties.Feature.Parent as SPSite;
                    web = site.RootWeb;
                }
                else if (properties.Feature.Parent is SPWeb)
                {
                    web = properties.Feature.Parent as SPWeb;
                }
                //
                if (web != null && PublishingWeb.IsPublishingWeb(web))
                {
                    PublishingWeb publishingWeb = PublishingWeb.GetPublishingWeb(web);

                    if (PagesUrl != null && PagesUrl.Count > 0)
                    {
                        //Get Pages library name and url
                        SPList pagesList = publishingWeb.PagesList;
                        if (pagesList != null)
                        {
                            PagesListName = pagesList.Title;
                            PagesListUrl = pagesList.RootFolder.Url;
                        }
                        //Restore master page
                        //Set Master Page
                        string masterPageUrl = GetMasterPageUrl(web, DefaultMasterPageUrl);
                        web.CustomMasterUrl = masterPageUrl;
                        web.Update();

                        //Restore landing page
                        SetWelcomePage(publishingWeb, DefaultWelcomePage);
                        //Virtual methods
                        BeforeRemoveFiles(publishingWeb);
                        foreach (var item in PagesUrl)
                        {
                            RemoveFiels(publishingWeb, item);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //MRITSPELogger.LogToOperations(ex, string.Format("try to deactivating branding feature"), 100, EventSeverity.Error);
                SPUtility.TransferToErrorPage(ex.Message);
            }

            base.FeatureDeactivating(properties);
        }
        #endregion

        public virtual List<string> PagesUrl { get; set; }

        public virtual string PagesListName { get; set; }
        public virtual string PagesListUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingWeb"></param>
        protected virtual void BeforeRemoveFiles(PublishingWeb publishingWeb)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingWeb"></param>
        /// <param name="pageUrl"></param>
        protected virtual void RemoveFiels(PublishingWeb publishingWeb, string pageUrl)
        {
            string replacedUrl = string.Format(pageUrl, PagesListUrl);
            ITBrandingFeatureHelper.RemoveFiles(publishingWeb, replacedUrl);
        }

        /// <summary>
        /// Set Publishing web welcome page
        /// </summary>
        /// <param name="publishingWeb"></param>
        /// <param name="pageUrl"></param>
        protected void SetWelcomePage(PublishingWeb publishingWeb, string pageUrl)
        {

            SPFile newFile = publishingWeb.Web.GetFile(pageUrl);
            if (newFile != null)
            {
                publishingWeb.DefaultPage = newFile;
                publishingWeb.Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        private static string GetMasterPageUrl(SPWeb web, string pageUrl)
        {
            string siteUrlPrefix = string.Compare("/", web.Site.ServerRelativeUrl, StringComparison.InvariantCultureIgnoreCase) == 0 ? string.Empty : web.Site.ServerRelativeUrl;

            return string.Format("{0}/{1}", siteUrlPrefix, pageUrl);
        }
    }


    /// <summary>
    /// Entity for Module information
    /// </summary>
    internal class Module
    {
        public string Name { get; set; }
        public string ProvisioningUrl { get; set; }
        public string PhysicalPath { get; set; }
        public File[] Files { get; set; }

        public class File
        {
            public string Name { get; set; }
            public Dictionary<string, string> Properties { get; set; }
        }
    }
}
