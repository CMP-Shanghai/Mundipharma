using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using MR.SP.DueDiligence.Framework.Const;

namespace MR.SP.DueDiligence.Pages.Features.MR.SP.DueDiligence.Pages.ChangeForm
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("0453d231-2a05-410a-b9be-f80199081c1b")]
    public class MRSPDueDiligencePagesEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        SPSite curSite;
        SPWeb curWeb;
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
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
            if (curWeb != null)
            {
                curWeb.AllowUnsafeUpdates = true;
                SPList listProject = curWeb.Lists[ListName.Project];
                SPContentTypeCollection ctcs = listProject.ContentTypes;
                if (ctcs != null && ctcs.Count > 0)
                {
                    ctcs["DueDiligenceProjects"].NewFormTemplateName = "CustomProjectFormNew";
                    ctcs["DueDiligenceProjects"].EditFormTemplateName = "CustomProjectFormEdit";
                    ctcs["DueDiligenceProjects"].DisplayFormTemplateName = "CustomProjectFormDisplay";
                    ctcs["DueDiligenceProjects"].Update();
                }
                listProject.Update();
                curWeb.AllowUnsafeUpdates = false;
                
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
