using System;
using System.Collections.Generic;
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

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class IlrDesktopService : IIlrDesktopService
    {
        private readonly IIndex<IlrDesktopTaskKeys, IDesktopTask> _desktopTaskIndex;
        private readonly IMessengerService _messengerService;
        private readonly IDesktopContextFactory _desktopContextFactory;
        private readonly ILogger _logger;

        public IlrDesktopService(IIndex<IlrDesktopTaskKeys, IDesktopTask> desktopTaskIndex, IMessengerService messengerService, IDesktopContextFactory desktopContextFactory, ILogger logger)
        {
            _desktopTaskIndex = desktopTaskIndex;
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
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

            var stepsList = BuildTaskKeys().ToList();

            var step = 0;

            while (step < stepsList.Count)
            {
                var desktopTaskDefinition = stepsList[step];

                var result = await ExecuteTask(desktopTaskDefinition, step, stepsList.Count, context, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                if (!result.IsFaulted)
                {
                    step++;
                }
                else
                {
                    if (desktopTaskDefinition.FailureKey != null)
                    {
                        step = stepsList.FindIndex(s => s.Key == desktopTaskDefinition.FailureKey);

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

        private Task<Task<IDesktopContext>> ExecuteTask(IIlrDesktopTaskDefinition ilrDesktopTaskDefinition, int step, int stepCount, IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            _messengerService.Send(new TaskProgressMessage(ilrDesktopTaskDefinition.Key.GetDisplayText(), step, stepCount));

            return Task.Factory.StartNew(() => _desktopTaskIndex[ilrDesktopTaskDefinition.Key].ExecuteAsync(desktopContext, cancellationToken), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private IIlrDesktopTaskDefinition[] BuildTaskKeys()
        {
            return new IIlrDesktopTaskDefinition[]
            {
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PreExecution),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DatabaseCreate),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FileValidationService),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReferenceDataService),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ValidationService),
              //  new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FundingService),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DataStore),
              //  new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReportService),
              //  new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PostExecution),
            };
        }
    }
}
