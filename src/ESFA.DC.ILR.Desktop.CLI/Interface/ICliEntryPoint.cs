using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.CLI.Interface
{
    public interface ICliEntryPoint
    {
        Task Execute(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken);
    }
}
