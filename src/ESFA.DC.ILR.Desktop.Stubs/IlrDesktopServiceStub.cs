using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class IlrDesktopServiceStub : IIlrDesktopService
    {
        private readonly IMessengerService _messengerService;

        public IlrDesktopServiceStub(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public async Task ProcessAsync(string filePath, CancellationToken cancellationToken)
        {
            _messengerService.Send(new TaskProgressMessage("File Validation"));

            await Task.Delay(1000, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Validation"));
            
            await Task.Delay(2000, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Funding Calculation"));

            await Task.Delay(3000, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Report Generation"));
            
            await Task.Delay(2000, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Store Data"));

            await Task.Delay(2000, cancellationToken);
        }
    }
}
