using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class StoreDesktopContextTask : IDesktopTask
    {
        private readonly ILogger _logger;
        private readonly IFileService _fileService;
        private readonly IJsonSerializationService _jsonSerializationService;

        public StoreDesktopContextTask(ILogger logger, IFileService fileService, IJsonSerializationService jsonSerializationService)
        {
            _logger = logger;
            _fileService = fileService;
            _jsonSerializationService = jsonSerializationService;
        }

        public async Task<IDesktopContext> ExecuteAsync(IDesktopContext desktopContext, CancellationToken cancellationToken)
        {
            var outputPath = Path.Combine(desktopContext.KeyValuePairs[ILRContextKeys.Container].ToString(), "DesktopContext.json");

            using (var writeStream = await _fileService.OpenWriteStreamAsync(outputPath, null, cancellationToken))
            {
                _jsonSerializationService.Serialize(desktopContext, writeStream);
            }

            _logger.LogInfo($"Saved Desktop context To : {outputPath}");

            return await Task.FromResult(desktopContext);
        }
    }
}
