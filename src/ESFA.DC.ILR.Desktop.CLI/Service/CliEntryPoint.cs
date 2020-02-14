using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;

namespace ESFA.DC.ILR.Desktop.CLI.Service
{
    public class CliEntryPoint : ICliEntryPoint
    {
        private readonly IMessengerService _messengerService;
        private readonly IDesktopContextFactory _desktopContextFactory;
        private readonly IIlrDesktopService _ilrDesktopService;
        private readonly IVersionMediatorService _versionMediatorService;
        private readonly IDesktopReferenceDataDownloadService _desktopReferenceDataDownloadService;
        private readonly IReferenceDataVersionInformationService _referenceDataVersionInformationService;
        private readonly IReleaseVersionInformationService _releaseVersionInformationService;
        private readonly IFeatureSwitchService _featureSwitchService;

        public CliEntryPoint(
            IMessengerService messengerService, 
            IDesktopContextFactory desktopContextFactory,
            IIlrDesktopService ilrDesktopService,
            IVersionMediatorService versionMediatorService,
            IDesktopReferenceDataDownloadService desktopReferenceDataDownloadService,
            IReferenceDataVersionInformationService referenceDataVersionInformationService,
            IReleaseVersionInformationService releaseVersionInformationService,
            IFeatureSwitchService featureSwitchService)
        {
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
            _ilrDesktopService = ilrDesktopService;
            _versionMediatorService = versionMediatorService;
            _desktopReferenceDataDownloadService = desktopReferenceDataDownloadService;
            _referenceDataVersionInformationService = referenceDataVersionInformationService;
            _releaseVersionInformationService = releaseVersionInformationService;
            _featureSwitchService = featureSwitchService;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
        }

        public async Task ExecuteAsync(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken)
        {
            if (_featureSwitchService.VersionUpdate)
            {
                await CheckForReferenceDataUpdates();
            }

            var context = _desktopContextFactory.Build(commandLineArguments);

            await _ilrDesktopService.ProcessAsync(context, cancellationToken);
        }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            Console.WriteLine($"{taskProgressMessage.CurrentTask}/{taskProgressMessage.TaskCount} - {taskProgressMessage.TaskName}");
        }

        private async Task CheckForReferenceDataUpdates()
        {
            // to be follow up under a story - stubbed for now. Replace with commandLineArguments.CheckAndUpdateReferenceData at later date. 
            var checkForRefData = "N";

            if (string.Equals(checkForRefData, "Y", StringComparison.OrdinalIgnoreCase))
            {
                var appVersion = await _versionMediatorService.GetNewVersion();

                if (appVersion.ApplicationVersion == _releaseVersionInformationService.VersionNumber && appVersion.LatestReferenceDataVersion != _referenceDataVersionInformationService.VersionNumber)
                {
                    await _desktopReferenceDataDownloadService.GetReferenceData(appVersion.LatestReferenceDataFileName, appVersion.LatestReferenceDataVersion);
                }
            }
        }
    }
}
