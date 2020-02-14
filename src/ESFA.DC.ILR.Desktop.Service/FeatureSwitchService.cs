using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class FeatureSwitchService : IFeatureSwitchService
    {
        private readonly IFeatureSwitchConfiguration _featureSwitchConfiguration;

        public FeatureSwitchService(IFeatureSwitchConfiguration featureSwitchConfiguration)
        {
            _featureSwitchConfiguration = featureSwitchConfiguration;
        }

        public bool ReportFilters => _featureSwitchConfiguration.Configuration.ReportFilters;

        public bool VersionUpdate => _featureSwitchConfiguration.Configuration.VersionUpdate;
    }
}
