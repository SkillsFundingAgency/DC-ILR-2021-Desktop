using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Desktop.Service.Tasks.Interface
{
    public interface IPostProcessingDesktopTaskContext
    {
        string Container { get; }

        string OutputDirectory { get; }

        IReadOnlyCollection<string> ReportFileNames { get; }
    }
}
