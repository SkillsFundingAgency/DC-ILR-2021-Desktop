using System;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Tasks.Interface;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Tasks
{
    public class PublishMdbDesktopTaskContext : IPublishMdbDesktopTaskContext
    {
        private readonly IDesktopContext _desktopContext;

        public PublishMdbDesktopTaskContext(IDesktopContext desktopContext)
        {
            _desktopContext = desktopContext;
        }

        public string OriginalFilename => _desktopContext.KeyValuePairs[ILRContextKeys.OriginalFilename].ToString();

        public string Container => _desktopContext.KeyValuePairs[ILRContextKeys.Container].ToString();

        public string OutputDirectory => _desktopContext.OutputDirectory;

        public string ExportDirectory => "Export";

        public string MdbFileName => "ESFA.DC.ILR.DataStore.Database.mdb";

        public DateTime DateTime => _desktopContext.DateTimeUtc;
    }
}
