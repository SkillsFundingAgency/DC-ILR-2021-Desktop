using System;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Tasks.Interface
{
    public interface IPublishMdbDesktopTaskContext
    {
        string Container { get; }

        string ExportDirectory { get; }

        string MdbFileName { get; }

        string OriginalFilename { get; }

        DateTime DateTime { get; }
    }
}
