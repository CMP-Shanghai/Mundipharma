using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework
{
    [Flags]
    public enum EnumRole
    {
        [Description("None")]
        None = 0,
        [Description("Normal User")]
        Normal = 1,
        [Description("Participant")]
        Participant = 2,
        [Description("Administrator")]
        Admin = 4
    }
}
