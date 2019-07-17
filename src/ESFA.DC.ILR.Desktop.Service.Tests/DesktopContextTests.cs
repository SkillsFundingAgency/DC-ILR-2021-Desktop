using System;
using ESFA.DC.ILR.Desktop.Service.Context;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class DesktopContextTests
    {
        [Fact]
        public void DateTimeUtc()
        {
            var dateTime = new DateTime(2019, 6, 1);

            NewContext(dateTime).DateTimeUtc.Should().Be(dateTime);
        }

        [Fact]
        public void OutputDirectory()
        {
            var outputDirectory = "Output Directory";

            NewContext(outputDirectory: outputDirectory).OutputDirectory.Should().Be(outputDirectory);
        }

        [Fact]
        public void Container()
        {
            NewContext().KeyValuePairs["Container"].Should().Be("Sandbox");
        }

        [Fact]
        public void Filename()
        {
            var fileName = "FileName Value";

            NewContext(filePath: fileName).KeyValuePairs["Filename"].Should().Be(fileName);
        }

        [Fact]
        public void OriginalFileName()
        {
            var fileName = "FileName Value";

            NewContext(filePath: fileName).KeyValuePairs["OriginalFilename"].Should().Be(fileName);
        }

        [Fact]
        public void ValidationErrors()
        {
            NewContext().KeyValuePairs["ValidationErrors"].Should().Be("ValidationErrors.json");
        }

        [Fact]
        public void InvalidLearnRefNumbers()
        {
            NewContext().KeyValuePairs["InvalidLearnRefNumbers"].Should().Be("InvalidLearnRefNumbers.json");
        }

        [Fact]
        public void ValidLearnRefNumbers()
        {
            NewContext().KeyValuePairs["ValidLearnRefNumbers"].Should().Be("ValidLearnRefNumbers.json");
        }

        [Fact]
        public void ValidationErrorLookups()
        {
            NewContext().KeyValuePairs["ValidationErrorLookups"].Should().Be("ValidationErrorLookups.json");
        }

        [Fact]
        public void ReportOutputFileNames()
        {
            NewContext().KeyValuePairs["ReportOutputFileNames"].Should().Be(string.Empty);
        }

        [Fact]
        public void IlrDatabaseConnectionString()
        {
            var connectionString = "Connection String";

            NewContext(connectionString: connectionString).KeyValuePairs["IlrDatabaseConnectionString"].Should().Be(connectionString);
        }

        [Fact]
        public void Ukprn()
        {
            NewContext().KeyValuePairs["UkPrn"].Should().Be(12345678);
        }

        [Fact]
        public void KeyValuePairsCount()
        {
            NewContext().KeyValuePairs.Should().HaveCount(18);
        }

        private DesktopContext NewContext(DateTime? dateTime = null, string outputDirectory = null, string filePath = null, string connectionString = null)
        {
            return new DesktopContext(
                dateTime.HasValue ? dateTime.Value : new DateTime(2018, 1, 1),
                outputDirectory,
                filePath,
                connectionString);
        }
    }
}
