using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class DesktopReferenceDataDownloadServiceTests
    {
        [Fact]
        public async Task GetReferenceData()
        {
            var cancellationToken = CancellationToken.None;
            var fileName = "FileName.zip";
            var versionNumber = "1.0.0.0";
            var byteArray = new byte[] { };
            Stream stream = new MemoryStream();

            var apiClientMock = new Mock<IReferenceDataResultClient>();
            apiClientMock.Setup(a => a.GetAsync(fileName)).Returns(Task.FromResult(byteArray));

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(f => f.OpenWriteStreamAsync(It.IsAny<string>(), null, cancellationToken)).Returns(Task.FromResult(stream));

            var desktopSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopSettingsMock.Setup(d => d.SaveAsync(cancellationToken)).Returns(Task.CompletedTask);

            await NewService(apiClientMock.Object, desktopSettingsMock.Object, fileServiceMock.Object).GetReferenceData(fileName, versionNumber);

            apiClientMock.VerifyAll();
            fileServiceMock.VerifyAll();
            desktopSettingsMock.VerifyAll();
        }

        private DesktopReferenceDataDownloadService NewService(
            IReferenceDataResultClient apiClient = null,
            IDesktopServiceSettings desktopServiceSettings = null,
            IFileService fileService = null)
        {
            return new DesktopReferenceDataDownloadService(apiClient, desktopServiceSettings, fileService);
        }
    }
}
