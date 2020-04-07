using System;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Factories;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void VersionMessageFactory_GetVersionMessage()
        {
            var url = "test";
            var versionNumber = "1.0.0.0";
            var date = new DateTime(2020, 11, 10, 8, 0, 0);

            var applicationVersionResult = new ApplicationVersionResult
            {
                ApplicationVersion = versionNumber,
                Url = url,
                ReleaseDateTime = date
            };

            var factory = new VersionMessageFactory();

            var result = factory.GetVersionMessage(applicationVersionResult);

            result.ApplicationVersion.ApplicationVersion.Should().Be(versionNumber);
            result.ApplicationVersion.ReleaseDateTime.Should().Be(date);
            result.ApplicationVersion.Url.Should().Be(url);
        }

        [Fact]
        public void VersionFactory_GetVersion()
        {
            var versionNumber = "1.0.0.0";
            var versionSplit = versionNumber.Split('.');
            var major = Convert.ToInt32(versionSplit[0]);
            var minor = Convert.ToInt32(versionSplit[1]);
            var increment = Convert.ToInt32(versionSplit[2]);

            var refDataVersion = "1.1.8.202003231000";
            var refDataVersionSplit = refDataVersion.Split('.');
            var refDataMajor = Convert.ToInt32(refDataVersionSplit[0]);
            var refDataMinor = Convert.ToInt32(refDataVersionSplit[1]);
            var refDataIncrement = Convert.ToInt32(refDataVersionSplit[2]);
            var refDataDate = new DateTime(2020, 03, 23, 10, 0, 0);

            var factory = new VersionFactory();

            var result = factory.GetVersion(versionNumber, refDataVersion);

            result.VersionName.Should().Be(versionNumber);
            result.Major.Should().Be(major);
            result.Minor.Should().Be(minor);
            result.Increment.Should().Be(increment);

            result.ReferenceDataVersion.VersionName.Should().Be(refDataVersion);
            result.ReferenceDataVersion.Major.Should().Be(refDataMajor);
            result.ReferenceDataVersion.Minor.Should().Be(refDataMinor);
            result.ReferenceDataVersion.Increment.Should().Be(refDataIncrement);
            result.ReferenceDataVersion.ReleaseDateTime.Should().Be(refDataDate);
        }

        [Fact]
        public void ApplicationVersionResultFactory_GetApplicationVersionResult()
        {
            var url = "test";
            var versionNumber = "1.0.0.0";
            var date = new DateTime(2020, 11, 10, 8, 0, 0);

            var factory = new ApplicationVersionResultFactory();

            var result = factory.GetResult(versionNumber, date, url);

            result.ApplicationVersion.Should().Be(versionNumber);
            result.ReleaseDateTime.Should().Be(date);
            result.Url.Should().Be(url);
        }
    }
}