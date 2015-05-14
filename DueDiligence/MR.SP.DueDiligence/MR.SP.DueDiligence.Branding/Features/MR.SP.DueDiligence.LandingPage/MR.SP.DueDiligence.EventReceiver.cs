using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Xml;
using Microsoft.SharePoint.Utilities;
using System.Xml.Linq;
using System.IO;

namespace MR.SP.DueDiligence.Branding.Features.MR.SP.DueDiligence.LandingPage
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("632fdc61-dd5d-4185-935a-daa58056bf1a")]
    public class MRSPDueDiligenceEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

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
                if (web != null)
                {
                    Uri xmlpath = new Uri(web.Url + "/Style%20Library/DiligencePortal/landingpage/PageConfiguration.xml");
                    SPFile file = web.GetFile(xmlpath.AbsoluteUri);
                    using (MemoryStream mStream = new MemoryStream(file.OpenBinary()))
                    {

                        using (StreamReader reader = new StreamReader(mStream, true))
                        {
                            XmlDocument configDoc = new XmlDocument();
                            configDoc.Load(reader);
                            WikiPageCreator creator = new WikiPageCreator(configDoc.SelectSingleNode("//Library"));
                            creator.Deploy(web);
                        }
                    }

                    base.FeatureActivated(properties);
                }
            }
            catch (Exception ex)
            {
                //ULSLogger.LogToOperations(ex, string.Format("try to activate branding feature"), 100, EventSeverity.Error);
                SPUtility.TransferToErrorPage(ex.Message);
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
        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}



    }
}
