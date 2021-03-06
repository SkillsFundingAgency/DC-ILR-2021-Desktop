﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.ReportService.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class ReportFilterService : IReportFilterService
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly Func<Module> _reportModuleFunc;

        private IEnumerable<IDesktopContextReportFilterQuery> _reportFilterQueries = new List<IDesktopContextReportFilterQuery>();

        public ReportFilterService(ILifetimeScope lifetimeScope, Func<Module> reportModuleFunc)
        {
            _lifetimeScope = lifetimeScope;
            _reportModuleFunc = reportModuleFunc;
        }

        public IEnumerable<IReportFilterDefinition> GetReportFilterDefinitions()
        {
            using (var childLifetimeScope = _lifetimeScope.BeginLifetimeScope(c =>
            {
                c.RegisterModule(_reportModuleFunc());
            }))
            {
                var filteredReports = childLifetimeScope.Resolve<IEnumerable<IFilteredReport>>();

                return filteredReports?.Select(r => r.Filter) ?? Enumerable.Empty<IReportFilterDefinition>();
            }
        }

        public void SaveReportFilterQueries(IEnumerable<IDesktopContextReportFilterQuery> reportFilterQueries) => _reportFilterQueries = reportFilterQueries;

        public IEnumerable<IDesktopContextReportFilterQuery> GetReportFilterQueries() => _reportFilterQueries;
    }
}
