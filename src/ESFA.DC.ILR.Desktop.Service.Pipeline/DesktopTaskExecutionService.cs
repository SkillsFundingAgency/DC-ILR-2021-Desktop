using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline
{
    public class DesktopTaskExecutionService : IDesktopTaskExecutionService
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IImmutableDictionary<IlrDesktopTaskKeys, Func<Module>> _keyedModules;

        public DesktopTaskExecutionService(ILifetimeScope lifetimeScope, IImmutableDictionary<IlrDesktopTaskKeys, Func<Module>> keyedModules)
        {
            _lifetimeScope = lifetimeScope;
            _keyedModules = keyedModules;
        }

        public async Task<IDesktopContext> ExecuteAsync(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            Task<IDesktopContext> task = ExecuteAsyncAction(ilrDesktopTaskKey, desktopContext, cancellationToken);

            return await Task.Run(() => task, cancellationToken).ConfigureAwait(false);
        }

        private async Task<IDesktopContext> ExecuteAsyncAction(IlrDesktopTaskKeys ilrDesktopTaskKey, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope(c =>
                {
                    _keyedModules.TryGetValue(ilrDesktopTaskKey, out var module);

                    if (module != null)
                    {
                        c.RegisterModule(module());
                    }
                }))
            {
                return await executionLifetimeScope
                    .ResolveKeyed<IDesktopTask>(ilrDesktopTaskKey)
                    .ExecuteAsync(desktopContext, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
