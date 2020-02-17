using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class FeatureSwitchServiceTests
    {
        [Fact]
        public void VersionUpdate()
        {
            NewService().VersionUpdate.Should().BeTrue();
        }

        [Fact]
        public void ReportFilters()
        {
            NewService().ReportFilters.Should().BeFalse();
        }

        private FeatureSwitchService NewService()
        {
            var featureSwitchConfigurationMock = new Mock<IFeatureSwitchConfiguration>();

            featureSwitchConfigurationMock.Setup(sc => sc.Configuration.VersionUpdate).Returns(true);
            featureSwitchConfigurationMock.Setup(sc => sc.Configuration.ReportFilters).Returns(false);

            return new FeatureSwitchService(featureSwitchConfigurationMock.Object);
        }
    }
}
