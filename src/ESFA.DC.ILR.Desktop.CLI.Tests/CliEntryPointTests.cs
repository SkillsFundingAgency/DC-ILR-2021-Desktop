using System.Threading;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.CLI.Service;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.Logging.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.CLI.Tests
{
    public class CliEntryPointTests
    {
        [Fact]
        public void ExecuteAsync()
        {
            var context = Mock.Of<IDesktopContext>();
            var commandLineArguments = Mock.Of<ICommandLineArguments>();
            var cancellationToken = CancellationToken.None;

            var desktopContextFactoryMock = new Mock<IDesktopContextFactory>();

            desktopContextFactoryMock.Setup(f => f.Build(commandLineArguments)).Returns(context);

            var ilrDesktopServiceMock = new Mock<IIlrDesktopService>();

            NewEntryPoint(desktopContextFactory: desktopContextFactoryMock.Object, ilrDesktopService: ilrDesktopServiceMock.Object).ExecuteAsync(commandLineArguments, cancellationToken);

            ilrDesktopServiceMock.Verify(s => s.ProcessAsync(context, cancellationToken));
        }

        [Fact]
        public void MessengerRegistration()
        {
            var messengerService = new Mock<IMessengerService>();

            var entryPoint = NewEntryPoint(messengerService.Object);

            messengerService.Verify(m => m.Register<TaskProgressMessage>(entryPoint, entryPoint.HandleTaskProgressMessage));
        }

        private CliEntryPoint NewEntryPoint(
            IMessengerService messengerService = null,
            IDesktopContextFactory desktopContextFactory = null,
            IIlrDesktopService ilrDesktopService = null,
            IVersionMediatorService versionMediatorService = null,
            IDesktopReferenceDataDownloadService desktopReferenceDataDownloadService = null,
            IReferenceDataVersionInformationService referenceDataVersionInformationService = null,
            IReleaseVersionInformationService releaseVersionInformationService = null,
            IFeatureSwitchService featureSwitchService = null)
        {
            return new CliEntryPoint(
                messengerService ?? Mock.Of<IMessengerService>(),
                desktopContextFactory ?? Mock.Of<IDesktopContextFactory>(),
                ilrDesktopService ?? Mock.Of<IIlrDesktopService>(),
                versionMediatorService ?? Mock.Of<IVersionMediatorService>(),
                desktopReferenceDataDownloadService ?? Mock.Of<IDesktopReferenceDataDownloadService>(),
                referenceDataVersionInformationService ?? Mock.Of<IReferenceDataVersionInformationService>(),
                releaseVersionInformationService ?? Mock.Of<IReleaseVersionInformationService>(),
                featureSwitchService ?? Mock.Of<IFeatureSwitchService>(),
                Mock.Of<ILogger>());
        }
    }
}
