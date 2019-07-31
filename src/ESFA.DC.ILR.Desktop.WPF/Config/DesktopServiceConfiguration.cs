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
        public string ReleaseDate => FormattedDate(ReleaseDateKey);

        [ConfigurationProperty(ReferenceDataDatekey, IsRequired = true)]
        public string ReferenceDataDate => FormattedDate(ReferenceDataDatekey);

        private string FormattedDate(string key)
        {
            if (DateTime.TryParse(this[key].ToString(), out var returnDate))
            {
                return returnDate.ToString("dd/MM/yyyy hh:mm:ss");
            }

            return string.Empty;
        }
    }
}
