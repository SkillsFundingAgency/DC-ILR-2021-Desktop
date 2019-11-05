using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Service.Tasks.Interface
{
    public interface IPostProcessingDesktopTaskContext
    {
        string Container { get; }

        string OutputDirectory { get; }

        string OriginalFilename { get; }

        IReadOnlyCollection<string> ReportFileNames { get; }

        string ExportFolder { get; }

        string MdbFileName { get; }
    }
}
