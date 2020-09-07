using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces
{
    public interface IDesktopContextFactory
    {
        Task<IDesktopContext> Build(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken);
    }
}
