using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IDesktopTask
    {
        Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken);
    }
}
