using System.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class APIConfiguration : ConfigurationSection, IAPIConfiguration
    {
        private const string BaseUrlKey = "APIBaseUrl";
        private const string ApplicationVersionPathKey = "ApplicationVersionPath";
        private const string ApiVersionHeaderKey = "ApiVersionHeader";
        private const string ApiVersionKey = "ApiVersion";

        [ConfigurationProperty(BaseUrlKey, IsRequired = true)]
        public string APIBaseUrl { get; set; }

        [ConfigurationProperty(ApplicationVersionPathKey, IsRequired = true)]
        public string ApplicationVersionPath { get; set; }

        [ConfigurationProperty(ApiVersionHeaderKey, IsRequired = true)]
        public string APIVersionHeaderKey { get; set; }

        [ConfigurationProperty(ApiVersionKey, IsRequired = true)]
        public string APIVersionNumber { get; set; }
    }
}