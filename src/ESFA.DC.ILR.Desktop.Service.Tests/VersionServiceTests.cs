using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using FluentAssertions;
using Moq;
using Xunit;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class VersionServiceTests
    {
        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 0)]
        public async Task GetLatestApplicationVersion_Correctly_Returns_Newer_Version(
            int currentMajor,
            int currentMinor,
            int currentIncrement)
        {
            var versionNumber = "1.1.1.1";
            var releaseDateTime = new DateTime(2019, 11, 19, 8, 0, 0);
            var url = "foo.com";
            var major = 1;
            var minor = 1;
            var increment = 1;

            var currentVersion = new Version
            {
                ApplicationVersion = "1.0.0.0",
                ReleaseDateTime = releaseDateTime,
                Major = currentMajor,
                Minor = currentMinor,
                Increment = currentIncrement
            };

            var applicationVersion = GetApplicationVersion(
                versionNumber,
                url,
                releaseDateTime,
                releaseDateTime,
                major,
                minor,
                increment);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDateTime
            };

            var versionClientMock = new Mock<IApplicationVersionClient>();
            versionClientMock
                .Setup(m => m.GetApplicationVersionsAsync())
                .ReturnsAsync(applicationVersion);

            var versionResultFactory = new Mock<IApplicationVersionResultFactory>();
            versionResultFactory
                .Setup(m => m.GetApplicationVersionResult(versionNumber, releaseDateTime, url))
                .Returns(applicationVersionResult);

            var service = NewService(versionClientMock.Object, versionResultFactory.Object);

            var result = await service.GetLatestApplicationVersion(currentVersion);

            result.ApplicationVersion.Should().Be(versionNumber);
            result.ReleaseDateTime.Should().Be(releaseDateTime);
            result.Url.Should().Be(url);
        }

        [Theory]
        [InlineData(2, 0, 0)]
        [InlineData(1, 2, 1)]
        [InlineData(1, 1, 2)]
        public async Task GetLatestApplicationVersion_Correctly_Returns_Null_When_No_Newer_Version(
            int currentMajor,
            int currentMinor,
            int currentIncrement)
        {
            var versionNumber = "1.1.1.1";
            var releaseDateTime = new DateTime(2019, 11, 19, 8, 0, 0);
            var url = "foo.com";
            var major = 1;
            var minor = 1;
            var increment = 1;

            var currentVersion = new Version
            {
                ApplicationVersion = "1.0.0.0",
                ReleaseDateTime = releaseDateTime,
                Major = currentMajor,
                Minor = currentMinor,
                Increment = currentIncrement
            };

            var applicationVersion = GetApplicationVersion(
                versionNumber,
                url,
                releaseDateTime,
                releaseDateTime,
                major,
                minor,
                increment);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDateTime
            };

            var versionClientMock = new Mock<IApplicationVersionClient>();
            versionClientMock
                .Setup(m => m.GetApplicationVersionsAsync())
                .ReturnsAsync(applicationVersion);

            var versionResultFactory = new Mock<IApplicationVersionResultFactory>();
            versionResultFactory
                .Setup(m => m.GetApplicationVersionResult(versionNumber, releaseDateTime, url))
                .Returns(applicationVersionResult);

            var service = NewService(versionClientMock.Object, versionResultFactory.Object);

            var result = await service.GetLatestApplicationVersion(currentVersion);

            result.Should().BeNull();
        }

        private ApplicationVersion GetApplicationVersion(
            string version = null,
            string url = null,
            DateTime? updatedDate = null,
            DateTime? releaseDate = null,
            int? major = null,
            int? minor = null,
            int? increment = null)
        {
            version = version ?? "1.1.1.1";
            url = url ?? "foo.com";
            updatedDate = updatedDate ?? DateTime.Now;
            releaseDate = releaseDate ?? DateTime.Now;
            major = major ?? 1;
            minor = minor ?? 0;
            increment = increment ?? 0;

            var applicationVersion = new ApplicationVersion
            {
                Url = url,
                LastUpdated = updatedDate.Value,
                Versions = new List<Version>
                {
                    new Version
                    {
                        ApplicationVersion = version,
                        ReleaseDateTime = releaseDate.Value,
                        Major = major.Value,
                        Minor = minor.Value,
                        Increment = increment.Value
                    }
                }
            };

            return applicationVersion;
        }

        private VersionService NewService(
            IApplicationVersionClient versionClient = null,
            IApplicationVersionResultFactory applicationVersionResultFactory = null)
        {
            return new VersionService(
                versionClient ?? Mock.Of<IApplicationVersionClient>(),
                applicationVersionResultFactory ?? Mock.Of<IApplicationVersionResultFactory>());
        }
    }
}