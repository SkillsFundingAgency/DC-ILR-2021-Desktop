using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Interface.Services
{
    public interface IVersionMessageFactory
    {
        VersionMessage GetVersionMessage(ApplicationVersionResult applicationVersionResult);
    }
}