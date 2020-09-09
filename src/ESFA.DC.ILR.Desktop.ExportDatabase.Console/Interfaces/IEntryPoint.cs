using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces
{
    public interface IEntryPoint
    {
        Task ExecuteAsync(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken);
    }
}
