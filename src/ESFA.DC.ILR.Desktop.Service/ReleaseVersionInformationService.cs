using System.Reflection;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class ReleaseVersionInformationService : IReleaseVersionInformationService
    {
        private readonly IServiceConfiguration _serviceConfiguration;

        public ReleaseVersionInformationService(IServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public string Date => _serviceConfiguration.Configuration.ReleaseDate;

        public string VersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
