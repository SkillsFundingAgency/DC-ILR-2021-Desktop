using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<Task<IDesktopContext>> ExecuteAsync(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            return await Task.Factory.StartNew(
                () => ExecuteAsyncAction(ilrDesktopTaskKey, desktopContext, cancellationToken),
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        private async Task<IDesktopContext> ExecuteAsyncAction(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope())
            {
                return await executionLifetimeScope.ResolveKeyed<IDesktopTask>(ilrDesktopTaskKey).ExecuteAsync(desktopContext, cancellationToken);
            }
        }
    }
}
