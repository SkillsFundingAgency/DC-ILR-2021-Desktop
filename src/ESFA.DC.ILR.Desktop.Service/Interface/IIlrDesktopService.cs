using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IIlrDesktopService
    {
        Task<ICompletionContext> ProcessAsync(IDesktopContext desktopContext, CancellationToken cancellationToken);
    }
}
