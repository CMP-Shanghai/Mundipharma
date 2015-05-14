using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Utilities;
using MR.SP.DueDiligence.Framework;
using System;
using System.Collections.Generic;

namespace MR.SP.DueDiligence.Branding
{
    public class BaseFeatureReceiver : SPFeatureReceiver
    {
        private const string DefaultWelcomePage = "Pages/Default.aspx";
        private const string DiligencePortalWelcomePage = "SitePages/DiligenceHome.aspx";

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
                    string masterPageUrl = GetMasterPageUrl(web, "_catalogs/masterpage/DiligencePortal.master");
                    web.CustomMasterUrl = masterPageUrl;
                    web.Update();

                    string welcomePageUrl = DiligencePortalWelcomePage;
                    //Set Welcome Page
                    //SetWelcomePage(publishingWeb, welcomePageUrl);
                }
            }
            catch (Exception ex)
            {
                //ULSLogger.LogToOperations(ex, string.Format("try to activate branding feature"), 100, EventSeverity.Error);
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

                    if (IsWikiHomePage)
                    {
                        #region SitePages
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
                            string masterPageUrl = GetMasterPageUrl(web, "_catalogs/masterpage/seattle.master");
                            web.CustomMasterUrl = masterPageUrl;
                            web.Update();

                            //Restore landing page
                            //SetWelcomePage(publishingWeb, DefaultWelcomePage);
                            //Virtual methods
                            BeforeRemoveFiles(publishingWeb);
                            foreach (var item in PagesUrl)
                            {
                                RemoveFiels(publishingWeb, item);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Wiki Home page
                        //if (PagesUrl != null && PagesUrl.Count > 0)
                        //{
                        //    //Get Pages library name and url
                        //    SPList pagesList = web.Lists["SitePages"];
                        //    if (pagesList != null)
                        //    {
                        //        PagesListName = pagesList.Title;
                        //        PagesListUrl = pagesList.RootFolder.Url;
                        //    }

                        //    //Restore master page
                        //    //Set Master Page
                        //    string masterPageUrl = GetMasterPageUrl(web, "_catalogs/masterpage/seattle.master");
                        //    web.CustomMasterUrl = masterPageUrl;
                        //    web.Update();

                        //    //Restore landing page
                        //    SetWelcomePage(publishingWeb, DefaultWelcomePage);
                        //    //Virtual methods
                        //    BeforeRemoveFiles(publishingWeb);
                        //    foreach (var item in PagesUrl)
                        //    {
                        //        RemoveFiels(publishingWeb, item);
                        //    }
                        //}
                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {
                //ULSLogger.LogToOperations(ex, string.Format("try to deactivating branding feature"), 100, EventSeverity.Error);
                SPUtility.TransferToErrorPage(ex.Message);
            }

            base.FeatureDeactivating(properties);
        }
        #endregion

        public virtual List<string> PagesUrl { get; set; }

        public virtual string PagesListName { get; set; }
        public virtual string PagesListUrl { get; set; }

        public virtual bool IsWikiHomePage { get; set; }
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
            FeatureHelper.RemoveFiles(publishingWeb, replacedUrl);
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

