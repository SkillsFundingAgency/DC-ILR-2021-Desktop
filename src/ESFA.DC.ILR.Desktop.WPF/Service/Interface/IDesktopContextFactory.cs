using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service.Interface
{
    public interface IDesktopContextFactory
    {
        IDesktopContext Build(string filePath);
    }
}
