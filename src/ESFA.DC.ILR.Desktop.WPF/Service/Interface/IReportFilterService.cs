using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Stub;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IReportFilterService
    {
        IEnumerable<IReportFilterDefinition> GetReportFilterDefinitions();

        void SaveReportFilterQueries(IEnumerable<IReportFilterQuery> reportFilterQueries);

        IEnumerable<IReportFilterQuery> GetReportFilterQueries();
    }
}
