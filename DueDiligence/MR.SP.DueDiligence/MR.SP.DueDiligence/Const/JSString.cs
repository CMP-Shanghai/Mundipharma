using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework.Const
{
    public static class JSString
    {
        public const string BtnUpLoad = "<button onclick='var url=\"_webUrl_/_layouts/15/Upload.aspx?ddupload=1&List={_listID_}&RootFolder=_webUrl_%2F_listName_%2F_ID_\"; NewItem2(event, url); return false;' type=\"button\" unselectable=\"on\">_DisplayName_</button>";
    }
}
