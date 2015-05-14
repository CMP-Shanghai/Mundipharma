using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using System.IO;

namespace MR.SP.IdeaTracker.Branding
{
    public static class ITBrandingFeatureHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingWeb"></param>
        /// <param name="relativeFilePath"></param>
        public static void RemoveFiles(PublishingWeb publishingWeb, string relativeFilePath)
        {
            try
            {
                publishingWeb.Web.AllowUnsafeUpdates = true;

                SPFile file = publishingWeb.Web.GetFile(relativeFilePath);

                if (file != null)
                {
                    if (file.CheckOutType != SPFile.SPCheckOutType.None)
                    {
                        file.UndoCheckOut();
                    }
                    file.Delete();
                }
                publishingWeb.Web.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                //MRITLogger.LogToOperations(ex, string.Format("try to remove files from page library {0}", relativeFilePath), 100, EventSeverity.Error);
                throw;
            }

        }


        /// <summary>
        /// Check whether files in current module should be update by code
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        private static bool IsNeedUpdate(string moduleName, IList<string> needUpdateModuleName)
        {
            return needUpdateModuleName.Any(name => string.Compare(moduleName, name, true) == 0);
        }

        /// <summary>
        /// CheckOut status for current file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool CheckOutStatus(SPFile file)
        {
            if (file.CheckOutType != SPFile.SPCheckOutType.None)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whether Content Approval is enable on current library
        /// </summary>
        /// <param name="listitem"></param>
        /// <returns></returns>
        private static bool CheckContentApproval(SPListItem listitem)
        {
            bool isContentApprovalEnabled = listitem.ParentList.EnableModeration;

            return isContentApprovalEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XElement ToXElement(this XmlNode node)
        {
            var xDoc = new XDocument();

            using (XmlWriter xmlWriter = xDoc.CreateWriter())

                node.WriteTo(xmlWriter);

            return xDoc.Root;

        }
    }
}
