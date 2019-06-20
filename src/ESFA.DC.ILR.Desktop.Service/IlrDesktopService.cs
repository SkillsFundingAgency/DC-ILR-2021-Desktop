using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.Service.Model;
using ESFA.DC.ILR.Desktop.Service.Tasks;
using ESFA.DC.ILR.Desktop.Service.Tasks.Extensions;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class IlrDesktopService : IIlrDesktopService
    {
        private readonly IMessengerService _messengerService;
        private readonly IDesktopContextFactory _desktopContextFactory;
        private readonly IIlrPipelineProvider _ilrPipelineProvider;
        private readonly IDesktopTaskExecutionService _desktopTaskExecutionService;
        private readonly ILogger _logger;

        public IlrDesktopService(
            IMessengerService messengerService,
            IDesktopContextFactory desktopContextFactory,
            IIlrPipelineProvider ilrPipelineProvider,
            IDesktopTaskExecutionService desktopTaskExecutionService,
            ILogger logger)
        {
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
            _ilrPipelineProvider = ilrPipelineProvider;
            _desktopTaskExecutionService = desktopTaskExecutionService;
            _logger = logger;
        }

        public async Task<ICompletionContext> ProcessAsync(string filePath, CancellationToken cancellationToken)
        {
            var context = _desktopContextFactory.Build(filePath);

            var completionContext = new CompletionContext()
            {
                OutputDirectory = context.OutputDirectory,
                ProcessingCompletionState = ProcessingCompletionStates.Success,
            };

            var stepsList = _ilrPipelineProvider.Provide();

            var step = 0;

            while (step < stepsList.Count)
            {
                var desktopTaskDefinition = stepsList[step];

                _messengerService.Send(new TaskProgressMessage(desktopTaskDefinition.Key.GetDisplayText(), step, stepsList.Count));

                var result = await _desktopTaskExecutionService.ExecuteAsync(desktopTaskDefinition.Key, context, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                if (!result.IsFaulted)
                {
                    step++;
                }
                else
                {
                    if (desktopTaskDefinition.FailureKey != null)
                    {
                        step = _ilrPipelineProvider.IndexFor(desktopTaskDefinition.FailureKey.Value);

                        completionContext.ProcessingCompletionState = ProcessingCompletionStates.HandledFail;

                        _logger.LogError($"Task Execution Handled Failure - Step {step}", result.Exception);
                    }
                    else
                    {
                        completionContext.ProcessingCompletionState = ProcessingCompletionStates.UnhandledFail;

                        _logger.LogError($"Task Execution Unhandled Failure - Step {step}", result.Exception);

                        return completionContext;
                    }
                }
            }

            _messengerService.Send(new TaskProgressMessage("Processing Complete", stepsList.Count, stepsList.Count));

            return completionContext;
        }
    }
}
