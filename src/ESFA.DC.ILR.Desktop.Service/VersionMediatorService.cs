using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class VersionMediatorService : IVersionMediatorService
    {
        private readonly IVersionFactory _versionFactory;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;
        private readonly IVersionService _versionService;
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly ILogger _logger;

        public VersionMediatorService(
            IVersionFactory versionFactory,
            IReleaseVersionInformationService releaseVersionInformationService,
            IVersionService versionService,
            IDesktopServiceSettings desktopServiceSettings,
            ILogger logger)
        {
            _versionFactory = versionFactory;
            _releaseVersionInformationService = releaseVersionInformationService;
            _versionService = versionService;
            _desktopServiceSettings = desktopServiceSettings;
            _logger = logger;
        }

        public async Task<ApplicationVersionResult> GetNewVersion()
        {
            _logger.LogInfo("Retrieving latest known Application Version.");
            var result = await _versionService.GetLatestApplicationVersion(GetCurrentApplicationVersion());

            _logger.LogInfo("Finished Retrieving latest known Application Version.");
            return result;
        }

        protected Version GetCurrentApplicationVersion()
        {
            var version = _releaseVersionInformationService.VersionNumber;

            return _versionFactory.GetVersion(version, _desktopServiceSettings.ReferenceDataVersion);
        }
    }
}