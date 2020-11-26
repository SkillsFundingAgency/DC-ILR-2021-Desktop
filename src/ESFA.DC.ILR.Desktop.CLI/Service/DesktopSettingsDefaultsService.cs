using ESFA.DC.ILR.Desktop.CLI.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.CLI.Service
{
    public class DesktopSettingsDefaultsService : IDesktopSettingsDefaultsService
    {
        private IDesktopServiceSettings _desktopServiceSettings;
        public DesktopSettingsDefaultsService(IDesktopServiceSettings desktopServiceSettings)
        {
            _desktopServiceSettings = desktopServiceSettings;
        }
        public void CheckDefaults(ICommandLineArguments commandLineArguments)
        {
            ConnectionStringEnteredUpdateExportSqlTask(commandLineArguments.ConnectionString);
        }

        public void ConnectionStringEnteredUpdateExportSqlTask(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString) != true)
            {
                _desktopServiceSettings.ExportToSql = true;
            }
        }
    }
}
