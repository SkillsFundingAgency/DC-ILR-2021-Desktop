using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces;
using ESFA.DC.ILR.Desktop.Messaging;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Message;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Services
{
    public class EntryPoint : IEntryPoint
    {
        private readonly IMessengerService _messengerService;
        private readonly IDesktopContextFactory _desktopContextFactory;
        private readonly IIlrDesktopService _ilrDesktopService;

        public EntryPoint(
            IMessengerService messengerService,
            IDesktopContextFactory desktopContextFactory,
            IIlrDesktopService ilrDesktopService)
        {
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
            _ilrDesktopService = ilrDesktopService;
            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
        }

        public async Task ExecuteAsync(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken)
        {
            var context = await _desktopContextFactory.Build(commandLineArguments, cancellationToken);

            await _ilrDesktopService.ProcessAsync(context, cancellationToken);
        }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            System.Console.WriteLine($"{taskProgressMessage.CurrentTask}/{taskProgressMessage.TaskCount} - {taskProgressMessage.TaskName}");
        }

    }
}
