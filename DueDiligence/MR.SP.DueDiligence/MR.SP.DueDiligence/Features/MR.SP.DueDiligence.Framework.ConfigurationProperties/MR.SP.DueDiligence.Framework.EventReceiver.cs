using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Collections;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace MR.SP.DueDiligence.Framework.Features.MR.SP.DueDiligence.Framework.ConfigurationProperties
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("0e74ef72-a675-4813-a706-56157ceac3fc")]
    public class MRSPDueDiligenceFrameworkEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            bool isContextNull = false;
            SPSite curSite;
            SPWeb curWeb;
            try
            {
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
                        bool result = PopulateConfigurationSettings(curWeb, settingsList);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="curWeb"></param>
        /// <param name="settingsList"></param>
        /// <returns></returns>
        private static bool PopulateConfigurationSettings(SPWeb curWeb, Dictionary<string, string> settingsDic)
        {
            //throw new NotImplementedException();
            bool result = false;

            if (null == curWeb || null == settingsDic || settingsDic.Count <= 0) return false;

            foreach (var item in settingsDic)
            {
                string keyName = item.Key;
                string keyValue = item.Value;
                if (!curWeb.AllProperties.Contains(keyName))
                {
                    curWeb.AllProperties.Add(keyName, keyValue);
                    result = true;
                }
            }
            if (result)
            {
                curWeb.Update();
            }

            return result;
        }
    }
}
