using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests.Tasks
{
    public class PostProcessingDesktopTaskContextTests
    {
        [Fact]
        public void Container()
        {
            var container = "ContainerName";

            var keyValuePairs = new Dictionary<string, object>()
            {
                ["Container"] = container,
            };

            var desktopContextMock = new Mock<IDesktopContext>();

            desktopContextMock.SetupGet(c => c.KeyValuePairs).Returns(keyValuePairs);

            NewContext(desktopContextMock.Object).Container.Should().Be(container);
        }

        [Fact]
        public void OutputDirectory()
        {
            var outputDirectory = "OutputDirectoryName";

            var desktopContextMock = new Mock<IDesktopContext>();

            desktopContextMock.SetupGet(c => c.OutputDirectory).Returns(outputDirectory);

            NewContext(desktopContextMock.Object).OutputDirectory.Should().Be(outputDirectory);
        }

        [Fact]
        public void OriginalFilename()
        {
            var originalFilename = "OriginalFilenameName";

            var keyValuePairs = new Dictionary<string, object>()
            {
                ["OriginalFilename"] = originalFilename,
            };

            var desktopContextMock = new Mock<IDesktopContext>();

            desktopContextMock.SetupGet(c => c.KeyValuePairs).Returns(keyValuePairs);

            NewContext(desktopContextMock.Object).OriginalFilename.Should().Be(originalFilename);
        }

        [Fact]
        public void ReportFileNames_One()
        {
            var reportOne = "ReportOne";

            var reportFileNames = reportOne;

            var keyValuePairs = new Dictionary<string, object>()
            {
                ["ReportOutputFileNames"] = reportFileNames,
            };

            var desktopContextMock = new Mock<IDesktopContext>();

            desktopContextMock.SetupGet(c => c.KeyValuePairs).Returns(keyValuePairs);

            var result = NewContext(desktopContextMock.Object).ReportFileNames.ToList();

            result.Should().HaveCount(1);
            result[0].Should().Be(reportOne);
        }

        [Fact]
        public void ReportFileNames_Empty()
        {
            var reportFileNames = string.Empty;

            var keyValuePairs = new Dictionary<string, object>()
            {
                ["ReportOutputFileNames"] = reportFileNames,
            };

            var desktopContextMock = new Mock<IDesktopContext>();

            desktopContextMock.SetupGet(c => c.KeyValuePairs).Returns(keyValuePairs);

            var result = NewContext(desktopContextMock.Object).ReportFileNames.ToList();

            result.Should().BeEmpty();
        }

        [Fact]
        public void ReportFileNames_Multiple()
        {
            var reportOne = "ReportOne";
            var reportTwo = "ReportTwo";
            var reportThree = "ReportThree";

            var reportFileNames = reportOne + "|" + reportTwo + "|" + reportThree;

            var keyValuePairs = new Dictionary<string, object>()
            {
                ["ReportOutputFileNames"] = reportFileNames,
            };

            var desktopContextMock = new Mock<IDesktopContext>();

            desktopContextMock.SetupGet(c => c.KeyValuePairs).Returns(keyValuePairs);

            var result = NewContext(desktopContextMock.Object).ReportFileNames.ToList();

            result.Should().HaveCount(3);
            result[0].Should().Be(reportOne);
            result[1].Should().Be(reportTwo);
            result[2].Should().Be(reportThree);
        }

        private PostProcessingDesktopTaskContext NewContext(IDesktopContext desktopContext)
        {
            return new PostProcessingDesktopTaskContext(desktopContext);
        }
    }
}
