using System;
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
        private const string outputDirectoryDescription = "Choose Output Directory";
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IDialogInteractionService _dialogInteractionService;

        public SettingsViewModel(IDesktopServiceSettings desktopServiceSettings, IDialogInteractionService dialogInteractionService)
        {
            _dialogInteractionService = dialogInteractionService;
            _desktopServiceSettings = desktopServiceSettings;

            ChooseOutputDirectoryCommand = new RelayCommand(ChooseOutputDirectory);
            SaveSettingsCommand = new AsyncCommand(SaveSettings, CanExecute);
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
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public string OutputDirectoryDescription
        {
            get => outputDirectoryDescription;
        }

        public string OutputDirectory
        {
            get => _desktopServiceSettings.OutputDirectory;
            set
            {
                _desktopServiceSettings.OutputDirectory = value;
                RaisePropertyChanged();
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(IlrDatabaseConnectionString) && !string.IsNullOrWhiteSpace(OutputDirectory);
        }

        private void ChooseOutputDirectory()
        {
            var directory = _dialogInteractionService.GetFolderNameFromFolderBrowserDialog(_desktopServiceSettings.OutputDirectory, outputDirectoryDescription);
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
