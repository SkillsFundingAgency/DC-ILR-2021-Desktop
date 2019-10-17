using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.ReferenceData;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using IDesktopContextFactory = ESFA.DC.ILR.Desktop.WPF.Service.Interface.IDesktopContextFactory;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IReportFilterService _reportFilterService;
        private readonly IAssemblyService _assemblyService;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;

        public DesktopContextFactory(IDateTimeProvider dateTimeProvider, IDesktopServiceSettings desktopServiceSettings, IReportFilterService reportFilterService, IAssemblyService assemblyService, IReleaseVersionInformationService releaseVersionInformationService)
        {
            _dateTimeProvider = dateTimeProvider;
            _desktopServiceSettings = desktopServiceSettings;
            _reportFilterService = reportFilterService;
            _assemblyService = assemblyService;
            _releaseVersionInformationService = releaseVersionInformationService;
        }

        public IDesktopContext Build(string filePath)
        {
            return new DesktopContext(
                _dateTimeProvider.GetNowUtc(),
                _desktopServiceSettings.OutputDirectory,
                filePath,
                _assemblyService.GetExecutingAssemblyPath(),
                ReferenceDataConstants.FilePath,
                _desktopServiceSettings.IlrDatabaseConnectionString,
                _releaseVersionInformationService.VersionNumber,
                _reportFilterService.GetReportFilterQueries());
        }
    }
}