using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Interface
{
    public interface IIlrPipelineProvider
    {
        IReadOnlyList<IIlrDesktopTaskDefinition> Provide();

        int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey, IReadOnlyList<IIlrDesktopTaskDefinition> pipeline);
    }
}
