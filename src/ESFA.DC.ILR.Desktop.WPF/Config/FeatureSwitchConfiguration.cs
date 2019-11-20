using System;
using System.Configuration;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class FeatureSwitchConfiguration : ConfigurationSection
    {
        private const string ReportFiltersKey = "ReportFilters";

        private const string VersionUpdateKey = "VersionUpdate";

        public static FeatureSwitchConfiguration Configuration => ConfigurationManager.GetSection("FeatureSwitchConfiguration") as FeatureSwitchConfiguration;

        [ConfigurationProperty(ReportFiltersKey, IsRequired = true)]
        public bool ReportFilters => Convert.ToBoolean(this[ReportFiltersKey]);

        [ConfigurationProperty(VersionUpdateKey, IsRequired = true)]
        public bool VersionUpdate => Convert.ToBoolean(this[VersionUpdateKey]);
    }
}
