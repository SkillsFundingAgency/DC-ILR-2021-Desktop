using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class VersionMessageFactory : IVersionMessageFactory
    {
        public VersionMessage GetVersionMessage(ApplicationVersionResult applicationVersionResult)
        {
            return new VersionMessage
            {
                ApplicationVersion = applicationVersionResult
            };
        }
    }
}