using System;
using System.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;

namespace ESFA.DC.ILR.Desktop.CLI.Config
{
    public class CliServiceConfiguration : ConfigurationSection, IServiceConfiguration
    {
        private const string ReleaseDateKey = "ReleaseDate";
        private const string ReferenceDataDatekey = "ReferenceDataDate";

        public IServiceConfiguration Configuration => ConfigurationManager.GetSection("CliServiceConfiguration") as IServiceConfiguration;

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
