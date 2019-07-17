﻿using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContext : IDesktopContext
    {
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
                [ILRContextKeys.ValidationErrors] = "ValidationErrors.json",
                [ILRContextKeys.IlrDatabaseConnectionString] = connectionString,
                [ILRContextKeys.IlrReferenceData] = "IlrReferenceData.json",
                [ILRContextKeys.InvalidLearnRefNumbers] = "InvalidLearnRefNumbers.json",
                [ILRContextKeys.ValidLearnRefNumbers] = "ValidLearnRefNumbers.json",
                [ILRContextKeys.ValidationErrorLookups] = "ValidationErrorLookups.json",
                [ILRContextKeys.ReportOutputFileNames] = string.Empty,
                [ILRContextKeys.ReportTasks] = ReportTaskNameConstants.ValidationReport,

                [ILRContextKeys.FundingTaskALB] = "ALB",
                [ILRContextKeys.FundingTaskFM35] = "FM35",
                [ILRContextKeys.FundingTaskFM36] = "FM36",
                [ILRContextKeys.FundingAlbOutput] = "FundingOutputALB.json",
                [ILRContextKeys.FundingFm35Output] = "FundingOutputFM35.json",
                [ILRContextKeys.FundingFm36Output] = "FundingOutputFM36.json",
            };
        }

        public DateTime DateTimeUtc { get; set; }

        public string OutputDirectory { get; set; }

        public IDictionary<string, object> KeyValuePairs { get; set; }
    }
}
