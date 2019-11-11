using System.Collections.Generic;
using System.Configuration;
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
        public void Provide_Exports()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.Setup(dssm => dssm.ExportToSql).Returns(true);
            desktopServiceSettingsMock.Setup(dssm => dssm.ExportToAccessAndCsv).Returns(true);

            var pipeline = NewProvider(desktopServiceSettingsMock.Object).Provide().ToList();

            pipeline.Should().HaveCount(12);

            pipeline[0].Key.Should().Be(IlrDesktopTaskKeys.PreExecution);
            pipeline[0].FailureKey.Should().BeNull();

            pipeline[1].Key.Should().Be(IlrDesktopTaskKeys.DatabaseCreate);
            pipeline[1].FailureKey.Should().BeNull();

            pipeline[2].Key.Should().Be(IlrDesktopTaskKeys.MdbCreate);
            pipeline[2].FailureKey.Should().BeNull();

            pipeline[3].Key.Should().Be(IlrDesktopTaskKeys.FileValidationService);
            pipeline[3].FailureKey.Should().Be(IlrDesktopTaskKeys.ReportService);

            pipeline[4].Key.Should().Be(IlrDesktopTaskKeys.ReferenceDataService);
            pipeline[4].FailureKey.Should().BeNull();

            pipeline[5].Key.Should().Be(IlrDesktopTaskKeys.ValidationService);
            pipeline[5].FailureKey.Should().BeNull();

            pipeline[6].Key.Should().Be(IlrDesktopTaskKeys.FundingService);
            pipeline[6].FailureKey.Should().BeNull();

            pipeline[7].Key.Should().Be(IlrDesktopTaskKeys.DataStore);
            pipeline[7].FailureKey.Should().BeNull();

            pipeline[8].Key.Should().Be(IlrDesktopTaskKeys.MdbExport);
            pipeline[8].FailureKey.Should().BeNull();

            pipeline[9].Key.Should().Be(IlrDesktopTaskKeys.ReportService);
            pipeline[9].FailureKey.Should().BeNull();

            pipeline[10].Key.Should().Be(IlrDesktopTaskKeys.PostExecution);
            pipeline[10].FailureKey.Should().BeNull();

            pipeline[11].Key.Should().Be(IlrDesktopTaskKeys.MdbPublish);
            pipeline[11].FailureKey.Should().BeNull();
        }

        [Fact]
        public void Provide_Default()
        {
            var desktopServiceSettingsMock = new Mock<IDesktopServiceSettings>();
            desktopServiceSettingsMock.Setup(dssm => dssm.ExportToSql).Returns(false);
            desktopServiceSettingsMock.Setup(dss => dss.ExportToAccessAndCsv).Returns(false);

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
        [InlineData(IlrDesktopTaskKeys.FundingService, 1)]
        [InlineData(IlrDesktopTaskKeys.MdbCreate, 2)]
        [InlineData(IlrDesktopTaskKeys.DatabaseCreate, -1)]
        public void IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey, int index)
        {
            var taskList = new List<IIlrDesktopTaskDefinition>()
            {
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PreExecution),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FundingService),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbCreate),
            };

            NewProvider().IndexFor(ilrDesktopTaskKey, taskList).Should().Be(index);
        }

        private IlrPipelineProvider NewProvider(IDesktopServiceSettings desktopServiceSettings = null)
        {
            return new IlrPipelineProvider(desktopServiceSettings);
        }
    }
}
