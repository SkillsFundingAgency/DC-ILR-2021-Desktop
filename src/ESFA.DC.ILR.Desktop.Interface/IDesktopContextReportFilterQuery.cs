using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IDesktopContextReportFilterQuery
    {
        string ReportName { get; }

        IEnumerable<IDesktopContextReportFilterPropertyQuery> FilterProperties { get; }
    }
}
