using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Web;
using System.Collections.Generic;
using Microsoft.SharePoint.Utilities;
using System.IO;

namespace MR.SP.DueDiligence.Pages.Features.MR.SP.DueDiligence.IndicationInsights
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("7c78e7db-e722-4f06-b1ee-1552762884c8")]
    public class MRSPDueDiligenceEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        private const string WebPartToken = "[WebPartToken]";
        private const string WebPartToken1 = "[WebPartToken1]";
        private const string DefaultContent = "<table><tr><td width=\"66%\" valign=\"top\"><div style=\"font-size:2em;\">{0}</div><div style=\"font-style:italic; padding-left:8px; padding-top:4px\"></div><br/>"
            + WebPartToken + "<br/>"
            + WebPartToken1 
            + "</td><td></td></tr></table>";

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

                    Dictionary<string, string> settingsList = new Dictionary<string, string>();
                    foreach (SPFeatureProperty p in pCollection)
                    {
                        string keyName = p.Name;
                        string defaultValue = p.Value;

                        settingsList.Add(keyName, defaultValue);
                    }
                    if (settingsList.Count > 0)
                    {
                        bool result = CreateSitePage.FunctionalAreaPageProvision(curWeb, settingsList,DefaultContent, false);
                    }
                }

            }
            catch (Exception ex)
            {
                SPUtility.TransferToErrorPage(ex.ToString());
            }
            finally
            {
                if (isContextNull) HttpContext.Current = null;
            }

        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
