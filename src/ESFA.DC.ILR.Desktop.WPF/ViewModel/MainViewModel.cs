using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Command;
using ESFA.DC.ILR.Desktop.WPF.Command.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.Logging.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string FilenamePlaceholder = "No file chosen";

        private readonly IIlrDesktopService _ilrDesktopService;
        private readonly IDesktopContextFactory _desktopContextFactory;
        private readonly IWindowService _windowService;
        private readonly IDialogInteractionService _dialogInteractionService;
        private readonly IWindowsProcessService _windowsProcessService;
        private readonly IReleaseVersionInformationService _versionInformationService;
        private readonly IReferenceDataVersionInformationService _refDataVersionInformationService;
        private readonly ILogger _logger;
        private readonly IFeatureSwitchService _featureSwitchService;
        private readonly IVersionMediatorService _versionMediatorService;
        private readonly IDesktopReferenceDataDownloadService _desktopReferenceDataDownloadService;

        private CancellationTokenSource _cancellationTokenSource;
        private string _fileName = FilenamePlaceholder;
        private bool _canSubmit;
        private string _taskName;
        private int _currentTask;
        private int _taskCount = 1;
        private StageKeys _currentStage = StageKeys.ChooseFile;
        private string _reportsLocation;
        private bool _canCheckForNewVersion = true;
        private bool _newVersionBannerVisibility;
        private bool _newVersionBannerVisibilityError;
        private bool _uptoDateBannerVisibility;
        private bool _referenceDataBannerVisibility;
        private bool _checkingForUpdatesBannerVisibility;
        private bool _referenceDataDownloadingBannerVisibility;
        private bool _updateMenuEnabled = true;
        private ApplicationVersionResult _newVersion;

        public MainViewModel(
            IIlrDesktopService ilrDesktopService,
            IDesktopContextFactory desktopContextFactory,
            IMessengerService messengerService,
            IWindowService windowService,
            IDialogInteractionService dialogInteractionService,
            IWindowsProcessService windowsProcessService,
            IReleaseVersionInformationService versionInformationService,
            IReferenceDataVersionInformationService refDataVersionInformationService,
            ILogger logger,
            IFeatureSwitchService featureSwitchService,
            IVersionMediatorService versionMediatorService,
            IDesktopReferenceDataDownloadService desktopReferenceDataDownloadService)
        {
            _ilrDesktopService = ilrDesktopService;
            _desktopContextFactory = desktopContextFactory;
            _windowService = windowService;
            _dialogInteractionService = dialogInteractionService;
            _windowsProcessService = windowsProcessService;
            _versionInformationService = versionInformationService;
            _logger = logger;
            _featureSwitchService = featureSwitchService;
            _versionMediatorService = versionMediatorService;
            _refDataVersionInformationService = refDataVersionInformationService;
            _desktopReferenceDataDownloadService = desktopReferenceDataDownloadService;

            messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);

            CheckForUpdateCommand = new AsyncCommand(CheckForNewVersion, CanCheckForNewVersion);
            CheckForUpdateMenuCommand = new AsyncCommand(CheckForNewVersionFromMenu, CanCheckForNewVersion);

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => CanSubmit);
            SettingsNavigationCommand = new RelayCommand(SettingsNavigate);
            AboutNavigationCommand = new RelayCommand(AboutNavigate);
            ReportFiltersNavigationCommand = new RelayCommand(ReportFiltersNavigate);
            ReportsFolderCommand = new RelayCommand(() => ProcessStart(_reportsLocation));
            CancelAndReImportCommand = new RelayCommand(CancelAndReImport, () => !_cancellationTokenSource?.IsCancellationRequested ?? false);
            VersionNavigationCommand = new RelayCommand(NavigateToVersionsUrl);
            ReferenceDataDownloadCommand = new AsyncCommand(DownloadReferenceData);
        }

        public StageKeys CurrentStage
        {
            get => _currentStage;
            set
            {
                _currentStage = value;

                RaisePropertyChanged(nameof(ChooseFileVisibility));
                RaisePropertyChanged(nameof(ProcessingVisibility));
                RaisePropertyChanged(nameof(ProcessedSuccessfullyVisibility));
                RaisePropertyChanged(nameof(ProcessFailureHandledVisibility));
                RaisePropertyChanged(nameof(ProcessFailureUnhandledVisibility));
            }
        }

        public bool CanSubmit
        {
            get => _canSubmit;
            set
            {
                Set(ref _canSubmit, value);
                ProcessFileCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ChooseFileVisibility => CurrentStage == StageKeys.ChooseFile;

        public bool ProcessingVisibility => CurrentStage == StageKeys.Processing;

        public bool ProcessedSuccessfullyVisibility => CurrentStage == StageKeys.ProcessedSuccessfully;

        public bool ProcessFailureHandledVisibility => CurrentStage == StageKeys.ProcessHandledFailure;

        public bool ProcessFailureUnhandledVisibility => CurrentStage == StageKeys.ProcessUnhandledFailure;

        public bool NewVersionBannerVisibility
        {
            get => _newVersionBannerVisibility;
            set => Set(ref _newVersionBannerVisibility, value);
        }

        public bool NewVersionBannerVisibilityError
        {
            get => _newVersionBannerVisibilityError;
            set => Set(ref _newVersionBannerVisibilityError, value);
        }

        public bool UpToDateBannerVisibility
        {
            get => _uptoDateBannerVisibility;
            set => Set(ref _uptoDateBannerVisibility, value);
        }

        public bool CheckingForUpdatesBannerVisibility
        {
            get => _checkingForUpdatesBannerVisibility;
            set => Set(ref _checkingForUpdatesBannerVisibility, value);
        }

        public bool ReferenceDataBannerVisibility
        {
            get => _referenceDataBannerVisibility;
            set => Set(ref _referenceDataBannerVisibility, value);
        }

        public bool ReferenceDataDownloadingBannerVisibility
        {
            get => _referenceDataDownloadingBannerVisibility;
            set => Set(ref _referenceDataDownloadingBannerVisibility, value);
        }

        public bool UpdateMenuEnabled
        {
            get => _updateMenuEnabled;
            set => Set(ref _updateMenuEnabled, value);
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        public string ReportsLocation
        {
            get => _reportsLocation;
            set => Set(ref _reportsLocation, value);
        }

        public string TaskName
        {
            get => _taskName;
            set => Set(ref _taskName, value);
        }

        public int CurrentTask
        {
            get => _currentTask;
            set => Set(ref _currentTask, value);
        }

        public int TaskCount
        {
            get => _taskCount;
            set => Set(ref _taskCount, value);
        }

        public string ReleaseVersionNumber => _versionInformationService.VersionNumber;

        public string ReferenceDataVersionNumber => _refDataVersionInformationService.VersionNumber;

        public string ReleaseDate => _versionInformationService.Date;

        public bool ReportFiltersFeatureSwitch => _featureSwitchService.ReportFilters;

        public bool VersionUpdateFeatureSwitch => _featureSwitchService.VersionUpdate;

        public ApplicationVersionResult NewVersion
        {
            get => _newVersion;
            set => Set(ref _newVersion, value);
        }

        public AsyncCommand CheckForUpdateCommand { get; set; }

        public AsyncCommand CheckForUpdateMenuCommand { get; set; }

        public RelayCommand ChooseFileCommand { get; set; }

        public AsyncCommand ProcessFileCommand { get; set; }

        public RelayCommand SettingsNavigationCommand { get; set; }

        public RelayCommand AboutNavigationCommand { get; set; }

        public RelayCommand ReportFiltersNavigationCommand { get; set; }

        public RelayCommand ReportsFolderCommand { get; set; }

        public RelayCommand CancelAndReImportCommand { get; set; }

        public RelayCommand VersionNavigationCommand { get; set; }

        public IAsyncCommand ReferenceDataDownloadCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        public bool ApplicationVersionUpToDate() => NewVersion != null && NewVersion.ApplicationVersion == ReleaseVersionNumber;

        public bool ApplicationVersionUpdateAvailable() => NewVersion != null && NewVersion.ApplicationVersion != null && NewVersion.ApplicationVersion != ReleaseVersionNumber;

        public bool ApplicationVersionUpdateError() => NewVersion == null || NewVersion.ApplicationVersion == null;

        public bool ReferenceDataUpdateAvailable() => NewVersion != null && NewVersion.ApplicationVersion == ReleaseVersionNumber && NewVersion.LatestReferenceDataVersion != ReferenceDataVersionNumber;

        private void ShowChooseFileDialog()
        {
            var fileName = _dialogInteractionService.GetFileNameFromOpenFileDialog();

            if (!string.IsNullOrWhiteSpace(fileName) && fileName != FilenamePlaceholder)
            {
                FileName = fileName;
                CanSubmit = true;
            }
        }

        private async Task ProcessFile()
        {
            CurrentStage = StageKeys.Processing;

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                CancelAndReImportCommand.RaiseCanExecuteChanged();

                var desktopContext = _desktopContextFactory.Build(FileName);

                var completionContext = await _ilrDesktopService.ProcessAsync(desktopContext, _cancellationTokenSource.Token);

                ReportsLocation = completionContext.OutputDirectory;
                UpdateCurrentStageForCompletionState(completionContext.ProcessingCompletionState);
                CanSubmit = false;
                FileName = FilenamePlaceholder;
            }
            catch (TaskCanceledException taskCanceledException)
            {
                _logger.LogError("Operation Cancelled", taskCanceledException);

                CurrentStage = StageKeys.ChooseFile;
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
        }

        private void CancelAndReImport()
        {
            TaskName = "Cancelling";
            _cancellationTokenSource?.Cancel();

            CancelAndReImportCommand.RaiseCanExecuteChanged();
        }

        private void UpdateCurrentStageForCompletionState(ProcessingCompletionStates? processingCompletionState)
        {
            switch (processingCompletionState)
            {
                case ProcessingCompletionStates.Success:
                    CurrentStage = StageKeys.ProcessedSuccessfully;
                    break;
                case ProcessingCompletionStates.HandledFail:
                    CurrentStage = StageKeys.ProcessHandledFailure;
                    break;
                case ProcessingCompletionStates.UnhandledFail:
                    CurrentStage = StageKeys.ProcessUnhandledFailure;
                    break;
                case ProcessingCompletionStates.Cancelled:
                    CurrentStage = StageKeys.ChooseFile;
                    break;
            }
        }

        private void ProcessStart(string url) => _windowsProcessService.ProcessStart(url);

        private void SettingsNavigate() => _windowService.ShowSettingsWindow();

        private void AboutNavigate() => _windowService.ShowAboutWindow();

        private void ReportFiltersNavigate() => _windowService.ShowReportFiltersWindow();

        private async Task CheckForNewVersionFromMenu()
        {
            CheckingForUpdatesBanner();

            await CheckForNewVersion();

            if (ApplicationVersionUpToDate() && ReferenceDataUpdateAvailable())
            {
                ReferenceDataUpdate();
            }

            if (ApplicationVersionUpToDate() && !ReferenceDataUpdateAvailable())
            {
                VersionStatusUpToDate();
            }

            if (ApplicationVersionUpdateAvailable())
            {
                VersionStatusUpdateAvailable();
            }

            if (ApplicationVersionUpdateError())
            {
                VersionStatusUpdateError();
            }
        }

        private async Task CheckForNewVersion()
        {
            if (!_featureSwitchService.VersionUpdate)
            {
                return;
            }

            try
            {
                _canCheckForNewVersion = false;
                _updateMenuEnabled = false;
                NewVersion = await Task.Run(() => _versionMediatorService.GetNewVersion());
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception found connecting to SLD Public API", exception);
            }
            finally
            {
                _canCheckForNewVersion = true;
                _updateMenuEnabled = true;
            }

            if (ApplicationVersionUpToDate() && ReferenceDataUpdateAvailable())
            {
                ReferenceDataUpdate();
            }

            if (ApplicationVersionUpdateAvailable())
            {
                VersionStatusUpdateAvailable();
            }
        }

        private bool CanCheckForNewVersion()
        {
            return _canCheckForNewVersion;
        }

        private void CheckingForUpdatesBanner()
        {
            UpToDateBannerVisibility = false;
            ReferenceDataBannerVisibility = false;
            ReferenceDataDownloadingBannerVisibility = false;
            CheckingForUpdatesBannerVisibility = true;
            NewVersionBannerVisibilityError = false;
        }

        private void RefreshReferenceDataBanner()
        {
            ReferenceDataBannerVisibility = false;
            UpToDateBannerVisibility = true;
            ReferenceDataDownloadingBannerVisibility = false;
        }

        private void RefDataDownloadingBanner()
        {
            ReferenceDataBannerVisibility = false;
            ReferenceDataDownloadingBannerVisibility = true;
            UpToDateBannerVisibility = false;
        }

        private void VersionStatusUpToDate()
        {
            NewVersionBannerVisibility = false;
            NewVersionBannerVisibilityError = false;
            UpToDateBannerVisibility = true;
            CheckingForUpdatesBannerVisibility = false;
        }

        private void VersionStatusUpdateAvailable()
        {
            NewVersionBannerVisibility = true;
            NewVersionBannerVisibilityError = false;
            UpToDateBannerVisibility = false;
            CheckingForUpdatesBannerVisibility = false;
        }

        private void VersionStatusUpdateError()
        {
            NewVersionBannerVisibility = false;
            NewVersionBannerVisibilityError = true;
            UpToDateBannerVisibility = false;
            CheckingForUpdatesBannerVisibility = false;
        }

        private void ReferenceDataUpdate()
        {
            NewVersionBannerVisibility = false;
            NewVersionBannerVisibilityError = false;
            UpToDateBannerVisibility = false;
            ReferenceDataBannerVisibility = true;
            CheckingForUpdatesBannerVisibility = false;
        }

        private void NavigateToVersionsUrl()
        {
            _windowsProcessService.ProcessStart(NewVersion.Url);
        }

        private async Task DownloadReferenceData()
        {
            RefDataDownloadingBanner();

            await Task.Run(() => _desktopReferenceDataDownloadService.GetReferenceData(NewVersion.LatestReferenceDataFileName, NewVersion.LatestReferenceDataVersion));

            RefreshReferenceDataBanner();
        }
    }
}