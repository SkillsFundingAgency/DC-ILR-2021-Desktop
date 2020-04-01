using ESFA.DC.ILR.Desktop.Service.Tasks;
using ESFA.DC.ILR.Desktop.Service.Tasks.Extensions;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ILR.Desktop.Service.Tests
{
    public class IlrDesktopTaskKeysTests
    {
        [Theory]
        [InlineData(IlrDesktopTaskKeys.PreExecution, "Pre Processing")]
        [InlineData(IlrDesktopTaskKeys.DatabaseCreate, "Build Database")]
        [InlineData(IlrDesktopTaskKeys.FileValidationService, "File Validation")]
        [InlineData(IlrDesktopTaskKeys.ReferenceDataService, "Reference Data")]
        [InlineData(IlrDesktopTaskKeys.ValidationService, "Validation")]
        [InlineData(IlrDesktopTaskKeys.FundingService, "Funding Calculation")]
        [InlineData(IlrDesktopTaskKeys.DataStore, "Store Data")]
        [InlineData(IlrDesktopTaskKeys.ReportService, "Report Generation")]
        [InlineData(IlrDesktopTaskKeys.PostExecution, "Post Processing")]
        public void DisplayText(IlrDesktopTaskKeys desktopTaskKey, string displayText)
        {
            desktopTaskKey.GetDisplayText().Should().Be(displayText);
        }
    }
}
