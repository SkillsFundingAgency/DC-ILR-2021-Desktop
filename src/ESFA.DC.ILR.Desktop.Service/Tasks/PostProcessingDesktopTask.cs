using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class PostProcessingDesktopTask : IDesktopTask
    {
        private readonly ILogger _logger;

        public PostProcessingDesktopTask(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var postProcessingDesktopTaskContext = new PostProcessingDesktopTaskContext(desktopContext);

            Directory.CreateDirectory(desktopContext.OutputDirectory);

            if (postProcessingDesktopTaskContext.ReportFileNames != null)
            {
                foreach (var fileName in postProcessingDesktopTaskContext.ReportFileNames.Where(r => !string.IsNullOrWhiteSpace(r)))
                {
                    var source = Path.Combine(postProcessingDesktopTaskContext.Container, fileName);
                    var destination = Path.Combine(postProcessingDesktopTaskContext.OutputDirectory, fileName);

                    File.Copy(source, destination, true);

                    _logger.LogInfo($"Copying {source} to {destination}");
                }
            }

            return desktopContext;
        }
    }
}
