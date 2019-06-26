using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IContextMutator
    {
        IDesktopContext Mutate(IDesktopContext desktopContext);
    }
}
