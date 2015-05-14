using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Utilities;
using MR.SP.DueDiligence.Framework;
using System;
using System.Collections.Generic;

namespace MR.SP.DueDiligence.Pages
{
    public class BaseFeatureReceiver : SPFeatureReceiver
    {

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


                        //Virtual methods
                        BeforeRemoveFiles(publishingWeb);
                        foreach (var item in PagesUrl)
                        {
                            RemoveFiels(publishingWeb, item);
                        }
                    }
                    #endregion

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

