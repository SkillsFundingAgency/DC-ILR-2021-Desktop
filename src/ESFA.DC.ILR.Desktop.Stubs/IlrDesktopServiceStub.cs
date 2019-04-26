using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class IlrDesktopServiceStub : IIlrDesktopService
    {
        private readonly IMessengerService _messengerService;
        private readonly IDesktopTask _desktopTask;

        private readonly IDesktopTask _fileValidationServiceDesktopTask;
        private readonly IDesktopTask _referenceDataServiceDesktopTask;
        private readonly IDesktopTask _validationServiceDesktopTask;
        private readonly IDesktopTask _fundingServiceDesktopTask;
        private readonly IDesktopTask _reportsServiceDesktopTask;
        private readonly IDesktopTask _dataStoreServiceDesktopTask;

        public IlrDesktopServiceStub(IMessengerService messengerService, IDesktopTask desktopTask)
        {
            _messengerService = messengerService;

            _fileValidationServiceDesktopTask = desktopTask;
            _referenceDataServiceDesktopTask = desktopTask;
            _validationServiceDesktopTask = desktopTask;
            _fundingServiceDesktopTask = desktopTask;
            _reportsServiceDesktopTask = desktopTask;
            _dataStoreServiceDesktopTask = desktopTask;
        }

        public async Task ProcessAsync(string filePath, CancellationToken cancellationToken)
        {
            var context = new DesktopContextStub();

            _messengerService.Send(new TaskProgressMessage("File Validation"));

            await _fileValidationServiceDesktopTask.ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Reference Data"));
            
            await _referenceDataServiceDesktopTask.ExecuteAsync(context, cancellationToken);
            
            _messengerService.Send(new TaskProgressMessage("Validation"));

            await _validationServiceDesktopTask.ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Funding Calculation"));

            await _fundingServiceDesktopTask.ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Report Generation"));

            await _reportsServiceDesktopTask.ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Store Data"));

            await _dataStoreServiceDesktopTask.ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Processing Complete"));
        }
    }
}
