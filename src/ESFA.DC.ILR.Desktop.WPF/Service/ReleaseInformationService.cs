using System.Configuration;
using System.Reflection;
using ESFA.DC.ILR.Desktop.WPF.Config;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class ReleaseInformationService : IReleaseInformationService
    {
        public string ReleaseDate => DesktopServiceConfiguration.Configuration.ReleaseDate;

        public string ReleaseVersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
