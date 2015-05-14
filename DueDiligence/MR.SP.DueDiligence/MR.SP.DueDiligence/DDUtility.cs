using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework
{
    public static class DDUtility
    {
        public static int BatchItemNumber = 50;

        #region Get Property Value
        public static List<string> GetPropertyValues(List<string> keys, List<string> defalutValues)
        {
            List<string> result = defalutValues;
            if (keys.Count != defalutValues.Count) return result;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            for (int index = 0; index < keys.Count; index++)
                            {
                                string propertyKey = keys[index];
                                string defaultValue = defalutValues[index];
                                string propertValue = GetPropertyValue(web, propertyKey, defaultValue);
                                result[index] = propertValue;
                            }
                        }

                    }
                });

            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
                //SPUtility.LogCustomAppError(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// Get Property Value from web.AllProperties
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetPropertyValue(string propertyKey, string defaultValue)
        {
            string result = defaultValue;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            result = GetPropertyValue(web, propertyKey, defaultValue);
                        }

                    }
                });

            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
                //SPUtility.LogCustomAppError(ex.ToString());
            }
            finally
            {

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="web"></param>
        /// <param name="propertyKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static string GetPropertyValue(SPWeb web, string propertyKey, string defaultValue)
        {
            string result = defaultValue;
            if (string.IsNullOrEmpty(propertyKey) || null == web) return result;

            if (web.AllProperties.ContainsKey(propertyKey))
            {
                result = Convert.ToString(web.AllProperties[propertyKey]);
            }
            return result;
        }
        #endregion

        #region List Item Batch operation
        /// <summary>
        /// Batch delete items
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool BatchDelete(SPList sourceList, SPListItemCollection items)
        {
            bool result = false;
            if (sourceList == null || items == null) return result;

            try
            {
                //get the string for batch delete
                var batchString = new StringBuilder();
                batchString.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");
                string sourceListId = sourceList.ID.ToString();
                //keep the number of the items in each catagory and delete others
                int counter = 0;
                string resultMessage = string.Empty;
                for (int index = 0; index < items.Count; index++)
                {
                    if (counter >= BatchItemNumber)
                    {
                        //need execute this batch to aovid execeed the batch item number
                        batchString.Append("</Batch>");
                        resultMessage += sourceList.ParentWeb.ProcessBatchData(batchString.ToString());

                        //Initial next batch header
                        counter = 0;
                        batchString.Clear();
                        batchString.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");
                    }

                    SPListItem item = items[index];
                    batchString.Append("<Method>");
                    batchString.Append("<SetList Scope=\"Request\">" + sourceListId + "</SetList>");
                    batchString.Append("<SetVar Name=\"ID\">" + Convert.ToString(item.ID) + "</SetVar>");
                    batchString.Append("<SetVar Name=\"Cmd\">Delete</SetVar>");
                    batchString.Append("</Method>");
                    counter++;
                }
                batchString.Append("</Batch>");
                resultMessage += sourceList.ParentWeb.ProcessBatchData(batchString.ToString());
                result = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(Logger.Category.Unexpected, ex.ToString());
                throw;
            }

            return result;
        }

        #endregion
    }
}
