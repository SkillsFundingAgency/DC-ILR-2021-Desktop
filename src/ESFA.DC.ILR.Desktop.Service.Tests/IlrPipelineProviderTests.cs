using System.Linq;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class IlrPipelineProviderTests
    {
        [Fact]
        public void Provide()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.Setup(dssm => dssm.ExportToSql).Returns(true);

            var pipeline = NewProvider(desktopServiceSettingsMock.Object).Provide().ToList();

            pipeline.Should().HaveCount(9);

            pipeline[0].Key.Should().Be(IlrDesktopTaskKeys.PreExecution);
            pipeline[0].FailureKey.Should().BeNull();

            pipeline[1].Key.Should().Be(IlrDesktopTaskKeys.DatabaseCreate);
            pipeline[1].FailureKey.Should().BeNull();

            pipeline[2].Key.Should().Be(IlrDesktopTaskKeys.FileValidationService);
            pipeline[2].FailureKey.Should().Be(IlrDesktopTaskKeys.ReportService);

            pipeline[3].Key.Should().Be(IlrDesktopTaskKeys.ReferenceDataService);
            pipeline[3].FailureKey.Should().BeNull();

            pipeline[4].Key.Should().Be(IlrDesktopTaskKeys.ValidationService);
            pipeline[4].FailureKey.Should().BeNull();

            pipeline[5].Key.Should().Be(IlrDesktopTaskKeys.FundingService);
            pipeline[5].FailureKey.Should().BeNull();

            pipeline[6].Key.Should().Be(IlrDesktopTaskKeys.DataStore);
            pipeline[6].FailureKey.Should().BeNull();

            pipeline[7].Key.Should().Be(IlrDesktopTaskKeys.ReportService);
            pipeline[7].FailureKey.Should().BeNull();

            pipeline[8].Key.Should().Be(IlrDesktopTaskKeys.PostExecution);
            pipeline[8].FailureKey.Should().BeNull();
        }

        [Fact]
        public void ProvideNoSql()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.Setup(dssm => dssm.ExportToSql).Returns(false);

            var pipeline = NewProvider(desktopServiceSettingsMock.Object).Provide().ToList();

            pipeline.Should().HaveCount(7);

            pipeline[0].Key.Should().Be(IlrDesktopTaskKeys.PreExecution);
            pipeline[0].FailureKey.Should().BeNull();

            pipeline[1].Key.Should().Be(IlrDesktopTaskKeys.FileValidationService);
            pipeline[1].FailureKey.Should().Be(IlrDesktopTaskKeys.ReportService);

            pipeline[2].Key.Should().Be(IlrDesktopTaskKeys.ReferenceDataService);
            pipeline[2].FailureKey.Should().BeNull();

            pipeline[3].Key.Should().Be(IlrDesktopTaskKeys.ValidationService);
            pipeline[3].FailureKey.Should().BeNull();

            pipeline[4].Key.Should().Be(IlrDesktopTaskKeys.FundingService);
            pipeline[4].FailureKey.Should().BeNull();

            pipeline[5].Key.Should().Be(IlrDesktopTaskKeys.ReportService);
            pipeline[5].FailureKey.Should().BeNull();

            pipeline[6].Key.Should().Be(IlrDesktopTaskKeys.PostExecution);
            pipeline[6].FailureKey.Should().BeNull();
        }

        [Theory]
        [InlineData(IlrDesktopTaskKeys.PreExecution, 0)]
        [InlineData(IlrDesktopTaskKeys.DatabaseCreate, 1)]
        [InlineData(IlrDesktopTaskKeys.FileValidationService, 2)]
        [InlineData(IlrDesktopTaskKeys.ReferenceDataService, 3)]
        [InlineData(IlrDesktopTaskKeys.ValidationService, 4)]
        [InlineData(IlrDesktopTaskKeys.FundingService, 5)]
        [InlineData(IlrDesktopTaskKeys.DataStore, 6)]
        [InlineData(IlrDesktopTaskKeys.ReportService, 7)]
        [InlineData(IlrDesktopTaskKeys.PostExecution, 8)]
        public void IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey, int index)
        {
            NewProvider().IndexFor(ilrDesktopTaskKey).Should().Be(index);
        }

        private IlrPipelineProvider NewProvider(IDesktopServiceSettings desktopServiceSettings = null)
        {
            return new IlrPipelineProvider(desktopServiceSettings);
        }
    }
}
