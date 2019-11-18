﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;

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

            return _applicationVersionResultFactory.GetApplicationVersionResult(
                newerVersion.ApplicationVersion,
                newerVersion.ReleaseDateTime,
                applicationVersions.Url);
        }

        private Version GetNewerApplicationVersion(Version currentVersion, IEnumerable<Version> availableVersions)
        {
            return availableVersions.FirstOrDefault(v => v.Major > currentVersion.Major
                                              || (v.Major == currentVersion.Major && v.Minor > currentVersion.Minor)
                                              || (v.Major == currentVersion.Major && v.Minor == currentVersion.Minor && v.Increment > currentVersion.Increment));
        }
    }
}