using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Service.Tests.TestSpecificSubClasses
{
    public class VersionMediatorServiceTestClass : VersionMediatorService
    {
        public VersionMediatorServiceTestClass(
            IVersionFactory versionFactory,
            IReleaseVersionInformationService versionInformationService,
            IVersionService versionService)
            : base(versionFactory, versionInformationService, versionService)
        {
        }

        public new Version GetCurrentApplicationVersion()
        {
            return base.GetCurrentApplicationVersion();
        }
    }
}