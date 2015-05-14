using MR.SP.DueDiligence.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework.Entity
{
    public class UserInfo
    {
        public string LoginId;
        public string Name;
        public string Email;

        public bool IsAdmin { get { return (Role & EnumRole.Admin) == EnumRole.Admin; } }
        public bool IsParticipant { get { return (Role & EnumRole.Participant) == EnumRole.Participant; } }
        public bool IsNormalUser { get { return !(IsAdmin || IsParticipant); } }
        public EnumRole Role { get; set; }
    }
}
