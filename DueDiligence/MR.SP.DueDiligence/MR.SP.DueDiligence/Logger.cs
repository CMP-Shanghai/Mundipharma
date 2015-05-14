using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.SP.DueDiligence.Framework
{
    public class Logger : SPDiagnosticsServiceBase
    {
        public static string DDDiagnosticAreaName = "DueDiligence";
        private static Logger _Current;
        public static Logger Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new Logger();
                }

                return _Current;
            }
        }

        private Logger()
            : base("DueDiligence Logging Service", SPFarm.Local)
        {

        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
            {
                new SPDiagnosticsArea(DDDiagnosticAreaName, new List<SPDiagnosticsCategory>
                {
                    new SPDiagnosticsCategory("WebParts", TraceSeverity.Unexpected, EventSeverity.Error)
                })
            };

            return areas;
        }

        public static void LogError(Category categoryName, string errorMessage)
        {
            SPDiagnosticsCategory category = Logger.Current.Areas[DDDiagnosticAreaName].Categories[categoryName.ToString()];
            Logger.Current.WriteTrace(0, category, TraceSeverity.Unexpected, errorMessage);
        }

        public enum Category
        {
            Unexpected,
            High,
            Medium,
            Information
        }
    }
}
