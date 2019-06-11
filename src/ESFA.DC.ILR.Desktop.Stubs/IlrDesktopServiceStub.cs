using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using ESFA.DC.ILR.Desktop.Service.Tasks.Extensions;

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

        public async Task<string> ProcessAsync(string filePath, CancellationToken cancellationToken)
        {
            var context = _desktopContextFactory.Build(filePath);

            var steps = BuildTaskKeys();

            var stepCount = steps.Length;
            var step = 0;

            while (step < stepCount)
            {
                await ExecuteTask(steps, step, context, cancellationToken);
                step++;
            }

            _messengerService.Send(new TaskProgressMessage("Processing Complete", stepCount, stepCount));

            return context.OutputDirectory;
        }

        private async Task ExecuteTask(IlrDesktopTaskKeys[] ilrDesktopTaskKeys, int step, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var desktopTaskKey = ilrDesktopTaskKeys[step];

            _messengerService.Send(new TaskProgressMessage(desktopTaskKey.GetDisplayText(), step, ilrDesktopTaskKeys.Length));

            await Task.Factory.StartNew(() => _desktopTaskIndex[desktopTaskKey].ExecuteAsync(desktopContext, cancellationToken), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private IlrDesktopTaskKeys[] BuildTaskKeys()
        {
            return new IlrDesktopTaskKeys[]
            {
                IlrDesktopTaskKeys.PreExecution,
                IlrDesktopTaskKeys.DatabaseCreate,
                IlrDesktopTaskKeys.FileValidationService,
                IlrDesktopTaskKeys.ReferenceDataService,
                IlrDesktopTaskKeys.ValidationService,
                IlrDesktopTaskKeys.FundingService,
                IlrDesktopTaskKeys.DataStore,
                IlrDesktopTaskKeys.ReportService,
                IlrDesktopTaskKeys.PostExecution,
            };
        }
    }
}
