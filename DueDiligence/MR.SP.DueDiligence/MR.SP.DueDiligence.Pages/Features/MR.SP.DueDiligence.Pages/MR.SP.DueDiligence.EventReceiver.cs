using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MR.SP.DueDiligence.Pages.Features.MR.SP.DueDiligence.Pages
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("e1cedc25-732e-445d-9504-14b069cb0c96")]
    public class MRSPDueDiligenceEventReceiver : BaseFeatureReceiver
    {
        //Format: pagelistUrl/pagesName
        private static List<string> _pagesUrl = new List<string> { 
            "{0}/AML.aspx",
            "{0}/Cachexia-Insights---Today-and-Tomorrow-.aspx",
            "{0}/CLL-Insights.aspx",
            "{0}/Diligence-Process.aspx",
            "{0}/Insights-in-Anticoagulant-Reversal-Agents---Today-and-Tomorrow-.aspx",
            "{0}/NHL-Insights.aspx",
            "{0}/View-Background-Information.aspx",
            //"{0}/NormalUserHomePage.aspx"
        };
        public override List<string> PagesUrl
        {
            get
            {
                return _pagesUrl;
            }
            set
            {
                _pagesUrl = value;
            }
        }
    }
}
