using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ESFA.DC.ILR.Desktop.Interface;
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
        public void CollectionYear()
        {
            NewContext().KeyValuePairs["CollectionYear"].Should().Be("1920");
        }

        [Fact]
        public void ReturnPeriod()
        {
            NewContext().KeyValuePairs["ReturnPeriod"].Should().Be(12);
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
        public void FundingTaskALB()
        {
            NewContext().KeyValuePairs["ALB"].Should().Be("ALB");
        }

        [Fact]
        public void FundingTaskFM25()
        {
            NewContext().KeyValuePairs["FM25"].Should().Be("FM25");
        }

        [Fact]
        public void FundingTaskFM35()
        {
            NewContext().KeyValuePairs["FM35"].Should().Be("FM35");
        }

        [Fact]
        public void FundingTaskFM36()
        {
            NewContext().KeyValuePairs["FM36"].Should().Be("FM36");
        }

        [Fact]
        public void FundingTaskFM70()
        {
            NewContext().KeyValuePairs["FM70"].Should().Be("FM70");
        }

        [Fact]
        public void FundingTaskFM81()
        {
            NewContext().KeyValuePairs["FM81"].Should().Be("FM81");
        }

        [Fact]
        public void FundingAlbOutput()
        {
            NewContext().KeyValuePairs["FundingAlbOutput"].Should().Be("FundingAlbOutput.json");
        }

        [Fact]
        public void FundingFM25Output()
        {
            NewContext().KeyValuePairs["FundingFm25Output"].Should().Be("FundingFm25Output.json");
        }

        [Fact]
        public void FundingFM35Output()
        {
            NewContext().KeyValuePairs["FundingFm35Output"].Should().Be("FundingFm35Output.json");
        }

        [Fact]
        public void FundingFM36Output()
        {
            NewContext().KeyValuePairs["FundingFm36Output"].Should().Be("FundingFm36Output.json");
        }

        [Fact]
        public void FundingFM70Output()
        {
            NewContext().KeyValuePairs["FundingFm70Output"].Should().Be("FundingFm70Output.json");
        }

        [Fact]
        public void FundingFM81Output()
        {
            NewContext().KeyValuePairs["FundingFm81Output"].Should().Be("FundingFm81Output.json");
        }

        [Fact]
        public void ReferenceDataFilename()
        {
            var referenceDataFileName = "FISReferenceData.zip";
            var referenceDataFilePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{referenceDataFileName}";

            NewContext(referenceDataFileName: referenceDataFileName).KeyValuePairs["ReferenceDataFilename"].Should().Be(referenceDataFilePath);
        }

        [Fact]
        public void KeyValuePairsCount()
        {
            NewContext().KeyValuePairs.Should().HaveCount(27);
        }

        [Fact]
        public void ReportFilterQueries()
        {
            var reportFilterQueries = new List<IDesktopContextReportFilterQuery>();

            NewContext(reportFilterQueries: reportFilterQueries).ReportFilterQueries.Should().BeSameAs(reportFilterQueries);
        }

        private DesktopContext NewContext(
            DateTime? dateTime = null,
            string outputDirectory = null,
            string filePath = null,
            string referenceDataFileName = null,
            string connectionString = null,
            IEnumerable<IDesktopContextReportFilterQuery> reportFilterQueries = null)
        {
            return new DesktopContext(
                dateTime.HasValue ? dateTime.Value : new DateTime(2018, 1, 1),
                outputDirectory,
                filePath,
                referenceDataFileName,
                connectionString,
                reportFilterQueries);
        }
    }
}
