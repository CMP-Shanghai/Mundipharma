using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Web;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint.WebPartPages;
using System.Xml;
using System.IO;

namespace MR.SP.DueDiligence.Pages.Features.MR.SP.DueDiligence.FunctionalAreaPages
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("fc9680cb-67fb-4dbf-8785-274e8c4d6c35")]
    public class MRSPDueDiligenceEventReceiver : SPFeatureReceiver
    {
        private const string WebPartToken = "[WebPartToken]";
        private const string DefaultContent = "<table><tr><td width=\"66%\" valign=\"top\"><div style=\"font-size:2em;\">{0}</div><div style=\"font-style:italic; padding-left:8px; padding-top:4px\"></div><br/>"
            + WebPartToken 
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
                        bool result = CreateSitePage.FunctionalAreaPageProvision(curWeb, settingsList, DefaultContent);
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


       

    }
}
