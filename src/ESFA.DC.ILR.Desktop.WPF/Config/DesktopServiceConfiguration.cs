using System.Configuration;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class DesktopServiceConfiguration : ConfigurationSection
    {
        public static DesktopServiceConfiguration Configuration => ConfigurationManager.GetSection("DesktopServiceConfiguration") as DesktopServiceConfiguration;

        [ConfigurationProperty("ReleaseDate", IsRequired = true)]
        public string ReleaseDate => this["ReleaseDate"].ToString();
    }
}
