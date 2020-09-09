using Autofac;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Mutator;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline
{
    public class ContextMutatorExecutor : IContextMutatorExecutor
    {
        private readonly ILifetimeScope _lifetimeScope;

        public ContextMutatorExecutor(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public IDesktopContext Execute(ContextMutatorKeys contextMutatorKey, IDesktopContext context)
        {
            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope())
            {
                return executionLifetimeScope.ResolveKeyed<IContextMutator>(contextMutatorKey).Mutate(context);
            }
        }
    }
}
