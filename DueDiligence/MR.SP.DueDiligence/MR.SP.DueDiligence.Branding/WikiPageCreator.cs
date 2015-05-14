using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace MR.SP.DueDiligence.Branding
{
    public class WikiPageCreator
    {
        private XmlNode configNode;
        public WikiPageCreator(XmlNode ConfigNode)
        {
            this.configNode = ConfigNode;
        }

        public void Deploy(SPWeb web)
        {
            SPList list = this.GetListFromWebRelativeURL(web, this.configNode.Attributes["URL"].Value);
            foreach (XmlNode pageNode in this.configNode.SelectNodes("Page"))
            {
                #region Page Provision
                //get the page name from the xml attribute:
                string pageFileName = pageNode.Attributes["Name"].Value;

                //build the file URL:
                string fileURL = string.Format("{0}/{1}", list.RootFolder.ServerRelativeUrl, pageFileName);

                bool fileExisted = true;
                SPFile file = web.GetFile(fileURL);

                if (!file.Exists)
                {
                    //File does not exist, we need to create it:
                    file = SPUtility.CreateNewWikiPage(list, fileURL);
                    fileExisted = false;
                }

                //iterate through each web part node:
                Dictionary<string, string> propertyDic = new Dictionary<string, string>();
                foreach (XmlNode propertyNode in pageNode.SelectNodes("Properties/Property"))
                {
                    //dynamically add the web part and modify the contents
                    string propertyKey = propertyNode.Attributes["Key"].Value;
                    string propertyValue = propertyNode.Attributes["Value"].Value;
                    propertyDic.Add(propertyKey, propertyValue);
                }
                //Parse properties dictionary
                ParsePropertiesDictionary(web, propertyDic);
                //get the content as defined in the xml provisioning file
                string content = ((XmlCDataSection)pageNode.SelectSingleNode("Content").FirstChild).Value.Trim();
                
                foreach (KeyValuePair<string, string> pair in propertyDic)
                {
                    string tokenName = pair.Key;
                    string tokenValue = pair.Value;
                    if (!string.IsNullOrEmpty(tokenValue))
                    {
                        content = content.Replace(tokenName,tokenValue);
                    }
                }

                //grab reference to the web part manager for the page:
                using (SPLimitedWebPartManager splwm = file.GetLimitedWebPartManager(System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
                {
                    if (fileExisted)
                    {
                        while (splwm.WebParts.Count > 0)
                        {
                            splwm.DeleteWebPart(splwm.WebParts[0]);
                        }
                    }
                    //iterate through each web part node:
                    foreach (XmlNode webPartNode in pageNode.SelectNodes("WebParts/WebPart"))
                    {
                        //dynamically add the web part and modify the contents
                        content = InsertWebPart(webPartNode, content, splwm);
                    }

                }

                //grab reference to the item:
                SPListItem item = file.GetListItem(new string[] { "WikiField" });
                //update the 'WikiField' field:
                item["WikiField"] = content;

                //commit the changes:
                item.Update();
                #endregion
            }
        }

        private void ParsePropertiesDictionary(SPWeb web, Dictionary<string, string> dict)
        {
            if (web != null && dict != null && dict.Count > 0)
            {
                //{(ListId)}
                string webId = FormatToken("WebId");
                if (dict.ContainsKey(webId))
                {
                    dict[webId] = web.ID.ToString("B");
                }
                string siteId = FormatToken("SiteId");
                if (dict.ContainsKey(siteId))
                {
                    dict[webId] = web.Site.ID.ToString("B");
                }
                string listName = FormatToken("ListName");
                if (dict.ContainsKey(listName) && !string.IsNullOrEmpty(dict[listName]))
                {
                    SPList list = web.Lists.TryGetList(dict[listName]);
                    if (list != null)
                    {
                        string listId = FormatToken("ListId");
                        if (dict.ContainsKey(listId))
                        {
                            dict[listId] = list.ID.ToString("B");
                        }
                        string viewName = FormatToken("ViewName");
                        if (dict.ContainsKey(viewName) && !string.IsNullOrEmpty(dict[viewName]))
                        {
                            string viewId = FormatToken("ViewId");
                            if (dict.ContainsKey(viewId))
                            {
                                Guid vId = GetViewIdByName(list, dict[viewName]);
                                dict[viewId] = vId.ToString("B");
                            }
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }

        private string FormatToken(string token)
        {
            //throw new NotImplementedException();
            return string.Format("{({0})}", token);
        }

        private Guid GetViewIdByName(SPList list, string viewName)
        {
            Guid viewId = Guid.Empty;
            foreach (SPView view in list.Views)
            {
                if (string.Compare(view.Title, viewName, true) == 0)
                {
                    viewId = view.ID;
                    break;
                }
            }
            return viewId;
        }

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

        private string InsertWebPart(System.Web.UI.WebControls.WebParts.WebPart webpart, string wikiContent, SPLimitedWebPartManager WebPartManager)
        {
            //get the provisioning file web part ID:
            string webPartID = "8"; //mu custom ID

            //ensure that there's a place holder in the content for it.
            if (wikiContent.Contains(string.Format("{{<{0}>}}", webPartID)))
            {
                //create a new web part IDs:
                Guid wpID = Guid.NewGuid();
                string containingDivID = wpID.ToString();
                string webPartObjID = this.StorageKeyToID(wpID);

                //set web part object with appropriate ID
                webpart.ID = webPartObjID;

                //add the web part
                WebPartManager.AddWebPart(webpart, "wpz", 0);

                //build the wiki content place holder
                string webPartPlaceHolder = this.GetWikiWebPartContainer(containingDivID);

                //replace the provisioning place holder with the one required by the wiki page:
                wikiContent = wikiContent.Replace(string.Format("{{<{0}>}}", webPartID), webPartPlaceHolder);
            }
            return wikiContent;
        }
        private string GetWikiWebPartContainer(string containingDivID)
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

        private string StorageKeyToID(Guid storageKey)
        {
            if (!(Guid.Empty == storageKey))
            {
                return ("g_" + storageKey.ToString().Replace('-', '_'));
            }
            return string.Empty;
        }

        private SPList GetListFromWebRelativeURL(SPWeb web, string WebRelativeListURL)
        {
            string webUrl = string.Compare(web.ServerRelativeUrl, "/") == 0 ? string.Empty : web.ServerRelativeUrl;//to avoid //SitePages format
            string listUrl = string.Format("{0}/{1}", webUrl, WebRelativeListURL);
            return web.GetList(listUrl);
        }
    }
}
