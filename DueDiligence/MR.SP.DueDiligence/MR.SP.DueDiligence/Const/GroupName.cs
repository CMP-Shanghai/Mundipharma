using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework.Const
{
    public static class GroupName
    {
        //List all constant for list name
        public const string AdminGroup = "Due Diligence Administrators";
        public const string ProjectUsers = "ProjectUsers";
        public const string RD = "R&D Executive Director";
        public const string BDLead = "BD Lead";
        public const string CommercialHead = "Commercial Head";
        public const string Participants = "Participants";

        /// <summary>
        /// If user is participant, he should be one of below user group
        /// </summary>
        public static List<string> GroupsHasRightsOnProject = new List<string>(){BDLead,CommercialHead,RD,Participants,ProjectUsers};
    }
}
