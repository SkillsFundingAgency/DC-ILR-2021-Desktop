using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Service.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class ReferenceDataVersionInformationServiceTests
    {
        [Fact]
        public void Date()
        {
            var date = "01/08/2019";

            var serviceConfigurationMock = new Mock<IServiceConfiguration>();
            serviceConfigurationMock.Setup(sc => sc.Configuration.ReferenceDataDate).Returns(date);

            NewService(serviceConfigurationMock.Object).Date.Should().BeEquivalentTo(date);
        }

        [Fact]
        public void VersionNumber()
        {
            var versionNumber = "1.0.0.0";

            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.Setup(sc => sc.ReferenceDataVersion).Returns(versionNumber);

            NewService(desktopServiceSettings: desktopServiceSettingsMock.Object).VersionNumber.Should().BeEquivalentTo(versionNumber);
        }

        private ReferenceDataVersionInformationService NewService(IServiceConfiguration serviceConfiguration = null, IDesktopServiceSettings desktopServiceSettings = null)
        {
            return new ReferenceDataVersionInformationService(serviceConfiguration, desktopServiceSettings);
        }
    }
}
