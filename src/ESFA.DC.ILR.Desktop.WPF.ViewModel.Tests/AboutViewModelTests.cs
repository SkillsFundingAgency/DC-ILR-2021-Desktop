using System;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.Tests
{
    public class AboutViewModelTests
    {
        [Fact]
        public void CloseWindow()
        {
            var viewModel = NewViewModel();

            var closeableMock = new Mock<ICloseable>();

            viewModel.CloseWindowCommand.Execute(closeableMock.Object);

            closeableMock.Verify(c => c.Close());
        }

        [Fact]
        public void AboutItems()
        {
            var referenceDataVersionInformationServiceMock = new Mock<IReferenceDataVersionInformationService>();
            referenceDataVersionInformationServiceMock.SetupGet(x => x.Date).Returns(new DateTime(2020, 12, 25, 1, 2, 3).ToString());
            referenceDataVersionInformationServiceMock.SetupGet(x => x.VersionNumber).Returns("123.123.123");

            var releaseVersionInformationServiceMock = new Mock<IReleaseVersionInformationService>();
            releaseVersionInformationServiceMock.SetupGet(x => x.Date).Returns(new DateTime(2121, 12, 25, 1, 2, 3).ToString());
            releaseVersionInformationServiceMock.SetupGet(x => x.VersionNumber).Returns("123.123.123");

            var viewModel = NewViewModel(referenceDataVersionInformationServiceMock.Object, releaseVersionInformationServiceMock.Object);

            viewModel.AboutItems.Should().HaveCount(3);

            viewModel.AboutItems[0].Key.Should().Be("Version Number:");
            viewModel.AboutItems[0].Value.Should().Be("123.123.123");

            viewModel.AboutItems[1].Key.Should().Be("Release Date:");
            viewModel.AboutItems[1].Value.Should().Be(new DateTime(2121, 12, 25, 1, 2, 3).ToString());

            viewModel.AboutItems[2].Key.Should().Be("Reference Data Date:");
            viewModel.AboutItems[2].Value.Should().Be(new DateTime(2020, 12, 25, 1, 2, 3).ToString());
        }

        private AboutViewModel NewViewModel(IReferenceDataVersionInformationService referenceDataVersionInformationService = null, IReleaseVersionInformationService releaseVersionInformationService = null)
        {
            return new AboutViewModel(referenceDataVersionInformationService ?? new Mock<IReferenceDataVersionInformationService>().Object, releaseVersionInformationService ?? new Mock<IReleaseVersionInformationService>().Object);
        }
    }
}
