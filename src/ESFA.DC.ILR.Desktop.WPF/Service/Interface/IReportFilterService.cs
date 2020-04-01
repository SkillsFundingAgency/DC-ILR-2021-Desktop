using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.ReportService.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service.Interface
{
    public interface IReportFilterService
    {
        IEnumerable<IReportFilterDefinition> GetReportFilterDefinitions();

        void SaveReportFilterQueries(IEnumerable<IDesktopContextReportFilterQuery> reportFilterQueries);

        IEnumerable<IDesktopContextReportFilterQuery> GetReportFilterQueries();
    }
}
