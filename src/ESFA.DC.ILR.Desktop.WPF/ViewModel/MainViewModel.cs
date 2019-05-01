using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Message;
using ESFA.DC.ILR.Desktop.WPF.Command;
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

        private readonly IIlrDesktopService _ilrDesktopService;
        private readonly IMessengerService _messengerService;
        private readonly IWindowService _windowService;

        public MainViewModel(IIlrDesktopService ilrDesktopService, IMessengerService messengerService, IWindowService windowService)
        {
            if (IsInDesignMode)
            {
                FileName = "C:/Users/TestFiles/ILRFile.xml";
                TaskName = "File Validation";
            }
            else
            {
                FileName = "No file chosen";
                CurrentTask = 0;
                TaskCount = 1;

                _ilrDesktopService = ilrDesktopService;
                _messengerService = messengerService;
                _windowService = windowService;

                _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
            }

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog, () => !Processing);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => !Processing);
            SettingsNavigationCommand = new RelayCommand(SettingsNavigate, () => !Processing);
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

        private void ShowChooseFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
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

        private void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }
    }
}