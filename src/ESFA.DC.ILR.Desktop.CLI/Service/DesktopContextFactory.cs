using System;
using System.Linq;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.ReferenceData;

namespace ESFA.DC.ILR.Desktop.CLI.Service
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DesktopContextFactory(IDesktopServiceSettings desktopServiceSettings, IDateTimeProvider dateTimeProvider)
        {
            _desktopServiceSettings = desktopServiceSettings;
            _dateTimeProvider = dateTimeProvider;
        }

        public IDesktopContext Build(ICommandLineArguments commandLineArguments)
        {
            return new DesktopContext(
                _dateTimeProvider.GetNowUtc(),
                OverrideConfig(commandLineArguments.OutputDirectory, _desktopServiceSettings.OutputDirectory),
                commandLineArguments.FilePath,
                ReferenceDataConstants.FilePath,
                OverrideConfig(commandLineArguments.ConnectionString, _desktopServiceSettings.IlrDatabaseConnectionString),
                Enumerable.Empty<IReportFilterQuery>());
        }

        public string OverrideConfig(string commandLine, string config)
        {
            return !string.IsNullOrWhiteSpace(commandLine) ? commandLine : config;
        }
    }
}
