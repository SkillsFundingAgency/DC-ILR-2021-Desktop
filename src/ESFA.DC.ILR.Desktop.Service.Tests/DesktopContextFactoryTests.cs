using System;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
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

            NewFactory(dateTimeProviderMock.Object).Build("FileName").DateTimeUtc.Should().Be(dateTime);
        }

        private DesktopContextFactory NewFactory(IDateTimeProvider dateTimeProvider = null)
        {
            return new DesktopContextFactory(dateTimeProvider ?? Mock.Of<IDateTimeProvider>());
        }
    }
}
