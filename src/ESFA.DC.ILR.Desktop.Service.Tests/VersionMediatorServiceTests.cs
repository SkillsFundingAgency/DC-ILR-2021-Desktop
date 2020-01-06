using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Tests.TestSpecificSubClasses;
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
                VersionName = versionNumber,
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
                versionInformationService.Object,
                versionService.Object);

            var found = service.GetCurrentApplicationVersion();
            found.VersionName.Should().Be(version.VersionName);
            found.ReleaseDateTime.Should().Be(version.ReleaseDateTime);
            found.Major.Should().Be(version.Major);
            found.Minor.Should().Be(version.Minor);
            found.Increment.Should().Be(version.Increment);

            var result = service.GetCurrentApplicationVersion();

            result.VersionName.Should().Be(versionNumber);
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
                VersionName = versionNumber,
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
                versionInformationService.Object,
                versionService.Object);

            var found = service.GetCurrentApplicationVersion();
            found.VersionName.Should().Be(version.VersionName);
            found.ReleaseDateTime.Should().Be(version.ReleaseDateTime);
            found.Major.Should().Be(version.Major);
            found.Minor.Should().Be(version.Minor);
            found.Increment.Should().Be(version.Increment);
        }

        private VersionMediatorServiceTestClass GetVersionMediatorService(
            IVersionFactory versionFactory = null,
            IReleaseVersionInformationService versionInformationService = null,
            IVersionService versionService = null)
        {
            return new VersionMediatorServiceTestClass(
                versionFactory ?? Mock.Of<IVersionFactory>(),
                versionInformationService ?? Mock.Of<IReleaseVersionInformationService>(),
                versionService ?? Mock.Of<IVersionService>());
        }
    }
}