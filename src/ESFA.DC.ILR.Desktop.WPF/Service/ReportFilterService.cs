using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Stub;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class ReportFilterService : IReportFilterService
    {
        private IEnumerable<IReportFilterQuery> _reportFilterQueries = new List<IReportFilterQuery>();

        public IEnumerable<IReportFilterDefinition> GetReportFilterDefinitions()
        {
            return new List<IReportFilterDefinition>()
            {
                new ReportFilterDefinition()
                {
                    ReportName = "Report One",
                    Properties = new List<IReportFilterPropertyDefinition>()
                    {
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property One",
                            Type = typeof(DateTime?).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Two",
                            Type = typeof(string).FullName,
                        },
                    },
                },
                new ReportFilterDefinition()
                {
                    ReportName = "Report Two",
                    Properties = new List<IReportFilterPropertyDefinition>()
                    {
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property One",
                            Type = typeof(DateTime?).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Two",
                            Type = typeof(string).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Three",
                            Type = typeof(string).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Four",
                            Type = typeof(string).FullName,
                        },
                    },
                },
                new ReportFilterDefinition()
                {
                    ReportName = "Report Three",
                    Properties = new List<IReportFilterPropertyDefinition>()
                    {
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property One",
                            Type = typeof(DateTime?).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Two",
                            Type = typeof(string).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Three",
                            Type = typeof(DateTime?).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Four",
                            Type = typeof(DateTime?).FullName,
                        },
                        new ReportFilterPropertyDefinition()
                        {
                            Name = "Property Five",
                            Type = typeof(string).FullName,
                        },
                    },
                },
            };
        }

        public void SaveReportFilterQueries(IEnumerable<IReportFilterQuery> reportFilterQueries) => _reportFilterQueries = reportFilterQueries;

        public IEnumerable<IReportFilterQuery> GetReportFilterQueries() => _reportFilterQueries;
    }
}
