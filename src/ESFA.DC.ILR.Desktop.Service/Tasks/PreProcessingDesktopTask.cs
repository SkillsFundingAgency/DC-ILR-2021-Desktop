using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks.Interface;
using ESFA.DC.Logging.Interfaces;
using Polly;
using Polly.Retry;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class PreProcessingDesktopTask : IDesktopTask
    {
        private const string ExportPath = "Export";
        private readonly ILogger _logger;
        private readonly RetryPolicy _fileSystemRetryPolicy;

        public PreProcessingDesktopTask(ILogger logger)
        {
            _logger = logger;

            _fileSystemRetryPolicy = Policy
                .Handle<IOException>()
                .Or<DirectoryNotFoundException>()
                .WaitAndRetry(
                    new TimeSpan[]
                    {
                        TimeSpan.FromMilliseconds(500),
                        TimeSpan.FromSeconds(1000),
                        TimeSpan.FromSeconds(2000),
                    },
                    (exception, span) =>
                        _logger.LogError($"Exception Caught, retry in {span.Milliseconds} Milliseconds", exception));
        }

        public Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var preProcessingDesktopTaskContext = new PreProcessingDesktopTaskContext(desktopContext);

            DeleteSandboxIfExists(preProcessingDesktopTaskContext.Container);

            var fileName = Path.GetFileName(preProcessingDesktopTaskContext.FileName);
            var refDataFileName = Path.GetFileName(preProcessingDesktopTaskContext.ReferenceDataFileName);

            var newFilePath = Path.Combine(preProcessingDesktopTaskContext.Container, fileName);
            var newRefDataFilePath = Path.Combine(preProcessingDesktopTaskContext.Container, refDataFileName);

            CreateAndPopulateSandbox(preProcessingDesktopTaskContext, newFilePath, newRefDataFilePath);

            preProcessingDesktopTaskContext.FileName = fileName;
            preProcessingDesktopTaskContext.OriginalFileName = fileName;
            preProcessingDesktopTaskContext.ReferenceDataFileName = refDataFileName;

            if (TryGetUkprnFromFileName(fileName, out var ukprn))
            {
                preProcessingDesktopTaskContext.Ukprn = ukprn;
            }

            var fileSizeInBytes = new FileInfo(newFilePath).Length;
            preProcessingDesktopTaskContext.FileSizeInBytes = fileSizeInBytes;

            return Task.FromResult(desktopContext);
        }

        public bool TryGetUkprnFromFileName(string fileName, out int ukprn)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var fileNameSplit = fileName.Split('-');

                if (fileNameSplit.Length > 1)
                {
                    var ukprnString = fileNameSplit[1];

                    if (ukprnString.Length == 8)
                    {
                        return int.TryParse(ukprnString, out ukprn);
                    }
                }
            }

            ukprn = 0;

            return false;
        }

        private void DeleteSandboxIfExists(string container)
        {
            _fileSystemRetryPolicy.Execute(() =>
            {
                if (Directory.Exists(container))
                {
                    _logger.LogInfo($"Deleting Sandbox Directory : {container}");
                    Directory.Delete(container, true);

                    // next step is to create directory, file system operations are asynchronous so allow 50ms to catch up.
                    Thread.Sleep(50);
                }
            });
        }

        private void CreateAndPopulateSandbox(IPreProcessingDesktopTaskContext preProcessingDesktopTaskContext, string newFilePath, string newRefDataFilePath)
        {
            _fileSystemRetryPolicy
                .Execute(() =>
                {
                    _logger.LogInfo($"Creating and Populating Sandbox Directory : {preProcessingDesktopTaskContext.Container}, {preProcessingDesktopTaskContext.FileName}, {preProcessingDesktopTaskContext.ReferenceDataFileName}");
                    Directory.CreateDirectory(preProcessingDesktopTaskContext.Container);
                    Directory.CreateDirectory(Path.Combine(preProcessingDesktopTaskContext.Container, ExportPath));

                    // let the file system catch up, if this isn't long enough fall back to retry policy
                    Thread.Sleep(50);
                    File.Copy(preProcessingDesktopTaskContext.FileName, newFilePath, true);
                    File.Copy(preProcessingDesktopTaskContext.ReferenceDataFileName, newRefDataFilePath, true);
                });
        }
    }
}
