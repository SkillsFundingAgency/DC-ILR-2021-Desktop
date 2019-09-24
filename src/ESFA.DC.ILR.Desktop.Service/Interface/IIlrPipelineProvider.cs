using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IIlrPipelineProvider
    {
        IReadOnlyList<IIlrDesktopTaskDefinition> Provide();

        int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey);
    }
}
