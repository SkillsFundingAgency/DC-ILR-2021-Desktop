using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class VersionService : IVersionService
    {
        private readonly IApplicationVersionClient _versionClient;
        private readonly IApplicationVersionResultFactory _applicationVersionResultFactory;

        public VersionService(
            IApplicationVersionClient versionClient,
            IApplicationVersionResultFactory applicationVersionResultFactory)
        {
            _versionClient = versionClient;
            _applicationVersionResultFactory = applicationVersionResultFactory;
        }

        public async Task<ApplicationVersionResult> GetLatestApplicationVersion(Version currentVersion)
        {
            var applicationVersions = await _versionClient.GetApplicationVersionsAsync();

            var newerVersion = GetNewerApplicationVersion(currentVersion, applicationVersions.Versions);

            return newerVersion == null
                ? null
                : _applicationVersionResultFactory.GetApplicationVersionResult(
                    newerVersion.VersionName,
                    newerVersion.ReleaseDateTime,
                    applicationVersions.Url);
        }

        private Version GetNewerApplicationVersion(Version currentVersion, IEnumerable<Version> availableVersions)
        {
            return availableVersions
                .OrderByDescending(v => v.Major).ThenByDescending(v => v.Minor).ThenByDescending(v => v.Increment)
                .FirstOrDefault(v => v.Major > currentVersion.Major
                                              || (v.Major == currentVersion.Major && v.Minor > currentVersion.Minor)
                                              || (v.Major == currentVersion.Major && v.Minor == currentVersion.Minor && v.Increment > currentVersion.Increment)) ?? currentVersion;
        }
    }
}