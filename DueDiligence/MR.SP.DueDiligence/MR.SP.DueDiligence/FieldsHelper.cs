using Microsoft.SharePoint;

namespace MR.SP.DueDiligence.Framework
{
    public static class FieldsHelper
    {
        /// <summary>
        /// Refresh Field Internal name fur current environment
        /// </summary>
        /// <param name="projectList">SourceList</param>
        /// <param name="displayName">Field Display Name</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns></returns>
        public static string GetFieldInternalNameByDisplayName(SPList list, string displayName, string defaultValue)
        {
            string result = defaultValue;
            if (null == list || !list.Fields.ContainsField(displayName))
            { }
            else
            {
                result = list.Fields.GetField(displayName).InternalName;
            }
            return result;
        }
    }
}
