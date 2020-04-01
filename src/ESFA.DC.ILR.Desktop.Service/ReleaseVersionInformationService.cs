using System;
using System.Reflection;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;

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

        public string VersionNumber => FormatVersion();

        private string FormatVersion()
        {
            var assemblyVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            int index = assemblyVersion.LastIndexOf(".");

            return assemblyVersion.Substring(0, index);
        }
    }
}
