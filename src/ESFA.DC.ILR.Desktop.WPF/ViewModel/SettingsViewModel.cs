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
        private const string OutputDirectoryDescription = "Choose Output Directory";
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IDialogInteractionService _dialogInteractionService;

        private string _ilrDatabaseConnectionString;
        private string _outputDirectory;
        private bool _exportToSql;

        public SettingsViewModel(IDesktopServiceSettings desktopServiceSettings, IDialogInteractionService dialogInteractionService)
        {
            _dialogInteractionService = dialogInteractionService;
            _desktopServiceSettings = desktopServiceSettings;

            ChooseOutputDirectoryCommand = new RelayCommand(ChooseOutputDirectory);
            SaveSettingsCommand = new AsyncCommand<ICloseable>(SaveSettings, CanSave);
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);

            _ilrDatabaseConnectionString = _desktopServiceSettings.IlrDatabaseConnectionString;
            _outputDirectory = _desktopServiceSettings.OutputDirectory;
            _exportToSql = _desktopServiceSettings.ExportToSql;
        }

        public RelayCommand ChooseOutputDirectoryCommand { get; set; }

        public AsyncCommand<ICloseable> SaveSettingsCommand { get; set; }

        public RelayCommand<ICloseable> CloseWindowCommand { get; set; }

        public string IlrDatabaseConnectionString
        {
            get => _ilrDatabaseConnectionString;
            set
            {
                Set(ref _ilrDatabaseConnectionString, value);
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public string OutputDirectory
        {
            get => _outputDirectory;
            set
            {
                Set(ref _outputDirectory, value);
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ExportToSql
        {
            get => _exportToSql;
            set
            {
                Set(ref _exportToSql, value);
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanSave()
        {
            if (ExportToSql)
            {
                return !string.IsNullOrWhiteSpace(IlrDatabaseConnectionString) && !string.IsNullOrWhiteSpace(OutputDirectory);
            }

            return !string.IsNullOrWhiteSpace(OutputDirectory);
        }

        private void ChooseOutputDirectory()
        {
            var newDirectory = _dialogInteractionService.GetFolderNameFromFolderBrowserDialog(OutputDirectory, OutputDirectoryDescription);

            if (!string.IsNullOrWhiteSpace(newDirectory))
            {
                OutputDirectory = newDirectory;
            }
        }

        private async Task SaveSettings(ICloseable window)
        {
            _desktopServiceSettings.IlrDatabaseConnectionString = IlrDatabaseConnectionString;
            _desktopServiceSettings.OutputDirectory = OutputDirectory;
            _desktopServiceSettings.ExportToSql = ExportToSql;

            await _desktopServiceSettings.SaveAsync(CancellationToken.None);

            window?.Close();
        }

        private void CloseWindow(ICloseable window)
        {
            IlrDatabaseConnectionString = _desktopServiceSettings.IlrDatabaseConnectionString;
            OutputDirectory = _desktopServiceSettings.OutputDirectory;
            ExportToSql = _desktopServiceSettings.ExportToSql;

            window?.Close();
        }
    }
}
