using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Messaging;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.Logging.Interfaces;

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
        private readonly ILogger _logger;

        public CliEntryPoint(
            IMessengerService messengerService, 
            IDesktopContextFactory desktopContextFactory,
            IIlrDesktopService ilrDesktopService,
            IVersionMediatorService versionMediatorService,
            IDesktopReferenceDataDownloadService desktopReferenceDataDownloadService,
            IReferenceDataVersionInformationService referenceDataVersionInformationService,
            IReleaseVersionInformationService releaseVersionInformationService,
            IFeatureSwitchService featureSwitchService,
            ILogger logger)
        {
            _messengerService = messengerService;
            _desktopContextFactory = desktopContextFactory;
            _ilrDesktopService = ilrDesktopService;
            _versionMediatorService = versionMediatorService;
            _desktopReferenceDataDownloadService = desktopReferenceDataDownloadService;
            _referenceDataVersionInformationService = referenceDataVersionInformationService;
            _releaseVersionInformationService = releaseVersionInformationService;
            _featureSwitchService = featureSwitchService;
            _logger = logger;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
        }

        public async Task ExecuteAsync(ICommandLineArguments commandLineArguments, CancellationToken cancellationToken)
        {
            if (_featureSwitchService.VersionUpdate)
            {
                _logger.LogInfo("Checking for Reference data updates.");
                await CheckForReferenceDataUpdates(commandLineArguments.CheckAndUpdateReferenceData);
            }

            _logger.LogInfo("Creating Context.");
            var context = _desktopContextFactory.Build(commandLineArguments);

            await _ilrDesktopService.ProcessAsync(context, cancellationToken);
        }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            Console.WriteLine($"{taskProgressMessage.CurrentTask}/{taskProgressMessage.TaskCount} - {taskProgressMessage.TaskName}");
        }

        private async Task CheckForReferenceDataUpdates(string checkAndUpdateRefData)
        {
            if (string.Equals(checkAndUpdateRefData, "Y", StringComparison.OrdinalIgnoreCase))
            {
                var appVersion = await _versionMediatorService.GetNewVersion();
                var currentAppSchemaVersion = _releaseVersionInformationService.VersionNumber?.Split('.')[1];
                var latestAppSchemaVersion = appVersion.ApplicationVersion?.Split('.')[1];


                if (currentAppSchemaVersion == latestAppSchemaVersion && appVersion.LatestReferenceDataVersion != _referenceDataVersionInformationService.VersionNumber)
                {
                    _logger.LogInfo(string.Concat(
                        "Later version of Reference data found. Was ",
                        _referenceDataVersionInformationService.VersionNumber,
                        ", now ",
                        appVersion.LatestReferenceDataVersion,
                        "."));

                    await _desktopReferenceDataDownloadService.GetReferenceData(appVersion.LatestReferenceDataFileName, appVersion.LatestReferenceDataVersion);
                }
                else
                {
                    _logger.LogInfo(string.Concat("Later version of Reference data not found. Current version is ", _referenceDataVersionInformationService.VersionNumber));
                }
            }
        }
    }
}
