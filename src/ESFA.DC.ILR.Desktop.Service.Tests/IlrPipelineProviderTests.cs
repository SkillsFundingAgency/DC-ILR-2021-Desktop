using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class IlrPipelineProviderTests
    {
        [Fact]
        public void Provide()
        {
            var pipeline = NewProvider().Provide().ToList();

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

        private IlrPipelineProvider NewProvider()
        {
            return new IlrPipelineProvider();
        }
    }
}
