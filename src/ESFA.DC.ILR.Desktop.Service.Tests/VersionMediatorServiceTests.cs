using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using FluentAssertions;
using Moq;
using Xunit;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class VersionMediatorServiceTests
    {
        [Fact]
        public async Task CheckForUpdates_Returns_True_And_Sends_Message()
        {
            var url = "test";
            var versionNumber = "1.0.0.0";
            var releaseDate = new DateTime(2019, 11, 10, 8, 0, 0);
            var versionSplit = versionNumber.Split('.');
            var major = Convert.ToInt32(versionSplit[0]);
            var minor = Convert.ToInt32(versionSplit[0]);
            var increment = Convert.ToInt32(versionSplit[0]);

            var version = new Version
            {
                ApplicationVersion = versionNumber,
                Major = major,
                Minor = minor,
                Increment = increment
            };

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDate
            };

            var versionMessage = new VersionMessage
            {
                ApplicationVersion = applicationVersionResult
            };

            var versionMessageFactoryMock = new Mock<IVersionMessageFactory>();
            versionMessageFactoryMock
                .Setup(m => m.GetVersionMessage(applicationVersionResult))
                .Returns(versionMessage);

            var versionFactoryMock = new Mock<IVersionFactory>();
            versionFactoryMock
                .Setup(m => m.GetVersion(versionNumber))
                .Returns(version);

            var messageServiceMock = new Mock<IMessengerService>();

            var versionInformationService = new Mock<IReleaseVersionInformationService>();
            versionInformationService
                .Setup(m => m.VersionNumber)
                .Returns(versionNumber);

            var versionService = new Mock<IVersionService>();
            versionService
                .Setup(m => m.GetLatestApplicationVersion(version))
                .ReturnsAsync(applicationVersionResult);

            var service = GetVersionMediatorService(
                versionFactoryMock.Object,
                versionMessageFactoryMock.Object,
                messageServiceMock.Object,
                versionInformationService.Object,
                versionService.Object);

            var found = await service.CheckForUpdates();
            found.Should().BeTrue();
            messageServiceMock.Verify(m => m.Send(versionMessage), Times.Once);

            var result = service.GetCurrentApplicationVersion();

            result.ApplicationVersion.Should().Be(versionNumber);
            result.Major.Should().Be(major);
            result.Minor.Should().Be(minor);
            result.Increment.Should().Be(increment);
        }

        [Fact]
        public async Task CheckForUpdates_Returns_Fasle_And_Does_Not_Send_Message()
        {
            var url = "test";
            var versionNumber = "1.0.0.0";
            var releaseDate = new DateTime(2019, 11, 10, 8, 0, 0);
            var versionSplit = versionNumber.Split('.');
            var major = Convert.ToInt32(versionSplit[0]);
            var minor = Convert.ToInt32(versionSplit[1]);
            var increment = Convert.ToInt32(versionSplit[2]);

            var version = new Version
            {
                ApplicationVersion = versionNumber,
                Major = major,
                Minor = minor,
                Increment = increment
            };

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDate
            };

            var versionMessage = new VersionMessage
            {
                ApplicationVersion = applicationVersionResult
            };

            var versionMessageFactoryMock = new Mock<IVersionMessageFactory>();
            versionMessageFactoryMock
                .Setup(m => m.GetVersionMessage(applicationVersionResult))
                .Returns(versionMessage);

            var versionFactoryMock = new Mock<IVersionFactory>();
            versionFactoryMock
                .Setup(m => m.GetVersion(versionNumber))
                .Returns(version);

            var messageServiceMock = new Mock<IMessengerService>();

            var versionInformationService = new Mock<IReleaseVersionInformationService>();
            versionInformationService
                .Setup(m => m.VersionNumber)
                .Returns(versionNumber);

            var versionService = new Mock<IVersionService>();
            versionService
                .Setup(m => m.GetLatestApplicationVersion(version))
                .ReturnsAsync(default(ApplicationVersionResult));

            var service = GetVersionMediatorService(
                versionFactoryMock.Object,
                versionMessageFactoryMock.Object,
                messageServiceMock.Object,
                versionInformationService.Object,
                versionService.Object);

            var found = await service.CheckForUpdates();
            found.Should().BeFalse();
            messageServiceMock.Verify(m => m.Send(It.IsAny<VersionMessage>()), Times.Never);
        }

        private VersionMediatorServiceTestClass GetVersionMediatorService(
            IVersionFactory versionFactory = null,
            IVersionMessageFactory versionMessageFactory = null,
            IMessengerService messengerService = null,
            IReleaseVersionInformationService versionInformationService = null,
            IVersionService versionService = null)
        {
            return new VersionMediatorServiceTestClass(
                versionFactory ?? Mock.Of<IVersionFactory>(),
                versionMessageFactory ?? Mock.Of<IVersionMessageFactory>(),
                messengerService ?? Mock.Of<IMessengerService>(),
                versionInformationService ?? Mock.Of<IReleaseVersionInformationService>(),
                versionService ?? Mock.Of<IVersionService>());
        }
    }

    public class VersionMediatorServiceTestClass : VersionMediatorService
    {
        public VersionMediatorServiceTestClass(
            IVersionFactory versionFactory,
            IVersionMessageFactory versionMessageFactory,
            IMessengerService messengerService,
            IReleaseVersionInformationService versionInformationService,
            IVersionService versionService)
        : base(versionFactory, versionMessageFactory, messengerService, versionInformationService, versionService)
        {
        }

        public new Version GetCurrentApplicationVersion()
        {
            return base.GetCurrentApplicationVersion();
        }
    }
}