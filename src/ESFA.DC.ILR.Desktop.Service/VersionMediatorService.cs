using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class VersionMediatorService : IVersionMediatorService
    {
        private readonly IVersionFactory _versionFactory;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;
        private readonly IVersionService _versionService;
        private readonly IDesktopServiceSettings _desktopServiceSettings;

        public VersionMediatorService(
            IVersionFactory versionFactory,
            IReleaseVersionInformationService releaseVersionInformationService,
            IVersionService versionService,
            IDesktopServiceSettings desktopServiceSettings)
        {
            _versionFactory = versionFactory;
            _releaseVersionInformationService = releaseVersionInformationService;
            _versionService = versionService;
            _desktopServiceSettings = desktopServiceSettings;
        }

        public async Task<ApplicationVersionResult> GetNewVersion()
        {
            var result = await _versionService.GetLatestApplicationVersion(GetCurrentApplicationVersion());

            return result;
        }

        protected Version GetCurrentApplicationVersion()
        {
            var version = _releaseVersionInformationService.VersionNumber;

            return _versionFactory.GetVersion(version, _desktopServiceSettings.ReferenceDataVersion);
        }
    }
}