using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Mutator;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IContextMutatorExecutor
    {
        IDesktopContext Execute(ContextMutatorKeys contextMutatorKey, IDesktopContext context);
    }
}
