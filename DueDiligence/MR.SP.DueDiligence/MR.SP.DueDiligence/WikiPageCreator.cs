using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MR.SP.DueDiligence.Framework
{
    public class WikiPageCreator
    {
        public const string WebPartToken = "[WebPartToken]";
        public const string DefaultContent = "<table><tr><td width=\"66%\" valign=\"top\"><div style=\"font-size:2em;\">{0}</div><div style=\"font-style:italic; padding-left:8px; padding-top:4px\"></div><br/>"
            + WebPartToken
            + "</td><td></td></tr></table>";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="web"></param>
        /// <param name="titleString"></param>
        /// <param name="ContentString"></param>
        /// <param name="sourceList"></param>
        /// <param name="sitePageList"></param>
        public static void EnsurePage(SPWeb web, string titleString, string ContentString, List<System.Web.UI.WebControls.WebParts.WebPart> webPartCollection, SPList sourceList, SPList sitePageList)
        {
            if (null == web || string.IsNullOrEmpty(titleString)) return;

            #region Page Provision
            //get the page name from the xml attribute:
            //string pageFileName = string.Format("{0}.aspx", titleString.Replace(" ", ""));
            string pageFileName = string.Format("{0}.aspx",titleString);

            //build the file URL:
            string fileURL = string.Format("{0}/{1}", sitePageList.RootFolder.ServerRelativeUrl, pageFileName);

            bool fileExisted = true;
            SPFile file = web.GetFile(fileURL);

            if (!file.Exists)
            {
                //File does not exist, we need to create it:
                file = SPUtility.CreateNewWikiPage(sitePageList, fileURL);
                fileExisted = false;
            }

            //string content = string.Format(DefaultContent, titleString);
            string content = string.IsNullOrEmpty(ContentString) ? string.Format(DefaultContent, titleString) : ContentString;

            #region Update Web Part
            using (SPLimitedWebPartManager splwm = file.GetLimitedWebPartManager(System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
            {
                if (fileExisted)
                {
                    while (splwm.WebParts.Count > 0)
                    {
                        splwm.DeleteWebPart(splwm.WebParts[0]);
                    }
                }

                content = webPartCollection.Aggregate(content, (current, wp) => InsertWebPart(wp, current, splwm, wp.ZoneIndex));
            }

            #endregion

            SPListItem item = file.GetListItem(new string[] { "WikiField" });
            //update the 'WikiField' field:
            item["WikiField"] = content;

            //commit the changes:
            item.Update();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webPartNode"></param>
        /// <param name="wikiContent"></param>
        /// <param name="WebPartManager"></param>
        /// <returns></returns>
        private string InsertWebPart(XmlNode webPartNode, string wikiContent, SPLimitedWebPartManager WebPartManager)
        {
            //get the provisioning file web part ID:
            string webPartID = webPartNode.Attributes["ID"].Value;

            //ensure that there's a place holder in the content for it.
            if (wikiContent.Contains(string.Format("{{<{0}>}}", webPartID)))
            {
                //get the web part xml:
                string webPartContent = ((XmlCDataSection)webPartNode.FirstChild).Value.Replace("\n", string.Empty).Replace("\r", string.Empty).Trim();

                //create the web part using the web part manager
                XmlReader xreader = XmlReader.Create(new System.IO.StringReader(webPartContent.Trim()));
                string WebPartImportErrorMsg = string.Empty;
                System.Web.UI.WebControls.WebParts.WebPart webpart = WebPartManager.ImportWebPart(xreader, out WebPartImportErrorMsg);

                //create a new web part IDs:
                Guid wpID = Guid.NewGuid();
                string containingDivID = wpID.ToString();
                string webPartObjID = StorageKeyToID(wpID);

                //set web part object with appropriate ID
                webpart.ID = webPartObjID;

                //add the web part
                WebPartManager.AddWebPart(webpart, "wpz", 0);

                //build the wiki content place holder
                string webPartPlaceHolder = GetWikiWebPartContainer(containingDivID);

                //replace the provisioning place holder with the one required by the wiki page:
                wikiContent = wikiContent.Replace(string.Format("{{<{0}>}}", webPartID), webPartPlaceHolder);
            }

            return wikiContent;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webpart"></param>
        /// <param name="wikiContent"></param>
        /// <param name="WebPartManager"></param>
        /// <param name="zoneIndex"></param>
        /// <returns></returns>
        private static string InsertWebPart(System.Web.UI.WebControls.WebParts.WebPart webpart, string wikiContent, SPLimitedWebPartManager WebPartManager, int zoneIndex)
        {
            //ensure that there's a place holder in the content for it.
            if (wikiContent.Contains(WebPartToken))
            {
                //create a new web part IDs:
                Guid wpID = Guid.NewGuid();
                string containingDivID = wpID.ToString();
                string webPartObjID = StorageKeyToID(wpID);

                //set web part object with appropriate ID
                webpart.ID = webPartObjID;

                //add the web part
                WebPartManager.AddWebPart(webpart, "wpz", zoneIndex);

                //build the wiki content place holder
                string webPartPlaceHolder = GetWikiWebPartContainer(containingDivID);

                //replace the provisioning place holder with the one required by the wiki page:
                wikiContent = wikiContent.Replace(WebPartToken, webPartPlaceHolder);
            }
            return wikiContent;
        }
        private static string GetWikiWebPartContainer(string containingDivID)
        {
            StringBuilder sb = new StringBuilder("<div class=\"ms-rtestate-read ms-rte-wpbox\"><div class=\"ms-rtestate-read ");
            sb.Append(containingDivID);
            sb.Append("\" id=\"div_");
            sb.Append(containingDivID);
            sb.Append("\"></div>\n");
            sb.Append("  <div class=\"ms-rtestate-read\" id=\"vid_");
            sb.Append(containingDivID);
            sb.Append("\" style=\"display:none\"></div>\n");
            sb.Append("</div>\n");
            string webPartPlaceHolder = sb.ToString();
            return webPartPlaceHolder;
        }

        private static string StorageKeyToID(Guid storageKey)
        {
            if (!(Guid.Empty == storageKey))
            {
                return ("g_" + storageKey.ToString().Replace('-', '_'));
            }
            return string.Empty;
        }
    }
}
