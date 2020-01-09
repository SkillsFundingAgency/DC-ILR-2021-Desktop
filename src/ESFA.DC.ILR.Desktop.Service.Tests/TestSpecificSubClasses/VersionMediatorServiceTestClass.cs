using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Tests.TestSpecificSubClasses
{
    public class VersionMediatorServiceTestClass : VersionMediatorService
    {
        public VersionMediatorServiceTestClass(
            IVersionFactory versionFactory,
            IReleaseVersionInformationService versionInformationService,
            IVersionService versionService,
            IDesktopServiceSettings desktopServiceSettings)
            : base(versionFactory, versionInformationService, versionService, desktopServiceSettings)
        {
        }

        public new Version GetCurrentApplicationVersion()
        {
            return base.GetCurrentApplicationVersion();
        }
    }
}