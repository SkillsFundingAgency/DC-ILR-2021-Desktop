using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class SettingsServiceStub : ISettingsService
    {
        private IDesktopServiceSettings _settings;

        public IDesktopServiceSettings Settings => _settings;

        public Task SaveAsync(IDesktopServiceSettings settings, CancellationToken cancellationToken)
        {
            _settings = settings;

            return Task.CompletedTask;
        }

        public Task<IDesktopServiceSettings> LoadAsync(CancellationToken cancellationToken)
        {
            if (_settings == null)
            {
                _settings = new DesktopServiceSettingsStub()
                {
                    IlrDatabaseConnectionString = "Connection String Placeholder",
                    OutputDirectory = "Output Directory Placeholder",
                };
            }

            return Task.FromResult(_settings);
        }
    }
}
