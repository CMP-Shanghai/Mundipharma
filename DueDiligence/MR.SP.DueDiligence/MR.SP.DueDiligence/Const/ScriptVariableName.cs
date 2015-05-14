using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework.Const
{
    public static class ScriptVariableName
    {
        private const string globalVariablePrefix = "dd_global_variable_";
        public static string GlobalVariableSiteId = FormatString("siteid");
        public static string GlobalVariableWebId = FormatString("webid");
        public static string GlobalVariableWebUrl = FormatString("web_url");
        public static string GlobalVariableFullUrl = FormatString("full_url");
        public static string GlobalVariableUserAccount = FormatString("user_account");


        private static string FormatString(string variableName)
        {
            return string.Format("{0}{1}",globalVariablePrefix,variableName);
        }
    }
}
