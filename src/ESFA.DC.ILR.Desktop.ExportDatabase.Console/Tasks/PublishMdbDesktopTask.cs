using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Tasks
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

            if (File.Exists(sourceMdb))
            {
                _logger.LogInfo($"Copying Access : {sourceMdb} to {destinationMdb}");

                Directory.CreateDirectory(outputDirectory);
                File.Copy(sourceMdb, destinationMdb, true);
            }
            else
            {
                _logger.LogInfo($"No File Found : {sourceMdb}");
            }

            // CSV Exports
            var exportDirectory = Path.Combine(context.Container, context.ExportDirectory);

            var csvFileNames = Directory.GetFiles(exportDirectory, "*.csv");

            var sourceArchive = Path.Combine(exportDirectory, "FISExportCSV.zip");

            ArchiveFiles(csvFileNames, sourceArchive);

            var destinationArchive = Path.Combine(outputDirectory, $"FIS-CSV {context.DateTime:yyyyMMdd-HHmmss}.zip");

            if (File.Exists(sourceArchive))
            {
                _logger.LogInfo($"Copying Exports : {sourceArchive} to {destinationArchive}");

                File.Copy(sourceArchive, destinationArchive, true);
            }
            else
            {
                _logger.LogInfo($"No File Found : {sourceArchive}");
            }

            return Task.FromResult(desktopContext);
        }

        public void ArchiveFiles(IEnumerable<string> fileNames, string archiveName)
        {
            using (ZipArchive archive = ZipFile.Open(archiveName, ZipArchiveMode.Create))
            {
                foreach (var filePath in fileNames)
                {
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }
        }
    }
}
