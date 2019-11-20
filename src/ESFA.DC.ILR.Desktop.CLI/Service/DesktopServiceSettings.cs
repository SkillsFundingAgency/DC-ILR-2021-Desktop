using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.CLI.Service
{
    public class DesktopServiceSettings : IDesktopServiceSettings
    {
        private const string IlrDatabaseConnectionStringKey = "IlrDatabaseConnectionString";
        private const string OutputDirectoryKey = "OutputDirectory";
        private const string ExportToSqlKey = "ExportToSQL";
        private const string ExportToAccessAndCsvKey = "ExportToAccessAndCsv";

        public string IlrDatabaseConnectionString { get; set; }

        public string OutputDirectory { get; set; }

        public bool ExportToSql { get; set; }

        public bool ExportToAccessAndCsv { get; set; }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            IlrDatabaseConnectionString = ConfigurationManager.AppSettings[IlrDatabaseConnectionStringKey];
            OutputDirectory = ConfigurationManager.AppSettings[OutputDirectoryKey];
            ExportToSql = Convert.ToBoolean(ConfigurationManager.AppSettings[ExportToSqlKey]);
            ExportToAccessAndCsv = Convert.ToBoolean(ConfigurationManager.AppSettings[ExportToAccessAndCsvKey]);
        }
    }
}
