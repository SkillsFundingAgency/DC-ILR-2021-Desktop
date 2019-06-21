using System.Configuration;
using System.Reflection;
using ESFA.DC.ILR.Desktop.WPF.Config;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class ReleaseVersionInformationService : IVersionInformationService
    {
        public string Date => DesktopServiceConfiguration.Configuration.ReleaseDate;

        public string VersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
