using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.CLI.Interface
{
    public interface IDesktopContextFactory
    {
        IDesktopContext Build(ICommandLineArguments commandLineArguments);
    }
}
