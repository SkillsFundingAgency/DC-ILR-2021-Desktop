using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Command;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IDesktopServiceSettings _desktopServiceSettings;
        private readonly ISettingsService _settingsService;
        private string _ilrDatabaseConnectionString;
        private string _outputDirectory;
        private const string ChooseOutputDirectoryDescription = @"Choose Output Directory";

        public SettingsViewModel(ISettingsService settingsService)
        {
            _desktopServiceSettings = settingsService.LoadAsync(CancellationToken.None).Result;
            _settingsService = settingsService;

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
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                try
                {
                    // Configure browser dialog box
                    folderBrowserDialog.Description = ChooseOutputDirectoryDescription;
                    folderBrowserDialog.ShowNewFolderButton = true;
                    folderBrowserDialog.SelectedPath = OutputDirectory;

                    // Show the dialog.
                    var result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        // Retrieve the selected path
                        OutputDirectory = folderBrowserDialog.SelectedPath;
                    }
                }
                catch (Exception ex)
                {

                }
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
