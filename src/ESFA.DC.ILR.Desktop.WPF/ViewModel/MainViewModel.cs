using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Command;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.Logging.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private const string _filenamePlaceholder = "No file chosen";

        private readonly IIlrDesktopService _ilrDesktopService;
        private readonly IMessengerService _messengerService;
        private readonly IWindowService _windowService;
        private readonly IDialogInteractionService _dialogInteractionService;
        private readonly IWindowsProcessService _windowsProcessService;
        private readonly IUrlService _urlService;
        private readonly ILogger _logger;

        private CancellationTokenSource _cancellationTokenSource;
        private string _fileName = _filenamePlaceholder;
        private bool _canSubmit;
        private string _taskName;
        private int _currentTask;
        private int _taskCount = 1;
        private StageKeys _currentStage = StageKeys.ChooseFile;
        private string _reportsLocation;

        public MainViewModel(
            IIlrDesktopService ilrDesktopService,
            IMessengerService messengerService,
            IWindowService windowService,
            IDialogInteractionService dialogInteractionService,
            IWindowsProcessService windowsProcessService,
            IUrlService urlService,
            ILogger logger)
        {
            _ilrDesktopService = ilrDesktopService;
            _messengerService = messengerService;
            _windowService = windowService;
            _dialogInteractionService = dialogInteractionService;
            _windowsProcessService = windowsProcessService;
            _urlService = urlService;
            _logger = logger;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => CanSubmit);
            SettingsNavigationCommand = new RelayCommand(SettingsNavigate);
            AboutNavigationCommand = new RelayCommand(AboutNavigate);
            SurveyHyperlinkCommand = new RelayCommand(() => ProcessStart(_urlService.Survey()));
            GuidanceHyperlinkCommand = new RelayCommand(() => ProcessStart(_urlService.Guidance()));
            HelpdeskHyperlinkCommand = new RelayCommand(() => ProcessStart(_urlService.Helpdesk()));
            ReportsFolderCommand = new RelayCommand(() => ProcessStart(_reportsLocation));
            CancelAndReuploadCommand = new RelayCommand(CancelAndReupload);
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

        public RelayCommand ChooseFileCommand { get; set; }

        public AsyncCommand ProcessFileCommand { get; set; }

        public RelayCommand SettingsNavigationCommand { get; set; }

        public RelayCommand AboutNavigationCommand { get; set; }

        public RelayCommand SurveyHyperlinkCommand { get; set; }

        public RelayCommand GuidanceHyperlinkCommand { get; set; }

        public RelayCommand HelpdeskHyperlinkCommand { get; set; }

        public RelayCommand ReportsFolderCommand { get; set; }

        public RelayCommand CancelAndReuploadCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        private void ShowChooseFileDialog()
        {
            var fileName = _dialogInteractionService.GetFileNameFromOpenFileDialog();

            if (!string.IsNullOrWhiteSpace(fileName) && fileName != _filenamePlaceholder)
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

                var completionContext = await _ilrDesktopService.ProcessAsync(FileName, _cancellationTokenSource.Token);

                ReportsLocation = completionContext.OutputDirectory;
                UpdateCurrentStageForCompletionState(completionContext.ProcessingCompletionState);
                CanSubmit = false;
                FileName = _filenamePlaceholder;
            }
            catch (OperationCanceledException operationCanceledException)
            {
                _logger.LogError("Operation Cancelled", operationCanceledException);

                CurrentStage = StageKeys.ChooseFile;
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
        }

        private void CancelAndReupload()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void UpdateCurrentStageForCompletionState(ProcessingCompletionStates processingCompletionState)
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
            }
        }

        private void ProcessStart(string url)
        {
            _windowsProcessService.ProcessStart(url);
        }

        private void SettingsNavigate()
        {
            _windowService.ShowSettingsWindow();
        }

        private void AboutNavigate()
        {
            _windowService.ShowAboutWindow();
        }
    }
}