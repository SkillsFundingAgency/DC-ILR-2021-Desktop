using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IIlrPipelineProvider
    {
        IReadOnlyList<IIlrDesktopTaskDefinition> Provide();

        int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey);
    }
}
