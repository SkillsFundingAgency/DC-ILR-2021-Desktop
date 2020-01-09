using System.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class APIConfiguration : ConfigurationSection, IAPIConfiguration
    {
        private const string BaseUrlKey = "APIBaseUrl";
        private const string ApplicationVersionPathKey = "ApplicationVersionPath";
        private const string ReferenceDataVersionPathKey = "ReferenceDataVersionPath";
        private const string ApiVersionHeaderKey = "ApiVersionHeader";
        private const string ApiVersionKey = "ApiVersion";

        public IAPIConfiguration Configuration => ConfigurationManager.GetSection("APIConfiguration") as APIConfiguration;

        [ConfigurationProperty(BaseUrlKey, IsRequired = true)]
        public string APIBaseUrl => GetFromKey(BaseUrlKey);

        [ConfigurationProperty(ApplicationVersionPathKey, IsRequired = true)]
        public string ApplicationVersionPath => GetFromKey(ApplicationVersionPathKey);

        [ConfigurationProperty(ReferenceDataVersionPathKey, IsRequired = true)]
        public string ReferenceDataVersionPath => GetFromKey(ReferenceDataVersionPathKey);

        [ConfigurationProperty(ApiVersionHeaderKey, IsRequired = true)]
        public string APIVersionHeaderKey => GetFromKey(ApiVersionHeaderKey);

        [ConfigurationProperty(ApiVersionKey, IsRequired = true)]
        public string APIVersionNumber => GetFromKey(ApiVersionKey);

        public string GetFromKey(string key)
        {
            var result = this[key];
            return result == null ? null : string.IsNullOrWhiteSpace(result.ToString()) ? null : result.ToString();
        }
    }
}