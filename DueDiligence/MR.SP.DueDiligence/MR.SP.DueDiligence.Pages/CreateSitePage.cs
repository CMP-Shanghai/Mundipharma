using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;
using MR.SP.DueDiligence.WebPart.WPIndicationNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MR.SP.DueDiligence.Pages
{
    public class CreateSitePage
    {
        private const string WebPartToken = "[WebPartToken]";
        private const string WebPartToken1 = "[WebPartToken1]";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curWeb"></param>
        /// <param name="settingsDic"></param>
        /// <param name="IsDefaultDocumentLibrary"></param>
        /// <returns></returns>
        public static bool FunctionalAreaPageProvision(SPWeb curWeb, Dictionary<string, string> settingsDic,string pageContent,bool IsDefaultDocumentLibrary=true)
        {
            //throw new NotImplementedException();
            bool result = false;
            string keyName = "SitePageName";
            if (null == curWeb || null == settingsDic || settingsDic.Count <= 0) return false;

            if (settingsDic.ContainsKey(keyName))
            {
                string[] functionalAreas = settingsDic[keyName].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                SPList pageList = curWeb.Lists.TryGetList("Site Pages");

                foreach (string functionalAreaName in functionalAreas)
                {
                    //Craete Document library
                    SPList functionalList = EnsureList(curWeb, functionalAreaName,IsDefaultDocumentLibrary);
                    //Create Wikipage and add document library list view
                    EnsurePage(curWeb, functionalAreaName, functionalList, pageList,pageContent, IsDefaultDocumentLibrary);
                }
            }


            return result;
        }


        private static SPList EnsureList(SPWeb curWeb, string functionalAreaName, bool IsDefaultDocumentLibrary)
        {
            if (null == curWeb || string.IsNullOrEmpty(functionalAreaName)) return null;

            SPList resultList = null;
            string DocumentTypeConfig = "Indication Document Type";
            string fieldDocumentType = "IndicationDocumentType";
            try
            {
                resultList = curWeb.Lists.TryGetList(functionalAreaName);
                if (resultList == null)
                {
                    //create list
                    Guid listId = curWeb.Lists.Add(functionalAreaName, functionalAreaName, SPListTemplateType.DocumentLibrary);
                    curWeb.Update();
                    resultList = curWeb.Lists[listId];
                    if (!IsDefaultDocumentLibrary)
                    {
                        
                        //resultList.Fields.Add("DocumentType",SPFieldType.Lookup,true,)
                        
                        SPList listDocumentTypeLookup = curWeb.Lists.TryGetList(DocumentTypeConfig);
                        Guid listDocumentTypeLookupGuid;
                        if (listDocumentTypeLookup == null)
                        {
                            listDocumentTypeLookupGuid = curWeb.Lists.Add(DocumentTypeConfig, DocumentTypeConfig, SPListTemplateType.CustomGrid);
                            listDocumentTypeLookup=curWeb.Lists[listDocumentTypeLookupGuid];
                        }
                        else
                        {
                            listDocumentTypeLookupGuid = listDocumentTypeLookup.ID;
                        }
                        
                        resultList.Fields.AddLookup(fieldDocumentType, listDocumentTypeLookupGuid, true);
                        SPFieldLookup sfl = resultList.Fields[fieldDocumentType] as SPFieldLookup;
                        sfl.LookupField = listDocumentTypeLookup.Fields["Title"].InternalName;
                        sfl.Update();
                        resultList.Update();

                        SPView viewDefault = resultList.DefaultView;
                        //viewDefault.ViewFields.Add(fieldDocumentType);
                        viewDefault.Query = "<GroupBy Collapse='TRUE'><FieldRef Name='" + fieldDocumentType + "'/></GroupBy>";
                        viewDefault.Update();
                        

                    }
                }
                

            }
            catch (Exception ex)
            { }

            return resultList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="web"></param>
        /// <param name="functionalAreaName"></param>
        /// <param name="functionalList">Functional Area Document Library</param>
        /// <param name="pageList">Site Page List</param>
        private static void EnsurePage(SPWeb web, string functionalAreaName, SPList functionalList, SPList pageList, string pageContent,bool IsDefaultDocumentLibrary = true)
        {
            if (null == web || string.IsNullOrEmpty(functionalAreaName)) return;

            #region Page Provision
            //get the page name from the xml attribute:
            string pageName=functionalAreaName.Replace(" ", "");
            if (!IsDefaultDocumentLibrary)
            {
                pageName = "Indication Insights-" + pageName;
            }
            else
            {
                pageName = "Functional Areas-" + pageName;
            }
            string pageFileName = string.Format("{0}.aspx", pageName);

            //build the file URL:
            string fileURL = string.Format("{0}/{1}", pageList.RootFolder.ServerRelativeUrl, pageFileName);

            bool fileExisted = true;
            SPFile file = web.GetFile(fileURL);

            if (!file.Exists)
            {
                //File does not exist, we need to create it:
                file = SPUtility.CreateNewWikiPage(pageList, fileURL);
                fileExisted = false;
            }

            //Set title
            string content = string.Format(pageContent, functionalAreaName);

            ////grab reference to the web part manager for the page:
            using (SPLimitedWebPartManager splwm = file.GetLimitedWebPartManager(System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
            {
                if (fileExisted)
                {
                    while (splwm.WebParts.Count > 0)
                    {
                        splwm.DeleteWebPart(splwm.WebParts[0]);
                    }
                }

                //dynamically add the web part and modify the contents
                XsltListViewWebPart lv = new XsltListViewWebPart();
                lv.ListId = functionalList.ID;
                lv.ViewGuid = functionalList.DefaultView.ID.ToString();
                lv.ListName = functionalList.ID.ToString("B").ToUpper();
                lv.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                lv.Style.Add("min-width", "450px");
                content = InsertWebPart(lv, content, splwm);

                if (!IsDefaultDocumentLibrary)
                {
                    WPIndicationNotes wpIndication = new WPIndicationNotes();
                    wpIndication.ListName = functionalAreaName;
                    wpIndication.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                    content = InsertWebPart(wpIndication, content, splwm,WebPartToken1);
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

        private static string InsertWebPart(System.Web.UI.WebControls.WebParts.WebPart webpart, string wikiContent, SPLimitedWebPartManager webPartManager)
        {
            //ensure that there's a place holder in the content for it.
            string defaultToken = WebPartToken;
            
            return InsertWebPart(webpart,wikiContent,webPartManager, defaultToken);
        }
        private static string InsertWebPart(System.Web.UI.WebControls.WebParts.WebPart webpart, string wikiContent, SPLimitedWebPartManager webPartManager,string webPartToken)
        {
            //ensure that there's a place holder in the content for it.
            if (wikiContent.Contains(webPartToken))
            {
                //create a new web part IDs:
                Guid wpID = Guid.NewGuid();
                string containingDivID = wpID.ToString();
                string webPartObjID = StorageKeyToID(wpID);

                //set web part object with appropriate ID
                webpart.ID = webPartObjID;

                //add the web part
                webPartManager.AddWebPart(webpart, "wpz", 0);

                //build the wiki content place holder
                string webPartPlaceHolder = GetWikiWebPartContainer(containingDivID);

                //replace the provisioning place holder with the one required by the wiki page:
                wikiContent = wikiContent.Replace(webPartToken, webPartPlaceHolder);
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
