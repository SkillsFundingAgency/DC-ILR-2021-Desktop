using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Desktop.Service.Stub;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IReportFilterService
    {
        IEnumerable<IReportFilterDefinition> GetReportFilterDefinitions();
    }
}
