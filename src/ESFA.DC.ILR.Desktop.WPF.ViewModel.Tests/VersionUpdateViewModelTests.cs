using System;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests.TestSpecificSubClasses;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class VersionUpdateViewModelTests
    {
        [Fact]
        public void NavigateToVersionsUrl_Calls_WindowsProcessService_ProcessStart_With_Correct_Url()
        {
            var url = "foo.com";

            var messengerServiceMock = new Mock<IMessengerService>();

            var applicationVersionResult = new ApplicationVersionResult
            {
                Url = url
            };

            var windowServiceMock = new Mock<IWindowsProcessService>();
            windowServiceMock.Setup(m => m.ProcessStart(url));

            var viewModel = NewViewModel(
                messengerServiceMock.Object,
                windowServiceMock.Object);
            viewModel.ApplicationVersionResult = applicationVersionResult;

            viewModel.VersionNavigationCommand.Execute(null);

            windowServiceMock.Verify(m => m.ProcessStart(url), Times.Once);
        }

        [Fact]
        public void Constructor_Registers_VersionMessage_Message_Handler()
        {
            var messengerServiceMock = new Mock<IMessengerService>();

            var viewModel = NewViewModel(messengerServiceMock.Object);

            messengerServiceMock.Verify(m => m.Register(viewModel, It.IsAny<Action<VersionMessage>>()));
        }

        [Fact]
        public void Initialize_Sets_Properties_To_Passed_In_Values()
        {
            var applicationVersion = "1.1.1.1";
            var url = "foo.com";
            var releaseDate = DateTime.Now;

            var message = new VersionMessage
            {
                ApplicationVersion = new ApplicationVersionResult
                {
                    ApplicationVersion = applicationVersion,
                    Url = url,
                    ReleaseDateTime = releaseDate
                }
            };

            var viewModelStub = new VersionUpdateViewModelTestSpecificTestClass(
                Mock.Of<IMessengerService>(),
                Mock.Of<IWindowsProcessService>());

            viewModelStub.Initialize(message);

            viewModelStub.ShowProgress.Should().BeFalse();
            viewModelStub.ApplicationVersionResult.Url.Should().Be(url);
            viewModelStub.ApplicationVersionResult.ApplicationVersion.Should().Be(applicationVersion);
            viewModelStub.ApplicationVersionResult.ReleaseDateTime.Should().Be(releaseDate);

            viewModelStub.VersionItems.Should().HaveCount(2);
            viewModelStub.VersionItems[1].Value.Should().Be(releaseDate.ToString());
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
            IMessengerService messengerService = null,
            IWindowsProcessService windowsProcessService = null)
        {
            return new VersionUpdateViewModel(
                messengerService ?? Mock.Of<IMessengerService>(),
                windowsProcessService ?? Mock.Of<IWindowsProcessService>());
        }
    }
}