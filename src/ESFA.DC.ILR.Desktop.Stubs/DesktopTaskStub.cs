using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs.Tasks
{
    public class DesktopTaskStub : IDesktopTask
    {
        public async Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);

            return desktopContext;
        }
    }
}
