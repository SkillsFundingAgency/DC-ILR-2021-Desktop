using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class DesktopReferenceDataDownloadService : IDesktopReferenceDataDownloadService
    {
        private const string DownloadFolder = @"ReferenceData\";

        private readonly IReferenceDataResultClient _apiClient;
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IFileService _fileService;
        private readonly ILogger _logger;

        public DesktopReferenceDataDownloadService(
            IReferenceDataResultClient apiClient,
            IDesktopServiceSettings desktopServiceSettings,
            IFileService fileService,
            ILogger logger)
        {
            _apiClient = apiClient;
            _desktopServiceSettings = desktopServiceSettings;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task GetReferenceData(string fileName, string versionNumber)
        {
            _logger.LogInfo("Retrieving latest Reference Data.");
            var cancellationToken = CancellationToken.None;

            try
            {
                var byteArray = await _apiClient.GetAsync(fileName);

                _logger.LogInfo(string.Concat("Downloading Reference Data version: ", fileName));
                using (var fileStream = new MemoryStream(byteArray))
                {
                    using (var writeStream = await _fileService.OpenWriteStreamAsync(DownloadFolder + fileName, null, cancellationToken))
                    {
                        await fileStream.CopyToAsync(writeStream, 8096, cancellationToken);
                    }

                    await SaveReferenceDataVersionToConfig(versionNumber);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(string.Concat("Error when downloading Reference Data version: ", fileName), e);
                throw;
            }
        }

        private async Task SaveReferenceDataVersionToConfig(string versionNumber)
        {
            _logger.LogInfo("Updating config file with reference data version number.");
            _desktopServiceSettings.ReferenceDataVersion = versionNumber;

            await _desktopServiceSettings.SaveAsync(CancellationToken.None);
        }
    }
}
