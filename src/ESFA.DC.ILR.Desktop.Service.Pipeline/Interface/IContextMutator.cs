using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Interface
{
    public interface IContextMutator
    {
        IDesktopContext Mutate(IDesktopContext desktopContext);
    }
}
