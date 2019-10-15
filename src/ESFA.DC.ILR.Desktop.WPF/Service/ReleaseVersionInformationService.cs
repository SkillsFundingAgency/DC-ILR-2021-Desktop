using System.Configuration;
using System.Reflection;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.WPF.Config;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class ReleaseVersionInformationService : IReleaseVersionInformationService
    {
        public string Date => DesktopServiceConfiguration.Configuration.ReleaseDate;

        public string VersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
