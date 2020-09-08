using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Interface
{
    public interface IDesktopTaskExecutionService
    {
        Task<IDesktopContext> ExecuteAsync(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken);
    }
}
