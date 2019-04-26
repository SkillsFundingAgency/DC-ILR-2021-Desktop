using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class IlrDesktopServiceStub : IIlrDesktopService
    {
        public async Task ProcessAsync(string filePath, CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken);
        }
    }
}
