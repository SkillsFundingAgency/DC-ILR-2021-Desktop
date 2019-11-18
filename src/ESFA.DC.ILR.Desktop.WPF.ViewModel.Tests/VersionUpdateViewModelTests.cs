using System;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using Moq;
using Xunit;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class VersionUpdateViewModelTests
    {
        [Fact]
        public void AboutWindowNavigationCommandExecute()
        {
            var url = "foo";
            var version = "1.0.0.0";
            var releaseDate = new DateTime(2019, 11, 1, 10, 0, 0);

            var versionModel = new Version
            {
                ApplicationVersion = version,
                Major = 1,
                Minor = 0,
                Increment = 0
            };

            var versionResult = new ApplicationVersionResult
            {
                ApplicationVersion = version,
                ReleaseDateTime = releaseDate,
                Url = url
            };

            var windowServiceMock = new Mock<IWindowsProcessService>();

            var versionInfoServiceMock = new Mock<IReleaseVersionInformationService>();
            versionInfoServiceMock.Setup(m => m.VersionNumber).Returns(version);

            var versionFactoryMock = new Mock<IVersionFactory>();
            versionFactoryMock.Setup(m => m.GetVersion(version)).Returns(versionModel);

            var versionServiceMock = new Mock<IVersionService>();
            versionServiceMock.Setup(m => m.GetLatestApplicationVersion(versionModel)).ReturnsAsync(versionResult);

            var viewModel = NewViewModel(
                windowServiceMock.Object,
                versionInfoServiceMock.Object,
                versionServiceMock.Object,
                versionFactoryMock.Object);

            viewModel.VersionNavigationCommand.Execute(null);

            windowServiceMock.Verify(m => m.ProcessStart(url), Times.Once);
        }

        [Fact]
        public void CloseWindow()
        {
            var viewModel = NewViewModel();

            var closeableMock = new Mock<ICloseable>();

            viewModel.CloseWindowCommand.Execute(closeableMock.Object);

            closeableMock.Verify(c => c.Close());
        }

        private VersionUpdateViewModel NewViewModel(
            IWindowsProcessService windowsProcessService = null,
            IReleaseVersionInformationService releaseVersionInformationService = null,
            IVersionService versionService = null,
            IVersionFactory versionFactory = null)
        {
            return new VersionUpdateViewModel(
                windowsProcessService ?? Mock.Of<IWindowsProcessService>(),
                releaseVersionInformationService ?? Mock.Of<IReleaseVersionInformationService>(),
                versionService ?? Mock.Of<IVersionService>(),
                versionFactory ?? Mock.Of<IVersionFactory>());
        }
    }
}