using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class VersionMediatorService : IVersionMediatorService
    {
        private readonly IVersionFactory _versionFactory;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;
        private readonly IVersionService _versionService;

        public VersionMediatorService(
            IVersionFactory versionFactory,
            IReleaseVersionInformationService releaseVersionInformationService,
            IVersionService versionService)
        {
            _versionFactory = versionFactory;
            _releaseVersionInformationService = releaseVersionInformationService;
            _versionService = versionService;
        }

        public async Task<ApplicationVersionResult> GetNewVersion()
        {
            var result = await _versionService.GetLatestApplicationVersion(GetCurrentApplicationVersion());

            return result;
        }

        protected Version GetCurrentApplicationVersion()
        {
            var version = _releaseVersionInformationService.VersionNumber;
            return _versionFactory.GetVersion(version);
        }
    }
}