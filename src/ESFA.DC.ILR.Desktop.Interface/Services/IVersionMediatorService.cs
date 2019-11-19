using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service
{
    public interface IVersionMediatorService
    {
        Task<bool> CheckForUpdates();
    }
}