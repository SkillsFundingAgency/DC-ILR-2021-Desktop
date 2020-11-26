using System.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Constants;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class APIConfiguration : ConfigurationSection, IAPIConfiguration
    {
        public IAPIConfiguration Configuration => ConfigurationManager.GetSection(APIConstants.ConfigSection) as APIConfiguration;

        [ConfigurationProperty(APIConstants.BaseUrlKey, IsRequired = true)]
        public string APIBaseUrl => GetFromKey(APIConstants.BaseUrlKey);

        [ConfigurationProperty(APIConstants.ApplicationVersionPathKey, IsRequired = true)]
        public string ApplicationVersionPath => GetFromKey(APIConstants.ApplicationVersionPathKey);

        [ConfigurationProperty(APIConstants.ReferenceDataVersionPathKey, IsRequired = true)]
        public string ReferenceDataVersionPath => GetFromKey(APIConstants.ReferenceDataVersionPathKey);

        [ConfigurationProperty(APIConstants.ApiVersionHeaderKey, IsRequired = true)]
        public string APIVersionHeaderKey => GetFromKey(APIConstants.ApiVersionHeaderKey);

        [ConfigurationProperty(APIConstants.ApiVersionKey, IsRequired = true)]
        public string APIVersionNumber => GetFromKey(APIConstants.ApiVersionKey);

        [ConfigurationProperty(APIConstants.AcademicYearKey, IsRequired = true)]
        public string AcademicYear => GetFromKey(APIConstants.AcademicYearKey);

        public string GetFromKey(string key)
        {
            var result = this[key];
            return result == null ? null : string.IsNullOrWhiteSpace(result.ToString()) ? null : result.ToString();
        }
    }
}