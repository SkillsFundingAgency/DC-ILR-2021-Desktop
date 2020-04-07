using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class DesktopServiceSettings : IDesktopServiceSettings
    {
        private const string IlrDatabaseConnectionStringKey = "IlrDatabaseConnectionString";
        private const string OutputDirectoryKey = "OutputDirectory";
        private const string ExportToSqlKey = "ExportToSQL";
        private const string ExportToAccessAndCsvKey = "ExportToAccessAndCsv";
        private const string ReferenceDataVersionKey = "ReferenceDataVersion";
        private const string FundingInformationSystem = "Funding Information System 2020-21";

        public string IlrDatabaseConnectionString { get; set; }

        public string OutputDirectory { get; set; }

        public bool ExportToSql { get; set; }

        public bool ExportToAccessAndCsv { get; set; }

        public string ReferenceDataVersion { get; set; }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            IlrDatabaseConnectionString = ConfigurationManager.AppSettings[IlrDatabaseConnectionStringKey];
            OutputDirectory = ConfigurationManager.AppSettings[OutputDirectoryKey];
            ExportToSql = Convert.ToBoolean(ConfigurationManager.AppSettings[ExportToSqlKey]);
            ExportToAccessAndCsv = Convert.ToBoolean(ConfigurationManager.AppSettings[ExportToAccessAndCsvKey]);
            ReferenceDataVersion = ConfigurationManager.AppSettings[ReferenceDataVersionKey];

            if (string.IsNullOrWhiteSpace(OutputDirectory))
            {
                OutputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FundingInformationSystem);
                await SaveAsync(cancellationToken);
            }
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Clear();

            config.AppSettings.Settings.Add(IlrDatabaseConnectionStringKey, IlrDatabaseConnectionString);
            config.AppSettings.Settings.Add(OutputDirectoryKey, OutputDirectory);
            config.AppSettings.Settings.Add(ExportToSqlKey, ExportToSql.ToString());
            config.AppSettings.Settings.Add(ExportToAccessAndCsvKey, ExportToAccessAndCsv.ToString());
            config.AppSettings.Settings.Add(ReferenceDataVersionKey, ReferenceDataVersion);

            config.Save(ConfigurationSaveMode.Modified);

            return Task.CompletedTask;
        }
    }
}
