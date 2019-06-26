using System;
using System.Configuration;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class DesktopServiceConfiguration : ConfigurationSection
    {
        private const string ReleaseDateKey = "ReleaseDate";
        private const string ReferenceDataDatekey = "ReferenceDataDate";

        public static DesktopServiceConfiguration Configuration => ConfigurationManager.GetSection("DesktopServiceConfiguration") as DesktopServiceConfiguration;

        [ConfigurationProperty(ReleaseDateKey, IsRequired = true)]
        public string ReleaseDate => DateTime.Parse(this[ReleaseDateKey].ToString()).ToString();

        [ConfigurationProperty(ReferenceDataDatekey, IsRequired = true)]
        public string ReferenceDataDate => (this[ReferenceDataDatekey].ToString()).ToString();
    }
}
