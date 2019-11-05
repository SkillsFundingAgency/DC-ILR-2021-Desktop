using System.IO;
using System.Linq;
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

            var outputDirectory = Path.Combine(postProcessingDesktopTaskContext.OutputDirectory, Path.GetFileNameWithoutExtension(postProcessingDesktopTaskContext.OriginalFilename));

            Directory.CreateDirectory(outputDirectory);

            if (postProcessingDesktopTaskContext.ReportFileNames != null)
            {
                foreach (var fileName in postProcessingDesktopTaskContext.ReportFileNames.Where(r => !string.IsNullOrWhiteSpace(r)))
                {
                    var source = Path.Combine(postProcessingDesktopTaskContext.Container, fileName);
                    var destination = Path.Combine(outputDirectory, fileName);

                    File.Copy(source, destination, true);

                    _logger.LogInfo($"Copying {source} to {destination}");
                }
            }

            File.Copy(Path.Combine(postProcessingDesktopTaskContext.Container, postProcessingDesktopTaskContext.ExportFolder, postProcessingDesktopTaskContext.MdbFileName), Path.Combine(outputDirectory, postProcessingDesktopTaskContext.MdbFileName), true);

            return desktopContext;
        }
    }
}
