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

            if (Directory.Exists(preProcessingDesktopTaskContext.Container))
            {
                Directory.Delete(preProcessingDesktopTaskContext.Container, true);
            }

            Directory.CreateDirectory(preProcessingDesktopTaskContext.Container);

            var fileName = Path.GetFileName(preProcessingDesktopTaskContext.FileName);

            var newFilePath = Path.Combine(preProcessingDesktopTaskContext.Container, fileName);

            File.Copy(preProcessingDesktopTaskContext.FileName, newFilePath);

            preProcessingDesktopTaskContext.FileName = fileName;
            preProcessingDesktopTaskContext.OriginalFileName = fileName;

            var fileSizeInBytes = new FileInfo(newFilePath).Length;
            preProcessingDesktopTaskContext.FileSizeInBytes = fileSizeInBytes;

            return Task.FromResult(desktopContext);
        }
    }
}
