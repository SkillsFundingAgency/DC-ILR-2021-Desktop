using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Command;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IDialogInteractionService _dialogInteractionService;

        public SettingsViewModel(IDesktopServiceSettings desktopServiceSettings, IDialogInteractionService dialogInteractionService)
        {
            _dialogInteractionService = dialogInteractionService;
            _desktopServiceSettings = desktopServiceSettings;

            ChooseOutputDirectoryCommand = new RelayCommand(ChooseOutputDirectory);
            SaveSettingsCommand = new AsyncCommand(SaveSettings);
        }

        public RelayCommand ChooseOutputDirectoryCommand { get; set; }

        public AsyncCommand SaveSettingsCommand { get; set; }

        public string IlrDatabaseConnectionString
        {
            get => _desktopServiceSettings.IlrDatabaseConnectionString;
            set
            {
                _desktopServiceSettings.IlrDatabaseConnectionString = value;
                RaisePropertyChanged();
            }
        }

        public string OutputDirectory
        {
            get => _desktopServiceSettings.OutputDirectory;
            set
            {
                _desktopServiceSettings.OutputDirectory = value;
                RaisePropertyChanged();
            }
        }

        private void ChooseOutputDirectory()
        {
            var directory = _dialogInteractionService.GetFolderNameFromFolderBrowserDialog(_desktopServiceSettings.OutputDirectory, " ");
            if (!string.IsNullOrWhiteSpace(directory))
            {
                OutputDirectory = directory;
            }
        }

        private async Task SaveSettings()
        {
            await _desktopServiceSettings.SaveAsync(CancellationToken.None);
        }
    }
}
