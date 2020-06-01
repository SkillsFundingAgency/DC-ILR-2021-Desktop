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
        private const string ConnectionStringTestSuccess = "Connection String Test Successful";

        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly IDialogInteractionService _dialogInteractionService;
        private readonly IConnectivityService _connectivityService;

        private string _ilrDatabaseConnectionString;
        private string _outputDirectory;
        private bool _exportToSql;
        private bool _exportToAccessAndCsv;
        private string _connectionStringTestFeedback;
        private bool _connectionStringTested;
        private bool _connectionStringError;
        private bool _connectionStringTestInProgress;

        public SettingsViewModel(
            IDesktopServiceSettings desktopServiceSettings,
            IDialogInteractionService dialogInteractionService,
            IConnectivityService connectivityService)
        {
            _dialogInteractionService = dialogInteractionService;
            _connectivityService = connectivityService;
            _desktopServiceSettings = desktopServiceSettings;

            ChooseOutputDirectoryCommand = new RelayCommand(ChooseOutputDirectory);
            SaveSettingsCommand = new AsyncCommand<ICloseable>(SaveSettings, CanSave);
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
            TestConnectionStringCommand = new AsyncCommand(TestConnectionString, CanTestConnectionString);

            _ilrDatabaseConnectionString = _desktopServiceSettings.IlrDatabaseConnectionString;
            _outputDirectory = _desktopServiceSettings.OutputDirectory;
            _exportToSql = _desktopServiceSettings.ExportToSql;
            _exportToAccessAndCsv = _desktopServiceSettings.ExportToAccessAndCsv;
        }

        public RelayCommand ChooseOutputDirectoryCommand { get; set; }

        public AsyncCommand<ICloseable> SaveSettingsCommand { get; set; }

        public RelayCommand<ICloseable> CloseWindowCommand { get; set; }

        public AsyncCommand TestConnectionStringCommand { get; set; }

        public string IlrDatabaseConnectionString
        {
            get => _ilrDatabaseConnectionString;
            set
            {
                Set(ref _ilrDatabaseConnectionString, value);
                SaveSettingsCommand.RaiseCanExecuteChanged();

                ConnectionStringTested = false;
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

        public bool ExportToAccessAndCsv
        {
            get => _exportToAccessAndCsv;
            set
            {
                Set(ref _exportToAccessAndCsv, value);
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public string ConnectionStringTestFeedback
        {
            get => _connectionStringTestFeedback;
            set { Set(ref _connectionStringTestFeedback, value); }
        }

        public bool ConnectionStringTested
        {
            get => _connectionStringTested;
            set { Set(ref _connectionStringTested, value); }
        }

        public bool ConnectionStringError
        {
            get => _connectionStringError;
            set { Set(ref _connectionStringError, value); }
        }

        public bool ConnectionStringTestInProgress
        {
            get => _connectionStringTestInProgress;
            set
            {
                Set(ref _connectionStringTestInProgress, value);
                TestConnectionStringCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanSave()
        {
            if (ExportToSql)
            {
                return !string.IsNullOrWhiteSpace(IlrDatabaseConnectionString) &&
                       !string.IsNullOrWhiteSpace(OutputDirectory);
            }

            return !string.IsNullOrWhiteSpace(OutputDirectory);
        }

        private bool CanTestConnectionString()
        {
            return !ConnectionStringTestInProgress;
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
            _desktopServiceSettings.ExportToAccessAndCsv = ExportToAccessAndCsv;

            await _desktopServiceSettings.SaveAsync(CancellationToken.None);

            window?.Close();
        }

        private async Task TestConnectionString()
        {
            ConnectionStringTestInProgress = true;
            ConnectionStringTested = false;

            try
            {
                var result = await _connectivityService.SqlServerTestAsync(IlrDatabaseConnectionString, CancellationToken.None);

                if (result)
                {
                    ConnectionStringError = false;
                    ConnectionStringTestFeedback = ConnectionStringTestSuccess;
                }
            }
            catch (Exception exception)
            {
                ConnectionStringError = true;
                ConnectionStringTestFeedback = exception.Message;
            }
            finally
            {
                ConnectionStringTested = true;
                ConnectionStringTestInProgress = false;
            }
        }

        private void CloseWindow(ICloseable window)
        {
            IlrDatabaseConnectionString = _desktopServiceSettings.IlrDatabaseConnectionString;
            OutputDirectory = _desktopServiceSettings.OutputDirectory;
            ExportToSql = _desktopServiceSettings.ExportToSql;
            ExportToAccessAndCsv = _desktopServiceSettings.ExportToAccessAndCsv;

            window?.Close();
        }
    }
}
