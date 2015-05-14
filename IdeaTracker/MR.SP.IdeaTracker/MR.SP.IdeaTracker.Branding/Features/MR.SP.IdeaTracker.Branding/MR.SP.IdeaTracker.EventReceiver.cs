using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Collections.Generic;
using Microsoft.SharePoint.Publishing;
using System.Linq;

namespace MR.SP.IdeaTracker.Branding
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    [Guid("2ba11fd1-8595-483c-a29e-7619f03c74d6")]
    public class MRSPIdeaTrackerEventReceiver : ITBrandingFeatureReceiverBase
    {
        private readonly List<string> _urls = new List<string>() {
            //"{0}/IdeaPortal.aspx"
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


        private PublishingWeb _publishingWeb;
        private const string StyleLibraryName = "Style Library";
        private const string TargetFolderName = "IdeaTracker";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingWeb"></param>
        protected override void BeforeRemoveFiles(PublishingWeb publishingWeb)
        {
            //Remove IdeaTracker Folder which located in Style Library

            if (publishingWeb == null) return;

            _publishingWeb = publishingWeb;
            SPSecurity.RunWithElevatedPrivileges(RemoveFolderForIdeaTracker);
        }

        /// <summary>
        /// Remove IdeaTracker folder
        /// </summary>
        private void RemoveFolderForIdeaTracker()
        {
            using (SPSite oSite = new SPSite(_publishingWeb.Web.Site.ID))
            {
                using (SPWeb rootWeb = oSite.RootWeb)
                {

                    //Delete Traget Folder
                    DeleteIdeaTrackerFolder(rootWeb);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootWeb"></param>
        private void DeleteIdeaTrackerFolder(SPWeb rootWeb)
        {
            #region Remove folder
            SPList oList = rootWeb.Lists.TryGetList(StyleLibraryName);
            if (oList != null)
            {
                //SPFolder rootFolder = oList.RootFolder;
                var folders = GetListFoders(oList);
                foreach (SPFolder folder in folders)
                {
                    if (folder.Name == TargetFolderName)
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
