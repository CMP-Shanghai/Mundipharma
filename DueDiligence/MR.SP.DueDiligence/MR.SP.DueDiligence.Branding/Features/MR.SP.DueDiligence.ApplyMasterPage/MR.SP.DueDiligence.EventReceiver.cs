using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace MR.SP.DueDiligence.Branding.Features.MR.SP.DueDiligence.ApplyMasterPage
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("37a3ec3c-7218-4cc0-a591-faf094293d09")]
    public class MRSPDueDiligenceEventReceiver : BaseFeatureReceiver
    {
        private readonly List<string> _urls = new List<string>() { 
            "{0}/DiligenceHome.aspx"
           
        };

        /// <summary>
        /// Page Urls which need to be handle
        /// </summary>
        public override List<string> PagesUrl
        {
            get
            {

                return _urls;
            }
            set
            {
                base.PagesUrl = value;
            }
        }

        public override bool IsWikiHomePage
        {
            get
            {
                return true;
            }
            set
            {
                base.IsWikiHomePage = value;
            }
        }

        private PublishingWeb _publishingWeb;
        private const string StyleLibraryName = "Style Library";
        private const string ProjectFolderName = "DiligencePortal";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingWeb"></param>
        protected override void BeforeRemoveFiles(PublishingWeb publishingWeb)
        {
            //Remove Folder which located in Style Library

            if (publishingWeb == null) return;

            _publishingWeb = publishingWeb;
            SPSecurity.RunWithElevatedPrivileges(RemoveProjectBrandingFolder);
        }

        /// <summary>
        /// Remove folder
        /// </summary>
        private void RemoveProjectBrandingFolder()
        {
            using (SPSite oSite = new SPSite(_publishingWeb.Web.Site.ID))
            {
                using (SPWeb rootWeb = oSite.RootWeb)
                {

                    //Delete Folder
                    #region Remove folder
                    SPList oList = rootWeb.Lists.TryGetList(StyleLibraryName);
                    if (oList != null)
                    {
                        //SPFolder rootFolder = oList.RootFolder;
                        var folders = GetListFoders(oList);
                        foreach (SPFolder folder in folders)
                        {
                            if (folder.Name == ProjectFolderName)
                            {
                                Console.WriteLine("Folder {0} is deleted on current web {1} ({2}). ", folder.Name, rootWeb.Url, rootWeb.Title);
                                folder.Delete();
                                break;
                            }
                        }
                    }
                    oList.Update();
                    #endregion
                }
            }
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
    }
}
