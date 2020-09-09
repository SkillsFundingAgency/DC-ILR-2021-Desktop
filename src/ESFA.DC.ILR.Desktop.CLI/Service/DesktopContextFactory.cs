using System.Linq;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Context;
using ESFA.DC.ILR.Desktop.Service.ReferenceData;

namespace ESFA.DC.ILR.Desktop.CLI.Service
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;
        private readonly IAssemblyService _assemblyService;

        public DesktopContextFactory(IDesktopServiceSettings desktopServiceSettings, IDateTimeProvider dateTimeProvider, IReleaseVersionInformationService releaseVersionInformationService, IAssemblyService assemblyService)
        {
            _desktopServiceSettings = desktopServiceSettings;
            _dateTimeProvider = dateTimeProvider;
            _assemblyService = assemblyService;
            _releaseVersionInformationService = releaseVersionInformationService;
        }

        public IDesktopContext Build(ICommandLineArguments commandLineArguments)
        {
            return new DesktopContext(
                _dateTimeProvider.GetNowUtc(),
                OverrideConfig(commandLineArguments.OutputDirectory, _desktopServiceSettings.OutputDirectory),
                commandLineArguments.FilePath,
                _assemblyService.GetExecutingAssemblyPath(),
                string.Concat(ReferenceDataConstants.FilePath, _desktopServiceSettings.ReferenceDataVersion, ReferenceDataConstants.FileExtension),
                OverrideConfig(commandLineArguments.ConnectionString, _desktopServiceSettings.IlrDatabaseConnectionString),
                _releaseVersionInformationService.VersionNumber,
                Enumerable.Empty<IDesktopContextReportFilterQuery>());
        }

        public string OverrideConfig(string commandLine, string config)
        {
            return !string.IsNullOrWhiteSpace(commandLine) ? commandLine : config;
        }
    }
}
