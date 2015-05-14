using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework.Const
{
    public static class Fields
    {
        //Fields in Project List
        public const string ProjectStatusInProject = "Status";
        public const string Form = "Form";
        public const string NextSteps="Next Step";
        public const string NextDeadline="Next Deadline";
        public const string Action = "Action";
        public const string Outcome = "Outcome";
        public const string TherapeuticArea = "Therapeutic Area";
        public const string Opportunity = "Opportunity";

        public const string BDLead = "BD Lead";
        public const string CommercialHead = "Commercial Head";
        public const string RDExecutiveDirector="R&D Executive Director";
        public const string Participants = "Participants";

        public const string ExternalParticipants = "External Participants";

        public const string UserDepartment = "UserDepartment";

        public const string NoOfParticipants = "No of Participants";

        //Check list
        public static Hashtable GetChecklist()
        {
            Hashtable ht = new Hashtable();
            ht.Add("KO Meeting Review", "KO Meeting");
            ht.Add("BC1 complete Review", "BC1 complete");
            ht.Add("CFPA complete Review", "CFPA complete");
            ht.Add("TPP complete Review", "TPP complete");
            ht.Add("SWOT complete Review", "SWOT complete");
            ht.Add("Document requests complete Review", "Document requests complete");
            ht.Add("Questions complete Review", "Questions complete");
            ht.Add("F2F Agenda complete Review", "F2F Agenda complete");
            ht.Add("Meeting minutes complete Review", "Meeting minutes complete");
            ht.Add("DD Report Draft 1 complete Review", "DD Report Draft 1 complete");
            ht.Add("DD Report Draft 2 complete Review", "DD Report Draft 2 complete");
            ht.Add("DD Report Final Draft complete Review", "DD Report Final Draft complete");
            ht.Add("DD Report FINAL Review", "DD Report FINAL");
            ht.Add("S&TC presentation draft 1 complete Review", "S&TC presentation draft 1 complete");
            ht.Add("S&TC Presentation FINAL Review", "S&TC Presentation FINAL");
            ht.Add("BC2 complete Review", "BC2 complete");
            return ht;
        }


        //Document library
        public const string DocumentTypeInDocument = "DocumentType";
        public const string ReviewDateInDocument = "Submission Date";
        public const string SortNumInDocument = "SortNum";
        public const string ProjectStatusInDocument = "ProjectStatus";
        public const string FullOrShort = "FullOrShort";
        

        //fields in ProjectViewcConfiguration
        public const string ViewTitle = "Title";
        public const string DisplayFields = "DisplayFields";
        public const string RowLimit = "RowLimit";
        public const string ViewQuery = "ViewQuery";

    }
}
