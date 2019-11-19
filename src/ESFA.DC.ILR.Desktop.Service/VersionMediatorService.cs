using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class VersionMediatorService : IVersionMediatorService
    {
        private readonly IVersionFactory _versionFactory;
        private readonly IVersionMessageFactory _versionMessageFactory;
        private readonly IMessengerService _messengerService;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;
        private readonly IVersionService _versionService;

        public VersionMediatorService(
            IVersionFactory versionFactory,
            IVersionMessageFactory versionMessageFactory,
            IMessengerService messengerService,
            IReleaseVersionInformationService releaseVersionInformationService,
            IVersionService versionService)
        {
            _versionFactory = versionFactory;
            _versionMessageFactory = versionMessageFactory;
            _messengerService = messengerService;
            _releaseVersionInformationService = releaseVersionInformationService;
            _versionService = versionService;
        }

        public async Task<bool> CheckForUpdates()
        {
            var result = await _versionService.GetLatestApplicationVersion(GetCurrentApplicationVersion());

            if (result != null)
            {
                _messengerService.Send(_versionMessageFactory.GetVersionMessage(result));
            }

            return result != null;
        }

        protected Version GetCurrentApplicationVersion()
        {
            var version = _releaseVersionInformationService.VersionNumber;
            return _versionFactory.GetVersion(version);
        }
    }
}