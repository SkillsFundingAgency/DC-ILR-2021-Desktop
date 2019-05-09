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
        private readonly ISettingsService _settingsService;
        private readonly IDialogInteractionService _dialogInteractionService;

        private string _ilrDatabaseConnectionString;
        private string _outputDirectory;

        public SettingsViewModel(ISettingsService settingsService, IDialogInteractionService dialogInteractionService)
        {
            _desktopServiceSettings = settingsService.LoadAsync(CancellationToken.None).Result;
            _settingsService = settingsService;
            _dialogInteractionService = dialogInteractionService;

            _ilrDatabaseConnectionString = _desktopServiceSettings.IlrDatabaseConnectionString;
            _outputDirectory = _desktopServiceSettings.OutputDirectory;

            ChooseOutputDirectoryCommand = new RelayCommand(ChooseOutputDirectory);
            SaveSettingsCommand = new AsyncCommand(SaveSettings);
        }

        public RelayCommand ChooseOutputDirectoryCommand { get; set; }

        public AsyncCommand SaveSettingsCommand { get; set; }

        public string IlrDatabaseConnectionString
        {
            get => _ilrDatabaseConnectionString;
            set
            {
                _ilrDatabaseConnectionString = value;
                RaisePropertyChanged();
            }
        }

        public string OutputDirectory
        {
            get => _outputDirectory;
            set
            {
                _outputDirectory = value;
                RaisePropertyChanged();
            }
        }

        private void ChooseOutputDirectory()
        {
            var choosenDirectory = _dialogInteractionService.GetFolderNameFromFolderBrowserDialog("", "");
            if(!string.IsNullOrWhiteSpace(choosenDirectory))
            {
                OutputDirectory = choosenDirectory;
            }
        }

        private async Task SaveSettings()
        {
            _desktopServiceSettings.IlrDatabaseConnectionString = IlrDatabaseConnectionString;
            _desktopServiceSettings.OutputDirectory = OutputDirectory;

            await _settingsService.SaveAsync(_desktopServiceSettings, CancellationToken.None);
        }
    }
}
