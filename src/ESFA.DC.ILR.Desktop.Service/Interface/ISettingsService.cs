using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface ISettingsService
    {
        Task SaveAsync(IDesktopServiceSettings settings, CancellationToken cancellationToken);
        
        Task<IDesktopServiceSettings> LoadAsync(CancellationToken cancellationToken, string directoryType);
    }
}
