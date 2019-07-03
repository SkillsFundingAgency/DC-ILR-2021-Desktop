using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;

namespace ESFA.DC.ILR.Desktop.CLI.Service
{
    public class CliEntryPoint : ICliEntryPoint
    {
        private readonly IMessengerService _messengerService;

        public CliEntryPoint(IMessengerService messengerService)
        {
            _messengerService = messengerService;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
        }

        public Task Execute(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            Console.WriteLine($"{taskProgressMessage.CurrentTask}/{taskProgressMessage.TaskCount} - {taskProgressMessage.TaskName}");
        }
    }
}
