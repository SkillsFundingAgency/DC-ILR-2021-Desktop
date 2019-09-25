using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContextReportFilterQuery : IDesktopContextReportFilterQuery
    {
        public string ReportName { get; set; }

        public IEnumerable<IDesktopContextReportFilterPropertyQuery> FilterProperties { get; set; }
    }
}
