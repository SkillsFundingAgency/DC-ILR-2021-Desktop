using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IDesktopTask
    {
        Task<IDesktopTask> ExecuteAsync(IDesktopTask desktopTask, CancellationToken cancellationToken);
    }
}
