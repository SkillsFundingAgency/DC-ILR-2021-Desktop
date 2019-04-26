using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IIlrDesktopService
    {
        Task ProcessAsync(string filePath, CancellationToken cancellationToken);
    }
}
