using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class PreProcessingDesktopTask : IDesktopTask
    {
        public Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var preProcessingDesktopTaskContext = new PreProcessingDesktopTaskContext(desktopContext);

            var directoryInfo = new DirectoryInfo(preProcessingDesktopTaskContext.Container);

            directoryInfo.Create();
            directoryInfo.Delete(true);
            directoryInfo.Create();

            var fileName = Path.GetFileName(preProcessingDesktopTaskContext.FileName);

            var newFilePath = Path.Combine(preProcessingDesktopTaskContext.Container, fileName);

            File.Copy(preProcessingDesktopTaskContext.FileName, newFilePath);

            preProcessingDesktopTaskContext.FileName = fileName;
            preProcessingDesktopTaskContext.OriginalFileName = fileName;

            return Task.FromResult(desktopContext);
        }
    }
}
