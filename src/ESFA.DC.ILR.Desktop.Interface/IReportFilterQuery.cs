using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IReportFilterQuery
    {
        string ReportName { get; }

        IEnumerable<IReportFilterQueryProperty> FilterProperties { get; }
    }
}
