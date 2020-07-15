using System;
using System.Collections.Generic;
using System.IO;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContext : IDesktopContext
    {
        private string Reports = string.Join(
            "|",
            ReportTaskNameConstants.ValidationReport,
            ReportTaskNameConstants.RuleViolationSummaryReport,
            ReportTaskNameConstants.FundingSummaryReport,
            ReportTaskNameConstants.MainOccupancyReport,
            ReportTaskNameConstants.AllbOccupancyReport,
            ReportTaskNameConstants.DevolvedAdultEducationFundingSummaryReport,
            ReportTaskNameConstants.DevolvedAdultEducationOccupancyReport,
            ReportTaskNameConstants.TrailblazerAppsOccupancyReport,
            ReportTaskNameConstants.TrailblazerEmployerIncentivesReport,
            ReportTaskNameConstants.AppsIndicativeEarningsReport,
            ReportTaskNameConstants.SummaryOfFundingByStudentReport,
            ReportTaskNameConstants.MathsAndEnglishReport,
            ReportTaskNameConstants.HNSReport,
            ReportTaskNameConstants.SummaryOfFm35FundingReport,
            ReportTaskNameConstants.CommunityLearningReport,
            ReportTaskNameConstants.HNSSummaryReport,
            ReportTaskNameConstants.FundingClaim1619Report,
            ReportTaskNameConstants.AdultFundingClaimReport);
            //ReportTaskNameConstants.AebStfiInitiativesOccupancyReport,
            //ReportTaskNameConstants.AebStfiInitiativesFundingSummaryReport);

        private string DataStore = string.Join(
            "|",
            DataStoreTaskNameConstants.TaskStoreALBTables,
            DataStoreTaskNameConstants.TaskStoreFM25Tables,
            DataStoreTaskNameConstants.TaskStoreFM35Tables,
            DataStoreTaskNameConstants.TaskStoreFM36Tables,
            DataStoreTaskNameConstants.TaskStoreFM70Tables,
            DataStoreTaskNameConstants.TaskStoreFM81Tables,
            DataStoreTaskNameConstants.TaskStoreInvalidTables,
            DataStoreTaskNameConstants.TaskStoreValidTables,
            DataStoreTaskNameConstants.TaskStoreValidationTables);

        private string MdbExport = string.Join(
            "|",
            ExportTaskNameConstants.TaskExportAlbTables,
            ExportTaskNameConstants.TaskExportDVTables,
            ExportTaskNameConstants.TaskExportEsfTables,
            ExportTaskNameConstants.TaskExportFM25FM35Tables,
            ExportTaskNameConstants.TaskExportFM25Tables,
            ExportTaskNameConstants.TaskExportFM35Tables,
            ExportTaskNameConstants.TaskExportFM36Tables,
            ExportTaskNameConstants.TaskExportInvalidTables,
            ExportTaskNameConstants.TaskExportTblTables,
            ExportTaskNameConstants.TaskExportValidTables,
            ExportTaskNameConstants.TaskExportValidationErrorTable);

        public DesktopContext(
            DateTime dateTime,
            string outputDirectory,
            string filePath,
            string executingAssemblyPath,
            string referenceDataFile,
            string connectionString,
            string releaseVersion,
            IEnumerable<IDesktopContextReportFilterQuery> reportFilterQueries)
        {
            DateTimeUtc = dateTime;
            OutputDirectory = outputDirectory;
            KeyValuePairs = new Dictionary<string, object>()
            {
                [ILRContextKeys.Container] = "Sandbox",
                [ILRContextKeys.Filename] = filePath,
                [ILRContextKeys.OriginalFilename] = filePath,
                [ILRContextKeys.Ukprn] = 12345678,
                [ILRContextKeys.CollectionYear] = "2021",
                [ILRContextKeys.ReturnPeriod] = 12,
                [ILRContextKeys.ValidationErrors] = "ValidationErrors.json",
                [ILRContextKeys.IlrDatabaseConnectionString] = connectionString,
                [ILRContextKeys.IlrReferenceData] = "IlrReferenceData.json",
                [ILRContextKeys.InvalidLearnRefNumbers] = "InvalidLearnRefNumbers.json",
                [ILRContextKeys.ValidLearnRefNumbers] = "ValidLearnRefNumbers.json",
                [ILRContextKeys.ValidationErrorLookups] = "ValidationErrorLookups.json",
                [ILRContextKeys.ReportOutputFileNames] = string.Empty,
                [ILRContextKeys.ReportTasks] = Reports,
                [ILRContextKeys.DataStoreTasks] = DataStore,
                [ILRContextKeys.ExportTasks] = MdbExport,
                [ILRContextKeys.ReferenceDataFilename] = Path.Combine(executingAssemblyPath, referenceDataFile),

                [ILRContextKeys.FundingTaskALB] = "ALB",
                [ILRContextKeys.FundingTaskFM25] = "FM25",
                [ILRContextKeys.FundingTaskFM35] = "FM35",
                [ILRContextKeys.FundingTaskFM36] = "FM36",
                [ILRContextKeys.FundingTaskFM70] = "FM70",
                [ILRContextKeys.FundingTaskFM81] = "FM81",
                [ILRContextKeys.FundingAlbOutput] = "FundingAlbOutput.json",
                [ILRContextKeys.FundingFm25Output] = "FundingFm25Output.json",
                [ILRContextKeys.FundingFm35Output] = "FundingFm35Output.json",
                [ILRContextKeys.FundingFm36Output] = "FundingFm36Output.json",
                [ILRContextKeys.FundingFm70Output] = "FundingFm70Output.json",
                [ILRContextKeys.FundingFm81Output] = "FundingFm81Output.json",
                [ILRContextKeys.ServiceReleaseVersion] = releaseVersion,
            };
            ReportFilterQueries = reportFilterQueries;
        }

        public DateTime DateTimeUtc { get; set; }

        public string OutputDirectory { get; set; }

        public IDictionary<string, object> KeyValuePairs { get; set; }

        public IEnumerable<IDesktopContextReportFilterQuery> ReportFilterQueries { get; set; }
    }
}
