using System.Collections.Generic;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.ReportService.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDesktopServiceSettings _desktopServiceSettings;

        public DesktopContextFactory(IDateTimeProvider dateTimeProvider, IDesktopServiceSettings desktopServiceSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _desktopServiceSettings = desktopServiceSettings;
        }

        public IDesktopContext Build(string filePath)
        {
            return new DesktopContext()
            {
                DateTimeUtc = _dateTimeProvider.GetNowUtc(),
                OutputDirectory = _desktopServiceSettings.OutputDirectory,
                KeyValuePairs = new Dictionary<string, object>()
                {
                    [ILRContextKeys.Container] = "Sandbox",
                    [ILRContextKeys.Filename] = filePath,
                    [ILRContextKeys.OriginalFilename] = filePath,
                    [ILRContextKeys.ValidationErrors] = "ValidationErrors.json",
                    [ILRContextKeys.IlrDatabaseConnectionString] = _desktopServiceSettings.IlrDatabaseConnectionString,
                    [ILRContextKeys.IlrReferenceData] = "IlrReferenceData.json",
                    [ILRContextKeys.InvalidLearnRefNumbers] = "InvalidLearnRefNumbers.json",
                    [ILRContextKeys.ValidLearnRefNumbers] = "ValidLearnRefNumbers.json",
                    [ILRContextKeys.ValidationErrorLookups] = "ValidationErrorLookups.json",
                    [ILRContextKeys.ReportOutputFileNames] = string.Empty,
                    [ILRContextKeys.ReportTasks] = ReportTaskNameConstants.ValidationReport,
                },
            };
        }
    }
}