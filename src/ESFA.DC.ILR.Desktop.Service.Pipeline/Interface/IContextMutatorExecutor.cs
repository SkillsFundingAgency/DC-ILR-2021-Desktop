using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Mutator;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Interface
{
    public interface IContextMutatorExecutor
    {
        IDesktopContext Execute(ContextMutatorKeys contextMutatorKey, IDesktopContext context);
    }
}
