using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class MdbExportProcessTask : IDesktopTask
    {
        private readonly ILogger _logger;

        public MdbExportProcessTask(ILogger logger)
        {
            _logger = logger;
        }

        public Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Started export Mdb Process");

            ProcessStartInfo startInfo = new ProcessStartInfo(@"ESFA.DC.ILR.Desktop.ExportDatabase.Console.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = $"-f DesktopContext.json -c {desktopContext.KeyValuePairs[ILRContextKeys.Container]}"
            };

            Process p = Process.Start(startInfo);
            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                throw new Exception("Export Access process failed with error, see logs");
            }

            return Task.FromResult(desktopContext);
        }
    }
}
