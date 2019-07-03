using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IDesktopServiceSettings
    {
        string IlrDatabaseConnectionString { get; set; }

        string OutputDirectory { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
