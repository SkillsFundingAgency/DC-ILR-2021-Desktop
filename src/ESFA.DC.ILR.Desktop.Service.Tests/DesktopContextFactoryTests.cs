using System;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class DesktopContextFactoryTests
    {
        [Fact]
        public void DateTimeUtc()
        {
            var dateTime = new DateTime(2019, 6, 1);

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            dateTimeProviderMock.Setup(p => p.GetNowUtc()).Returns(dateTime);

            NewFactory(dateTimeProviderMock.Object).Build(string.Empty).DateTimeUtc.Should().Be(dateTime);
        }

        [Fact]
        public void Container()
        {
            NewFactory().Build(string.Empty).KeyValuePairs["Container"].Should().Be("Sandbox");
        }

        [Fact]
        public void Filename()
        {
            var fileName = "FileName Value";

            NewFactory().Build(fileName).KeyValuePairs["Filename"].Should().Be(fileName);
        }

        [Fact]
        public void OriginalFileName()
        {
            var fileName = "FileName Value";

            NewFactory().Build(fileName).KeyValuePairs["OriginalFilename"].Should().Be(fileName);
        }

        [Fact]
        public void ValidationErrors()
        {
            NewFactory().Build(string.Empty).KeyValuePairs["ValidationErrors"].Should().Be("ValidationErrors");
        }

        [Fact]
        public void IlrDatabaseConnectionString()
        {
            var connectionString = "Connection String";

            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();

            desktopServiceSettingsMock.SetupGet(s => s.IlrDatabaseConnectionString).Returns(connectionString);

            NewFactory(desktopServiceSettings: desktopServiceSettingsMock.Object).Build(string.Empty).KeyValuePairs["IlrDatabaseConnectionString"].Should().Be(connectionString);
        }

        [Fact]
        public void KeyValuePairsCount()
        {
            NewFactory().Build(string.Empty).KeyValuePairs.Should().HaveCount(9);
        }

        private DesktopContextFactory NewFactory(IDateTimeProvider dateTimeProvider = null, IDesktopServiceSettings desktopServiceSettings = null)
        {
            return new DesktopContextFactory(
                dateTimeProvider ?? Mock.Of<IDateTimeProvider>(),
                desktopServiceSettings ?? Mock.Of<IDesktopServiceSettings>());
        }
    }
}
