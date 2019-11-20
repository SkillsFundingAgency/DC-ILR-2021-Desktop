using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class VersionFactory : IVersionFactory
    {
        public Version GetVersion(string versionNumber)
        {
            var versionParts = versionNumber.Split('.');
            return new Version
            {
                ApplicationVersion = versionNumber,
                Major = versionParts.Length > 0 ? int.TryParse(versionParts[0], out var major) ? major : default(int) : default(int),
                Minor = versionParts.Length > 1 ? int.TryParse(versionParts[1], out var minor) ? minor : default(int) : default(int),
                Increment = versionParts.Length > 2 ? int.TryParse(versionParts[2], out var increment) ? increment : default(int) : default(int)
            };
        }
    }
}