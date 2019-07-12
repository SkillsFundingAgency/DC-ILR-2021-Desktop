using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.CLI.Interface
{
    public interface ICliEntryPoint
    {
        Task ExecuteAsync(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken);
    }
}
