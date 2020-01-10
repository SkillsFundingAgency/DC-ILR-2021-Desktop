using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class DesktopReferenceDataDownloadService : IDesktopReferenceDataDownloadService
    {
        private readonly IReferenceDataResultClient _apiClient;
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IFileService _fileService;

        public DesktopReferenceDataDownloadService(IReferenceDataResultClient apiClient, IDesktopServiceSettings desktopServiceSettings, IFileService fileService)
        {
            _apiClient = apiClient;
            _desktopServiceSettings = desktopServiceSettings;
            _fileService = fileService;
        }

        public async Task GetReferenceData(string fileName, string versionNumber)
        {
            var cancellationToken = CancellationToken.None;

            var byteArray = await _apiClient.GetAsync(fileName);

            using (var fileStream = new MemoryStream(byteArray))
            {
                using (var writeStream = await _fileService.OpenWriteStreamAsync(@"ReferenceData\" + fileName, null, cancellationToken))
                {
                    await fileStream.CopyToAsync(writeStream, 8096, cancellationToken);
                }

                await SaveReferenceDataVersionToConfig(versionNumber);
            }
        }

        private async Task SaveReferenceDataVersionToConfig(string versionNumber)
        {
            _desktopServiceSettings.ReferenceDataVersion = versionNumber;

            await _desktopServiceSettings.SaveAsync(CancellationToken.None);
        }
    }
}
