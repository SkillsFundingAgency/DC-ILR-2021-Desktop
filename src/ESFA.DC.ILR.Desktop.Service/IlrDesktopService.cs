using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Messaging;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.Service.Model;
using ESFA.DC.ILR.Desktop.Service.Tasks.Extensions;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class IlrDesktopService : IIlrDesktopService
    {
        private readonly IMessengerService _messengerService;
        private readonly IIlrPipelineProvider _ilrPipelineProvider;
        private readonly IDesktopTaskExecutionService _desktopTaskExecutionService;
        private readonly IContextMutatorExecutor _contextMutatorExecutor;
        private readonly ILogger _logger;

        public IlrDesktopService(
            IMessengerService messengerService,
            IIlrPipelineProvider ilrPipelineProvider,
            IDesktopTaskExecutionService desktopTaskExecutionService,
            IContextMutatorExecutor contextMutatorExecutor,
            ILogger logger)
        {
            _messengerService = messengerService;
            _ilrPipelineProvider = ilrPipelineProvider;
            _desktopTaskExecutionService = desktopTaskExecutionService;
            _contextMutatorExecutor = contextMutatorExecutor;
            _logger = logger;
        }

        public async Task<ICompletionContext> ProcessAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var completionContext = new CompletionContext()
            {
                OutputDirectory = desktopContext.OutputDirectory,
                ProcessingCompletionState = ProcessingCompletionStates.Success,
            };

            var stepsList = _ilrPipelineProvider.Provide();

            var step = 0;

            while (step < stepsList.Count)
            {
                var desktopTaskDefinition = stepsList[step];

                _messengerService.Send(new TaskProgressMessage(desktopTaskDefinition.Key.GetDisplayText(), step, stepsList.Count));

                try
                {
                    desktopContext = await _desktopTaskExecutionService
                        .ExecuteAsync(desktopTaskDefinition.Key, desktopContext, cancellationToken)
                        .ConfigureAwait(false);

                    cancellationToken.ThrowIfCancellationRequested();

                    step++;
                }
                catch (TaskCanceledException taskCanceledException)
                {
                    completionContext.ProcessingCompletionState = ProcessingCompletionStates.Cancelled;

                    _logger.LogError($"Task Cancelled - Step {step}", taskCanceledException);

                    return completionContext;
                }
                catch (Exception exception)
                {
                    if (desktopTaskDefinition.FailureKey != null)
                    {
                        if (desktopTaskDefinition.FailureContextMutatorKey != null)
                        {
                            desktopContext = _contextMutatorExecutor.Execute(desktopTaskDefinition.FailureContextMutatorKey.Value, desktopContext);
                        }

                        step = _ilrPipelineProvider.IndexFor(desktopTaskDefinition.FailureKey.Value, stepsList);

                        completionContext.ProcessingCompletionState = ProcessingCompletionStates.HandledFail;

                        _logger.LogError($"Task Execution Handled Failure - Step {step}", exception);
                    }
                    else
                    {
                        completionContext.ProcessingCompletionState = ProcessingCompletionStates.UnhandledFail;

                        _logger.LogError($"Task Execution Unhandled Failure - Step {step}", exception);

                        return completionContext;
                    }
                }
            }

            _messengerService.Send(new TaskProgressMessage("Processing Complete", stepsList.Count, stepsList.Count));

            return completionContext;
        }
    }
}
