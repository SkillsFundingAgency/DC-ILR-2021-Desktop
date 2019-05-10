using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class SettingsServiceStub : ISettingsService
    {
        private IDesktopServiceSettings _settings;
        private IConfigService _configService;

        public IDesktopServiceSettings Settings => _settings; 

        public SettingsServiceStub(IConfigService configService)
        {
            _configService = configService;
        }

        public Task SaveAsync(IDesktopServiceSettings settings, string directoryTypeKey, CancellationToken cancellationToken)
        {
            _settings = settings;

            try
            {
                var connectionStringKey = _configService.UserSettingsKeyValuePair.Where(x => x.Key.ToLower()
                                                            .Contains("connectionstring")).FirstOrDefault();
                // Change ConnectionString
                if (!string.IsNullOrWhiteSpace(connectionStringKey.Key))
                {
                    if (!connectionStringKey.Value.Equals(_settings.IlrDatabaseConnectionString))
                        _configService.SaveConfigAppSettings(connectionStringKey.Key, settings.IlrDatabaseConnectionString);
                }

                // Change OutputDirectory
                var directoryOutput = _configService.UserSettingsKeyValuePair.Where(x => x.Key == directoryTypeKey).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(directoryOutput.Key))
                {
                    if (!directoryOutput.Value.Equals(_settings.OutputDirectory))
                        _configService.SaveConfigAppSettings(directoryTypeKey, _settings.OutputDirectory);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Loads user settings from config
        /// </summary>
        /// <param name="directoryTypeKey"> Fetches the Value from config based on a provided Key</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IDesktopServiceSettings> LoadAsync(string directoryTypeKey, CancellationToken cancellationToken)
        {
            if (_settings == null)
            {
                var connectionString = _configService.UserSettingsKeyValuePair
                                                        .Where(x => x.Key.ToLower()
                                                        .Contains("connectionstring")).FirstOrDefault().Value;

                var outputDir = _configService.UserSettingsKeyValuePair
                                                        .Where(x => x.Key == directoryTypeKey).FirstOrDefault().Value;

                _settings = new DesktopServiceSettingsStub()
                {
                    IlrDatabaseConnectionString = connectionString,
                    OutputDirectory = outputDir
                };
            }

            return Task.FromResult(_settings);
        }
    }
}
