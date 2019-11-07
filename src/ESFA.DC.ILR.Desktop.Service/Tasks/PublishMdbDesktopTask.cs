using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class PublishMdbDesktopTask : IDesktopTask
    {
        private readonly ILogger _logger;

        public PublishMdbDesktopTask(ILogger logger)
        {
            _logger = logger;
        }

        public Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var context = new PublishMdbDesktopTaskContext(desktopContext);

            var outputDirectory = Path.Combine(context.OutputDirectory, Path.GetFileNameWithoutExtension(context.OriginalFilename));

            var sourceMdb = Path.Combine(context.Container, context.ExportDirectory, context.MdbFileName);

            var destinationMdb = Path.Combine(outputDirectory, $"FIS {context.DateTime:yyyyMMdd-HHmmss}.mdb");

            _logger.LogInfo($"Copying Access : {sourceMdb} to {destinationMdb}");

            File.Copy(sourceMdb, destinationMdb, true);

            return Task.FromResult(desktopContext);
        }
    }
}
