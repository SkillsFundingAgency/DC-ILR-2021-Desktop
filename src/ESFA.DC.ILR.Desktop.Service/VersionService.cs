using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.Logging.Interfaces;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class VersionService : IVersionService
    {
        private readonly IApplicationVersionResultClient _versionClient;
        private readonly IAPIResultFactory<ApplicationVersionResult> _applicationVersionResultFactory;
        private readonly ILogger _logger;

        public VersionService(
            IApplicationVersionResultClient versionClient,
            IAPIResultFactory<ApplicationVersionResult> applicationVersionResultFactory,
            ILogger logger)
        {
            _versionClient = versionClient;
            _applicationVersionResultFactory = applicationVersionResultFactory;
            _logger = logger;
        }

        public async Task<ApplicationVersionResult> GetLatestApplicationVersion(Version currentVersion)
        {
            _logger.LogInfo("Retrieving Application versions from SLD API.");

            var result = new ApplicationVersionResult();
            try
            {
                var applicationVersions = await _versionClient.GetAsync();
                var newerVersion = GetNewerApplicationVersion(currentVersion, applicationVersions.Versions);

                _logger.LogInfo("Finished retrieving Application versions from SLD API.");

                result = newerVersion == null
                ? null : _applicationVersionResultFactory.GetResult(
                    newerVersion.VersionName,
                    newerVersion.ReleaseDateTime,
                    applicationVersions.Url,
                    newerVersion.ReferenceDataVersion?.VersionName,
                    newerVersion.ReferenceDataVersion?.FileName);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Application versions from SLD API.", e);
            }

            return result;
        }

        private Version GetNewerApplicationVersion(Version currentVersion, IEnumerable<Version> availableVersions)
        {
            return availableVersions
                .OrderByDescending(v => v.Major).ThenByDescending(v => v.Minor).ThenByDescending(v => v.Increment)
                .FirstOrDefault(v => v.Major > currentVersion.Major
                                              || (v.Major == currentVersion.Major && v.Minor > currentVersion.Minor)
                                              || (v.Major == currentVersion.Major && v.Minor == currentVersion.Minor && v.Increment > currentVersion.Increment)
                                              || (v.ReferenceDataVersion.Major == currentVersion.ReferenceDataVersion.Major
                                              && v.ReferenceDataVersion.Minor == currentVersion.ReferenceDataVersion.Minor
                                              && v.ReferenceDataVersion.Increment > currentVersion.ReferenceDataVersion.Increment)) ?? currentVersion;
        }
    }
}