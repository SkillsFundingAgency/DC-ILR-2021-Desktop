using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IReportFilterQuery
    {
        string ReportName { get; }

        IDictionary<string, object> FilterProperties { get; }
    }
}
