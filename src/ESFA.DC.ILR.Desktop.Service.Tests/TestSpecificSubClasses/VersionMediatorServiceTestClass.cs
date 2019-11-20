using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Tests.TestSpecificSubClasses
{
    public class VersionMediatorServiceTestClass : VersionMediatorService
    {
        public VersionMediatorServiceTestClass(
            IVersionFactory versionFactory,
            IVersionMessageFactory versionMessageFactory,
            IMessengerService messengerService,
            IReleaseVersionInformationService versionInformationService,
            IVersionService versionService)
            : base(versionFactory, versionMessageFactory, messengerService, versionInformationService, versionService)
        {
        }

        public new Version GetCurrentApplicationVersion()
        {
            return base.GetCurrentApplicationVersion();
        }
    }
}