using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Service.Stub
{
    public class ReportFilterDefinition : IReportFilterDefinition
    {
        public string ReportName { get; set; }

        public IEnumerable<IReportFilterPropertyDefinition> Properties { get; set; }
    }
}
