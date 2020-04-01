using System;
using System.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;

namespace ESFA.DC.ILR.Desktop.CLI.Config
{
    public class FeatureSwitchConfiguration : ConfigurationSection, IFeatureSwitchConfiguration
    {
        private const string ReportFiltersKey = "ReportFilters";
        private const string VersionUpdateKey = "VersionUpdate";

        public IFeatureSwitchConfiguration Configuration => ConfigurationManager.GetSection("FeatureSwitchConfiguration") as FeatureSwitchConfiguration;
   
        [ConfigurationProperty(ReportFiltersKey, IsRequired = true)]
        public bool ReportFilters => Convert.ToBoolean(this[ReportFiltersKey]);

        [ConfigurationProperty(VersionUpdateKey, IsRequired = true)]
        public bool VersionUpdate => Convert.ToBoolean(this[VersionUpdateKey]);
    }
}
