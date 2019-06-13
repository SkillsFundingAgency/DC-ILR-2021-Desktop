using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class DesktopServiceSettingsStub : IDesktopServiceSettings
    {
        private const string IlrDatabaseConnectionStringKey = "IlrDatabaseConnectionString";
        private const string OutputDirectoryKey = "OutputDirectory";
        private const string FundingInformationSystem = "Funding Information System 2019-20";

        private string _ilrDatabaseConnectionString;
        private string _outputDirectory;

        public string IlrDatabaseConnectionString
        {
            get => _ilrDatabaseConnectionString;
            set => _ilrDatabaseConnectionString = value;
        }

        public string OutputDirectory
        {
            get => _outputDirectory;
            set => _outputDirectory = value;
        }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            _ilrDatabaseConnectionString = ConfigurationManager.AppSettings[IlrDatabaseConnectionStringKey];
            _outputDirectory = ConfigurationManager.AppSettings[OutputDirectoryKey];

            if (string.IsNullOrWhiteSpace(_outputDirectory))
            {
                _outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FundingInformationSystem);
                await SaveAsync(cancellationToken);
            }
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Clear();

            config.AppSettings.Settings.Add(IlrDatabaseConnectionStringKey, _ilrDatabaseConnectionString);
            config.AppSettings.Settings.Add(OutputDirectoryKey, _outputDirectory);

            config.Save(ConfigurationSaveMode.Modified);

            return Task.CompletedTask;
        }
    }
}
