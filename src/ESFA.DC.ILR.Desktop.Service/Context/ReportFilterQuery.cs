using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class ReportFilterQuery : IReportFilterQuery
    {
        public string ReportName { get; set; }

        public IEnumerable<IReportFilterQueryProperty> FilterProperties { get; set; }
    }
}
