using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class IlrDesktopServiceStub : IIlrDesktopService
    {
        private readonly IIndex<IlrDesktopTaskKeys, IDesktopTask> _desktopTaskIndex;
        private readonly IMessengerService _messengerService;
        private readonly IDesktopContextFactory _desktopContextFactory;

        public IlrDesktopServiceStub(IIndex<IlrDesktopTaskKeys, IDesktopTask> desktopTaskIndex, IMessengerService messengerService, IDesktopContextFactory desktopContextFactory)
        {
            _desktopTaskIndex = desktopTaskIndex;
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
        }

        public async Task ProcessAsync(string filePath, CancellationToken cancellationToken)
        {
            var context = _desktopContextFactory.Build(filePath);

            var steps = BuildTaskKeys();

            var stepCount = steps.Length;
            var step = 0;

            _messengerService.Send(new TaskProgressMessage("Pre Processing", step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("File Validation", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Reference Data", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Validation", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Funding Calculation", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Report Generation", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Store Data", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Post Processing", ++step, stepCount));

            await _desktopTaskIndex[steps[step]].ExecuteAsync(context, cancellationToken);

            _messengerService.Send(new TaskProgressMessage("Processing Complete", ++step, stepCount));
        }

        private IlrDesktopTaskKeys[] BuildTaskKeys()
        {
            return new IlrDesktopTaskKeys[]
            {
                IlrDesktopTaskKeys.PreExecution,
                IlrDesktopTaskKeys.DatabaseCreate,
                IlrDesktopTaskKeys.FileValidationService,
                IlrDesktopTaskKeys.ValidationService,
                IlrDesktopTaskKeys.FundingService,
                IlrDesktopTaskKeys.DataStore,
                IlrDesktopTaskKeys.ReportService,
                IlrDesktopTaskKeys.PostExecution,
            };
        }
    }
}
