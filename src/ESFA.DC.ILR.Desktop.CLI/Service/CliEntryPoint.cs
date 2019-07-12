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
        private readonly IDesktopContextFactory _desktopContextFactory;
        private readonly IIlrDesktopService _ilrDesktopService;

        public CliEntryPoint(IMessengerService messengerService, IDesktopContextFactory desktopContextFactory, IIlrDesktopService ilrDesktopService)
        {
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
            _ilrDesktopService = ilrDesktopService;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
        }

        public async Task ExecuteAsync(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken)
        {
            var context = _desktopContextFactory.Build(commandLineArguments);

            await _ilrDesktopService.ProcessAsync(context, cancellationToken);
        }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            Console.WriteLine($"{taskProgressMessage.CurrentTask}/{taskProgressMessage.TaskCount} - {taskProgressMessage.TaskName}");
        }
    }
}
