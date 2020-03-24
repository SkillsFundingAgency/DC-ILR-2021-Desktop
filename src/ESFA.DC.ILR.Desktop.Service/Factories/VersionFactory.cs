using System;
using System.Globalization;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class VersionFactory : IVersionFactory
    {
        private readonly string _referenceDataDateFormat = "yyyyMMddhhmm";

        public Version GetVersion(string versionNumber, string referenceDataVersion)
        {
            var versionParts = versionNumber.Split('.');
            var refDataVersionParts = referenceDataVersion.Split('.');

            return new Version
            {
                VersionName = versionNumber,
                Major = versionParts.Length > 0 ? int.TryParse(versionParts[0], out var major) ? major : default(int) : default(int),
                Minor = versionParts.Length > 1 ? int.TryParse(versionParts[1], out var minor) ? minor : default(int) : default(int),
                Increment = versionParts.Length > 2 ? int.TryParse(versionParts[2], out var increment) ? increment : default(int) : default(int),
                ReferenceDataVersion = new Models.ReferenceData
                {
                    Major = refDataVersionParts.Length > 0 ? int.TryParse(refDataVersionParts[0], out var majorRefData) ? majorRefData : default(int) : default(int),
                    Minor = refDataVersionParts.Length > 1 ? int.TryParse(refDataVersionParts[1], out var minorRefData) ? minorRefData : default(int) : default(int),
                    Increment = refDataVersionParts.Length > 2 ? int.TryParse(refDataVersionParts[2], out var incrementRefData) ? incrementRefData : default(int) : default(int),
                    ReleaseDateTime = refDataVersionParts.Length > 3 ? DateTime.TryParseExact(refDataVersionParts[3], _referenceDataDateFormat, null, DateTimeStyles.None, out var releaseDate) ? releaseDate : default(DateTime?) : default(DateTime?),
                    VersionName = referenceDataVersion
                }
            };
        }
    }
}