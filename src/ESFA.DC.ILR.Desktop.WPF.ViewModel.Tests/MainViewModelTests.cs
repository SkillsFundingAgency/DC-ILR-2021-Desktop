using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.Logging.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public async Task CheckForUpdateMenuCommand_New_Version_Available()
        {
            const string version = "2.0.0.0";
            const string url = "foo";
            var releaseDate = new DateTime(2019, 11, 10, 8, 0, 0);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = version,
                ReleaseDateTime = releaseDate,
                Url = url
            };

            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(applicationVersionResult);

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var versionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInformationServiceMock.Setup(vm => vm.VersionNumber).Returns("3.0");

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object, versionInformationService: versionInformationServiceMock.Object);

            await viewModel.CheckForUpdateMenuCommand.ExecuteAsync();

            viewModel.NewVersion.ApplicationVersion.Should().Be(version);
            viewModel.NewVersion.ReleaseDateTime.Should().Be(releaseDate);
            viewModel.NewVersion.Url.Should().Be(url);

            viewModel.NewVersionBannerVisibility.Should().BeTrue();
            viewModel.NewVersionBannerVisibilityError.Should().BeFalse();
            viewModel.UpToDateBannerVisibility.Should().BeFalse();
        }

        [Fact]
        public async Task CheckForUpdateMenuCommand_No_New_Version_Available_Error_NullObject()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync((ApplicationVersionResult)null);

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object);

            await viewModel.CheckForUpdateMenuCommand.ExecuteAsync();

            viewModel.NewVersion.Should().BeNull();

            viewModel.NewVersionBannerVisibility.Should().BeFalse();
            viewModel.NewVersionBannerVisibilityError.Should().BeTrue();
            viewModel.UpToDateBannerVisibility.Should().BeFalse();
        }

        [Fact]
        public async Task CheckForUpdateMenuCommand_No_New_Version_Available_Error_NullAppVersion()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(new ApplicationVersionResult());

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object);

            await viewModel.CheckForUpdateMenuCommand.ExecuteAsync();

            viewModel.NewVersion.Should().NotBeNull();

            viewModel.NewVersionBannerVisibility.Should().BeFalse();
            viewModel.NewVersionBannerVisibilityError.Should().BeTrue();
            viewModel.UpToDateBannerVisibility.Should().BeFalse();
        }

        [Fact]
        public async Task CheckForUpdateMenuCommand_No_New_Version_Available()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(new ApplicationVersionResult()
                {
                    ApplicationVersion = "1.0"
                });

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var versionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInformationServiceMock.Setup(vm => vm.VersionNumber).Returns("1.0");

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object, versionInformationService: versionInformationServiceMock.Object);

            await viewModel.CheckForUpdateMenuCommand.ExecuteAsync();

            viewModel.NewVersion.Should().NotBeNull();

            viewModel.NewVersionBannerVisibility.Should().BeFalse();
            viewModel.NewVersionBannerVisibilityError.Should().BeFalse();
            viewModel.UpToDateBannerVisibility.Should().BeTrue();
        }

        [Fact]
        public async Task CheckForUpdateCommand_New_Version_Available()
        {
            const string version = "2.0.0.0";
            const string url = "foo";
            var releaseDate = new DateTime(2019, 11, 10, 8, 0, 0);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = version,
                ReleaseDateTime = releaseDate,
                Url = url
            };

            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(applicationVersionResult);

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var versionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInformationServiceMock.Setup(vm => vm.VersionNumber).Returns("3.0");

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object, versionInformationService: versionInformationServiceMock.Object);

            await viewModel.CheckForUpdateCommand.ExecuteAsync();

            viewModel.NewVersion.ApplicationVersion.Should().Be(version);
            viewModel.NewVersion.ReleaseDateTime.Should().Be(releaseDate);
            viewModel.NewVersion.Url.Should().Be(url);

            viewModel.NewVersionBannerVisibility.Should().BeTrue();
            viewModel.NewVersionBannerVisibilityError.Should().BeFalse();
            viewModel.UpToDateBannerVisibility.Should().BeFalse();
        }

        [Fact]
        public async Task CheckForUpdateCommand_No_New_Version_Available()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(new ApplicationVersionResult()
                {
                    ApplicationVersion = "1.0"
                });

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var versionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInformationServiceMock.Setup(vm => vm.VersionNumber).Returns("1.0");

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object, versionInformationService: versionInformationServiceMock.Object);

            await viewModel.CheckForUpdateCommand.ExecuteAsync();

            viewModel.NewVersion.Should().NotBeNull();

            viewModel.NewVersionBannerVisibility.Should().BeFalse();
            viewModel.NewVersionBannerVisibilityError.Should().BeFalse();
            viewModel.UpToDateBannerVisibility.Should().BeTrue();
        }

        [Fact]
        public async Task CheckForUpdateCommand_API_Error()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .Throws(new Exception());

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object);

            await viewModel.CheckForUpdateCommand.ExecuteAsync();

            viewModel.NewVersion.Should().BeNull();

            viewModel.NewVersionBannerVisibility.Should().BeFalse();
            viewModel.NewVersionBannerVisibilityError.Should().BeFalse();
            viewModel.UpToDateBannerVisibility.Should().BeFalse();
        }

        [Fact]
        public void ChooseFileCommandExecute()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns(fileName);

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void ChooseFileCommandExecute_Null()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns(null as string);

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.FileName = fileName;

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void ChooseFileCommandExecute_Empty()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns(string.Empty);

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.FileName = fileName;

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void ChooseFileCommandExecute_WhiteSpace()
        {
            var fileName = "File Name";

            var dialogInteractionServiceMock = new Mock<IDialogInteractionService>();

            dialogInteractionServiceMock.Setup(s => s.GetFileNameFromOpenFileDialog()).Returns("  ");

            var viewModel = NewViewModel(dialogInteractionService: dialogInteractionServiceMock.Object);

            viewModel.FileName = fileName;

            viewModel.ChooseFileCommand.Execute(null);

            viewModel.FileName.Should().Be(fileName);
        }

        [Fact]
        public void SettingsNavigationCommandExecute()
        {
            var windowServiceMock = new Mock<IWindowService>();

            NewViewModel(windowService: windowServiceMock.Object).SettingsNavigationCommand.Execute(null);

            windowServiceMock.Verify(s => s.ShowSettingsWindow(), Times.Once);
        }

        [Fact]
        public void AboutWindowNavigationCommandExecute()
        {
            var windowServiceMock = new Mock<IWindowService>();

            NewViewModel(windowService: windowServiceMock.Object).AboutNavigationCommand.Execute(null);

            windowServiceMock.Verify(s => s.ShowAboutWindow(), Times.Once);
        }

        [Fact]
        public void HandleTaskProgressMessage()
        {
            var taskName = "TaskName";
            var currentTask = 1;
            var taskCount = 2;

            var taskProgressMessage = new TaskProgressMessage(taskName, currentTask, taskCount);

            var viewModel = NewViewModel();

            viewModel.HandleTaskProgressMessage(taskProgressMessage);

            viewModel.TaskName.Should().Be(taskName);
            viewModel.CurrentTask.Should().Be(currentTask);
            viewModel.TaskCount.Should().Be(taskCount);
        }

        [Fact]
        public async Task ProcessFileCommandExecute()
        {
            var fileName = "FileName";
            var outputDirectory = "Output Directory";

            var ilrDesktopServiceMock = new Mock<IIlrDesktopService>();

            var completionContextMock = new Mock<ICompletionContext>();

            completionContextMock.SetupGet(c => c.OutputDirectory).Returns(outputDirectory);
            completionContextMock.SetupGet(c => c.ProcessingCompletionState).Returns(ProcessingCompletionStates.Success);

            var desktopContextMock = new Mock<IDesktopContext>();
            var desktopContextFactoryMock = new Mock<IDesktopContextFactory>();

            desktopContextFactoryMock.Setup(f => f.Build(fileName)).Returns(desktopContextMock.Object);

            var viewModel = NewViewModel(ilrDesktopServiceMock.Object, desktopContextFactoryMock.Object);

            ilrDesktopServiceMock.Setup(s => s.ProcessAsync(desktopContextMock.Object, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(completionContextMock.Object));

            viewModel.FileName = fileName;
            viewModel.CanSubmit = true;

            await viewModel.ProcessFileCommand.ExecuteAsync();

            viewModel.ReportsLocation.Should().Be(outputDirectory);
            viewModel.CurrentStage.Should().Be(StageKeys.ProcessedSuccessfully);
            viewModel.CanSubmit.Should().BeFalse();
            viewModel.FileName.Should().Be("No file chosen");
        }

        [Fact]
        public async Task ApplicationVersionUpToDate()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(new ApplicationVersionResult()
                {
                    ApplicationVersion = "1.0"
                });

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var versionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInformationServiceMock.Setup(vm => vm.VersionNumber).Returns("1.0");

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object, versionInformationService: versionInformationServiceMock.Object);

            await viewModel.CheckForUpdateCommand.ExecuteAsync();

            viewModel.ApplicationVersionUpToDate().Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationVersionUpdateAvailable()
        {
            const string version = "2.0.0.0";
            const string url = "foo";
            var releaseDate = new DateTime(2019, 11, 10, 8, 0, 0);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = version,
                ReleaseDateTime = releaseDate,
                Url = url
            };

            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync(applicationVersionResult);

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var versionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInformationServiceMock.Setup(vm => vm.VersionNumber).Returns("1.0");

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object, versionInformationService: versionInformationServiceMock.Object);

            await viewModel.CheckForUpdateMenuCommand.ExecuteAsync();

            viewModel.ApplicationVersionUpdateAvailable().Should().BeTrue();
        }

        [Fact]
        public async Task ApplicationVersionUpdateError()
        {
            var versionMediatorServiceMock = new Mock<IVersionMediatorService>();
            versionMediatorServiceMock
                .Setup(m => m.GetNewVersion())
                .ReturnsAsync((ApplicationVersionResult)null);

            var featureSwitchService = new Mock<IFeatureSwitchService>();
            featureSwitchService.Setup(m => m.VersionUpdate).Returns(true);

            var viewModel = NewViewModel(versionMediatorService: versionMediatorServiceMock.Object, featureSwitchService: featureSwitchService.Object);
            await viewModel.CheckForUpdateMenuCommand.ExecuteAsync();

            viewModel.ApplicationVersionUpdateError().Should().BeTrue();
        }

        private MainViewModel NewViewModel(
            IIlrDesktopService ilrDesktopService = null,
            IDesktopContextFactory desktopContextFactory = null,
            IMessengerService messengerService = null,
            IWindowService windowService = null,
            IDialogInteractionService dialogInteractionService = null,
            IWindowsProcessService windowsProcessService = null,
            IReleaseVersionInformationService versionInformationService = null,
            ILogger logger = null,
            IFeatureSwitchService featureSwitchService = null,
            IVersionMediatorService versionMediatorService = null)
        {
            return new MainViewModel(
                ilrDesktopService ?? Mock.Of<IIlrDesktopService>(),
                desktopContextFactory ?? Mock.Of<IDesktopContextFactory>(),
                messengerService ?? Mock.Of<IMessengerService>(),
                windowService ?? Mock.Of<IWindowService>(),
                dialogInteractionService ?? Mock.Of<IDialogInteractionService>(),
                windowsProcessService ?? Mock.Of<IWindowsProcessService>(),
                versionInformationService ?? Mock.Of<IReleaseVersionInformationService>(),
                logger ?? Mock.Of<ILogger>(),
                featureSwitchService ?? Mock.Of<IFeatureSwitchService>(),
                versionMediatorService ?? Mock.Of<IVersionMediatorService>());
        }
    }
}