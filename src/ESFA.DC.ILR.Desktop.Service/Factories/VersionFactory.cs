using System;
using ESFA.DC.ILR.Desktop.Interface.Services;
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
                Major = versionParts.Length > 0 ? Convert.ToInt32(versionParts[0]) : default(int),
                Minor = versionParts.Length > 1 ? Convert.ToInt32(versionParts[1]) : default(int),
                Increment = versionParts.Length > 2 ? Convert.ToInt32(versionParts[2]) : default(int)
            };
        }
    }
}