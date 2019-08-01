using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContext : IDesktopContext
    {
        private string Reports = string.Join(
            "|",
            ReportTaskNameConstants.ValidationReport,
            ReportTaskNameConstants.FundingSummaryReport,
            ReportTaskNameConstants.DevolvedAdultEducationOccupancyReport);

        public DesktopContext(
            DateTime dateTime,
            string outputDirectory,
            string filePath,
            string connectionString)
        {
            DateTimeUtc = dateTime;
            OutputDirectory = outputDirectory;
            KeyValuePairs = new Dictionary<string, object>()
            {
                [ILRContextKeys.Container] = "Sandbox",
                [ILRContextKeys.Filename] = filePath,
                [ILRContextKeys.OriginalFilename] = filePath,
                [ILRContextKeys.Ukprn] = 12345678,
                [ILRContextKeys.CollectionYear] = "1920",
                [ILRContextKeys.ReturnPeriod] = 12,
                [ILRContextKeys.ValidationErrors] = "ValidationErrors.json",
                [ILRContextKeys.IlrDatabaseConnectionString] = connectionString,
                [ILRContextKeys.IlrReferenceData] = "IlrReferenceData.json",
                [ILRContextKeys.InvalidLearnRefNumbers] = "InvalidLearnRefNumbers.json",
                [ILRContextKeys.ValidLearnRefNumbers] = "ValidLearnRefNumbers.json",
                [ILRContextKeys.ValidationErrorLookups] = "ValidationErrorLookups.json",
                [ILRContextKeys.ReportOutputFileNames] = string.Empty,
                [ILRContextKeys.ReportTasks] = Reports,
                [ILRContextKeys.ReturnPeriod] = 12,

                [ILRContextKeys.FundingTaskALB] = "ALB",
                [ILRContextKeys.FundingTaskFM25] = "FM25",
                [ILRContextKeys.FundingTaskFM35] = "FM35",
                [ILRContextKeys.FundingTaskFM36] = "FM36",
                [ILRContextKeys.FundingTaskFM70] = "FM70",
                [ILRContextKeys.FundingTaskFM81] = "FM81",
                [ILRContextKeys.FundingAlbOutput] = "FundingOutputALB.json",
                [ILRContextKeys.FundingFm25Output] = "FundingOutputFM25.json",
                [ILRContextKeys.FundingFm35Output] = "FundingOutputFM35.json",
                [ILRContextKeys.FundingFm36Output] = "FundingOutputFM36.json",
                [ILRContextKeys.FundingFm70Output] = "FundingOutputFM70.json",
                [ILRContextKeys.FundingFm81Output] = "FundingOutputFM81.json"
            };
        }

        public DateTime DateTimeUtc { get; set; }

        public string OutputDirectory { get; set; }

        public IDictionary<string, object> KeyValuePairs { get; set; }
    }
}
