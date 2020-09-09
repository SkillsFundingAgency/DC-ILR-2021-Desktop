using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Context;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Services
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IFileService _fileService;
        private readonly IJsonSerializationService _jsonSerializationService;

        public DesktopContextFactory(IFileService fileService, IJsonSerializationService jsonSerializationService)
        {
            _fileService = fileService;
            _jsonSerializationService = jsonSerializationService;
        }

        public async Task<IDesktopContext> Build(ICommandLineArguments commandLineArguments, CancellationToken cancellation)
        {
            IDesktopContext desktopContext;

            using (var fileStream = await _fileService.OpenReadStreamAsync(commandLineArguments.FilePath, commandLineArguments.Container, cancellation))
            {
                desktopContext = _jsonSerializationService.Deserialize<DesktopContext>(fileStream);
            }

            return desktopContext;
        }
    }
}
