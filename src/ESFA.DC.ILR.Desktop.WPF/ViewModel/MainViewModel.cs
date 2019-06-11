using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Command;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
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
        private bool _processing = false;
        private string _fileName;
        private string _taskName;
        private int _currentTask;
        private int _taskCount;
        private string _versionNumber = "2.456.01093";
        private string _refDataDateCreated = "13/02/2019";
        private StageKeys _currentStage = StageKeys.ChooseFile;
        private string _surveyHyperlinkUrl = "http://bbc.co.uk";
        private string _guidanceHyperlinkUrl = "http://google.co.uk";
        private string _reportsLocation = "C:/Users";

        private readonly IIlrDesktopService _ilrDesktopService;
        private readonly IMessengerService _messengerService;
        private readonly IWindowService _windowService;
        private readonly IDialogInteractionService _dialogInteractionService;
        private readonly IWindowsProcessService _windowsProcessService;

        public MainViewModel(
            IIlrDesktopService ilrDesktopService,
            IMessengerService messengerService,
            IWindowService windowService,
            IDialogInteractionService dialogInteractionService,
            IWindowsProcessService windowsProcessService)
        {
            CurrentTask = 0;
            TaskCount = 1;

            _ilrDesktopService = ilrDesktopService;
            _messengerService = messengerService;
            _windowService = windowService;
            _dialogInteractionService = dialogInteractionService;
            _windowsProcessService = windowsProcessService;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog, () => !Processing);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => !Processing);
            SettingsNavigationCommand = new RelayCommand(SettingsNavigate, () => !Processing);
            AboutNavigationCommand = new RelayCommand(AboutNavigate);
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
            SurveyHyperlinkCommand = new RelayCommand(() => ProcessStart(_surveyHyperlinkUrl));
            GuidanceHyperlinkCommand = new RelayCommand(() => ProcessStart(_guidanceHyperlinkUrl));
            ReportsFolderCommand = new RelayCommand(() => ProcessStart(_reportsLocation));
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
            }
        }

        public bool ChooseFileVisibility => CurrentStage == StageKeys.ChooseFile;

        public bool ProcessingVisibility => CurrentStage == StageKeys.Processing;

        public bool ProcessedSuccessfullyVisibility => CurrentStage == StageKeys.ProcessedSuccessfully;

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
        }

        public string VersionNumber
        {
            get { return _versionNumber; }

            set
            {
                Set(ref _versionNumber, value);
            }
        }

        public string RefDataDateCreated
        {
            get { return _refDataDateCreated; }

            set
            {
                Set(ref _refDataDateCreated, value);
            }
        }

        public bool Processing
        {
            get => _processing;
            set
            {
                _processing = value;

                ChooseFileCommand.RaiseCanExecuteChanged();
                ProcessFileCommand.RaiseCanExecuteChanged();
                SettingsNavigationCommand.RaiseCanExecuteChanged();
            }
        }

        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                RaisePropertyChanged();
            }
        }

        public int CurrentTask
        {
            get => _currentTask;
            set
            {
                _currentTask = value;
                RaisePropertyChanged();
            }
        }

        public int TaskCount
        {
            get => _taskCount;
            set
            {
                _taskCount = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand ChooseFileCommand { get; set; }

        public AsyncCommand ProcessFileCommand { get; set; }

        public RelayCommand SettingsNavigationCommand { get; set; }

        public RelayCommand AboutNavigationCommand { get; set; }

        public RelayCommand<ICloseable> CloseWindowCommand { get; set; }

        public RelayCommand SurveyHyperlinkCommand { get; set; }

        public RelayCommand GuidanceHyperlinkCommand { get; set; }

        public RelayCommand ReportsFolderCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        private void ShowChooseFileDialog()
        {
            var fileName = _dialogInteractionService.GetFileNameFromOpenFileDialog();

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                FileName = fileName;
            }
        }

        private async Task ProcessFile()
        {
            Processing = true;

            CurrentStage = StageKeys.Processing;

            await _ilrDesktopService.ProcessAsync(FileName, CancellationToken.None);

            CurrentStage = StageKeys.ProcessedSuccessfully;

            Processing = false;
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

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}