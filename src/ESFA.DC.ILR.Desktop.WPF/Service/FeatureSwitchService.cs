using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Config;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class FeatureSwitchService : IFeatureSwitchService
    {
        public bool ReportFilters => FeatureSwitchConfiguration.Configuration.ReportFilters;

        public bool VersionUpdate => FeatureSwitchConfiguration.Configuration.VersionUpdate;
    }
}
