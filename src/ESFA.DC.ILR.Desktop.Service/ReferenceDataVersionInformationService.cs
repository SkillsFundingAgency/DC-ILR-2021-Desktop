using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class ReferenceDataVersionInformationService : IReferenceDataVersionInformationService
    {
        private readonly IServiceConfiguration _serviceConfiguration;
        private readonly IDesktopServiceSettings _desktopServiceSettings;

        public ReferenceDataVersionInformationService(IServiceConfiguration serviceConfiguration, IDesktopServiceSettings desktopServiceSettings)
        {
            _serviceConfiguration = serviceConfiguration;
            _desktopServiceSettings = desktopServiceSettings;
        }

        public string Date => _serviceConfiguration.Configuration.ReferenceDataDate;

        public string VersionNumber => _desktopServiceSettings.ReferenceDataVersion;
    }
}
