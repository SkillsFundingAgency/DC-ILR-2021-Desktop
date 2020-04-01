using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service.Tests.TestSpecificSubClasses
{
    public class VersionMediatorServiceTestClass : VersionMediatorService
    {
        public VersionMediatorServiceTestClass(
            IVersionFactory versionFactory,
            IReleaseVersionInformationService versionInformationService,
            IVersionService versionService,
            IDesktopServiceSettings desktopServiceSettings,
            ILogger logger = null)
            : base(versionFactory, versionInformationService, versionService, desktopServiceSettings, logger)
        {
        }

        public new Version GetCurrentApplicationVersion()
        {
            return base.GetCurrentApplicationVersion();
        }
    }
}