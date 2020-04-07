using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.Logging.Interfaces;
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
            var releaseDateTime = new DateTime(2020, 11, 19, 8, 0, 0);
            var url = "foo.com";
            var major = 1;
            var minor = 1;
            var increment = 1;
            var refDataVersionNumber = "1.1.1.1";
            var refDataFileName = "FISReferenceData.1.1.1.1";

            var currentVersion = new Version
            {
                VersionName = "1.0.0.0",
                ReleaseDateTime = releaseDateTime,
                Major = currentMajor,
                Minor = currentMinor,
                Increment = currentIncrement,
                ReferenceDataVersion = new Models.ReferenceData
                {
                    FileName = "FISReferenceData.1.1.1.1",
                    Major = currentMajor,
                    Minor = currentMinor,
                    Increment = currentIncrement,
                    VersionName = refDataVersionNumber
                }
            };

            var applicationVersion = GetApplicationVersion(
                versionNumber,
                url,
                releaseDateTime,
                releaseDateTime,
                major,
                minor,
                increment,
                refDataVersionNumber);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDateTime,
                LatestReferenceDataVersion = refDataVersionNumber,
                LatestReferenceDataFileName = refDataFileName
            };

            var versionClientMock = new Mock<IApplicationVersionResultClient>();
            versionClientMock
                .Setup(m => m.GetAsync())
                .ReturnsAsync(applicationVersion);

            var versionResultFactory = new Mock<IAPIResultFactory<ApplicationVersionResult>>();
            versionResultFactory
                .Setup(m => m.GetResult(versionNumber, releaseDateTime, url, refDataVersionNumber, refDataFileName))
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
            var releaseDateTime = new DateTime(2020, 11, 19, 8, 0, 0);
            var url = "foo.com";
            var major = 1;
            var minor = 1;
            var increment = 1;
            var refDataVersionNumber = "1.1.1.1";
            var refDataFileName = "FISReferenceData.1.1.1.1";

            var currentVersion = new Version
            {
                VersionName = "1.0.0.0",
                ReleaseDateTime = releaseDateTime,
                Major = currentMajor,
                Minor = currentMinor,
                Increment = currentIncrement,
                ReferenceDataVersion = new Models.ReferenceData
                {
                    FileName = "FISReferenceData.1.1.1.1",
                    Major = currentMajor,
                    Minor = currentMinor,
                    Increment = currentIncrement,
                    VersionName = refDataVersionNumber
                }
            };

            var applicationVersion = GetApplicationVersion(
                versionNumber,
                url,
                releaseDateTime,
                releaseDateTime,
                major,
                minor,
                increment,
                refDataVersionNumber);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDateTime,
                LatestReferenceDataVersion = refDataVersionNumber,
                LatestReferenceDataFileName = refDataFileName
            };

            var versionClientMock = new Mock<IApplicationVersionResultClient>();
            versionClientMock
                .Setup(m => m.GetAsync())
                .ReturnsAsync(applicationVersion);

            var versionResultFactory = new Mock<IAPIResultFactory<ApplicationVersionResult>>();
            versionResultFactory
                .Setup(m => m.GetResult(versionNumber, releaseDateTime, url, refDataVersionNumber, refDataFileName))
                .Returns(applicationVersionResult);

            var service = NewService(versionClientMock.Object, versionResultFactory.Object);

            var result = await service.GetLatestApplicationVersion(currentVersion);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetLatestApplicationVersion_Correctly_Returns_NewVersion_RefData()
        {
            var versionNumber = "1.1.1.1";
            var releaseDateTime = new DateTime(2020, 11, 19, 8, 0, 0);
            var url = "foo.com";
            var major = 1;
            var minor = 1;
            var increment = 1;
            var refDataVersionNumber = "1.1.2";
            var refDataFileName = "FISReferenceData.1.1.2";

            var currentVersion = new Version
            {
                VersionName = versionNumber,
                ReleaseDateTime = releaseDateTime,
                Major = major,
                Minor = minor,
                Increment = increment,
                ReferenceDataVersion = new Models.ReferenceData
                {
                    FileName = refDataFileName,
                    Major = major,
                    Minor = minor,
                    Increment = 2,
                    VersionName = refDataVersionNumber
                }
            };

            var applicationVersion = GetApplicationVersion(
                versionNumber,
                url,
                releaseDateTime,
                releaseDateTime,
                major,
                minor,
                increment,
                refDataVersionNumber);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = releaseDateTime,
                LatestReferenceDataVersion = refDataVersionNumber,
                LatestReferenceDataFileName = refDataFileName
            };

            var versionClientMock = new Mock<IApplicationVersionResultClient>();
            versionClientMock
                .Setup(m => m.GetAsync())
                .ReturnsAsync(applicationVersion);

            var versionResultFactory = new Mock<IAPIResultFactory<ApplicationVersionResult>>();
            versionResultFactory
                .Setup(m => m.GetResult(versionNumber, releaseDateTime, url, refDataVersionNumber, refDataFileName))
                .Returns(applicationVersionResult);

            var service = NewService(versionClientMock.Object, versionResultFactory.Object);

            var result = await service.GetLatestApplicationVersion(currentVersion);

            result.ApplicationVersion.Should().Be(versionNumber);
            result.LatestReferenceDataVersion.Should().Be(refDataVersionNumber);
        }

        private ApplicationVersion GetApplicationVersion(
            string version = null,
            string url = null,
            DateTime? updatedDate = null,
            DateTime? releaseDate = null,
            int? major = null,
            int? minor = null,
            int? increment = null,
            string refDataVersionNumber = null)
        {
            version = version ?? "1.1.1.1";
            url = url ?? "foo.com";
            updatedDate = updatedDate ?? DateTime.Now;
            releaseDate = releaseDate ?? DateTime.Now;
            major = major ?? 1;
            minor = minor ?? 0;
            increment = increment ?? 0;
            refDataVersionNumber = refDataVersionNumber ?? "1.1.1.1";

            var applicationVersion = new ApplicationVersion
            {
                Url = url,
                LastUpdated = updatedDate.Value,
                Versions = new List<Version>
                {
                    new Version
                    {
                        VersionName = version,
                        ReleaseDateTime = releaseDate.Value,
                        Major = major.Value,
                        Minor = minor.Value,
                        Increment = increment.Value,
                        ReferenceDataVersion = new Models.ReferenceData
                        {
                            FileName = "FISReferenceData.1.1.1.1",
                            Major = major.Value,
                            Minor = minor.Value,
                            Increment = increment.Value,
                            VersionName = refDataVersionNumber
                        }
                    }
                }
            };

            return applicationVersion;
        }

        private VersionService NewService(
            IApplicationVersionResultClient versionClient = null,
            IAPIResultFactory<ApplicationVersionResult> applicationVersionResultFactory = null,
            ILogger loggerMock = null)
        {
            return new VersionService(
                versionClient ?? Mock.Of<IApplicationVersionResultClient>(),
                applicationVersionResultFactory ?? Mock.Of<IAPIResultFactory<ApplicationVersionResult>>(),
                loggerMock ?? Mock.Of<ILogger>());
        }
    }
}