using System.Configuration;
using System.Reflection;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class ReleaseInformationService : IReleaseInformationService
    {
        private const string ReleaseDateKey = "ReleaseDate";

        public string ReleaseDate => ConfigurationManager.AppSettings[ReleaseDateKey];

        public string ReleaseVersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
