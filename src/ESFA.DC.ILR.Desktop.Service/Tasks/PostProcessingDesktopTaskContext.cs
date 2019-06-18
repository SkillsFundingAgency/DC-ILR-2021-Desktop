using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class PostProcessingDesktopTaskContext : IPostProcessingDesktopTaskContext
    {
        private readonly IDesktopContext _desktopContext;

        public PostProcessingDesktopTaskContext(IDesktopContext desktopContext)
        {
            _desktopContext = desktopContext;
        }

        public string Container => _desktopContext.KeyValuePairs[ILRContextKeys.Container].ToString();

        public IReadOnlyCollection<string> ReportFileNames => _desktopContext.KeyValuePairs[ILRContextKeys.ReportOutputFileNames].ToString().Split('|');

        public string OutputDirectory => _desktopContext.OutputDirectory;
    }
}
