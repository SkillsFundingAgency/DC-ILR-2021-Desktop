using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class DesktopTaskExecutionService : IDesktopTaskExecutionService
    {
        private readonly ILifetimeScope _lifetimeScope;

        public DesktopTaskExecutionService(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task<IDesktopContext> ExecuteAsync(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            Task<IDesktopContext> task = ExecuteAsyncAction(ilrDesktopTaskKey, desktopContext, cancellationToken);

            return await Task.Run(() => task, cancellationToken).ConfigureAwait(false);
        }

        private async Task<IDesktopContext> ExecuteAsyncAction(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope())
            {
                return await executionLifetimeScope
                    .ResolveKeyed<IDesktopTask>(ilrDesktopTaskKey)
                    .ExecuteAsync(desktopContext, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
