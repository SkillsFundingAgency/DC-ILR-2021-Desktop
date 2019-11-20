using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IVersionMediatorService
    {
        Task<bool> CheckForUpdates();
    }
}