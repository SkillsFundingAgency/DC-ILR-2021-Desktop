using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Command;
using ESFA.DC.ILR.Desktop.WPF.Common;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

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

        private readonly IIlrDesktopService _ilrDesktopService;
        private readonly IMessengerService _messengerService;
        private readonly IWindowService _windowService;
        private readonly IDialogInteractionService _dialogInteractionService;

        public MainViewModel(
            IIlrDesktopService ilrDesktopService,
            IMessengerService messengerService,
            IWindowService windowService,
            IDialogInteractionService dialogInteractionService)
        {
            CurrentTask = 0;
            TaskCount = 1;

            _ilrDesktopService = ilrDesktopService;
            _messengerService = messengerService;
            _windowService = windowService;
            _dialogInteractionService = dialogInteractionService;

            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog, () => !Processing);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => !Processing);
            SettingsNavigationCommand = new RelayCommand(SettingsNavigate, () => !Processing);
            AboutNavigationCommand = new RelayCommand(AboutNavigate);
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

            await _ilrDesktopService.ProcessAsync(FileName, CancellationToken.None);

            Processing = false;
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