using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Service.Stub
{
    public interface IReportFilterDefinition
    {
        string ReportName { get; }

        IEnumerable<IReportFilterPropertyDefinition> Properties { get; }
    }
}
