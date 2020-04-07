using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class ReleaseVersionInformationServiceTests
    {
        [Fact]
        public void Date()
        {
            NewService().Date.Should().BeEquivalentTo("01/08/2020");
        }

        private ReleaseVersionInformationService NewService()
        {
            var serviceConfigurationMock = new Mock<IServiceConfiguration>();

            serviceConfigurationMock.Setup(sc => sc.Configuration.ReleaseDate).Returns("01/08/2020");

            return new ReleaseVersionInformationService(serviceConfigurationMock.Object);
        }
    }
}
