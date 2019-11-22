using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using Version = ESFA.DC.ILR.Desktop.Models.Version;

namespace ESFA.DC.ILR.Desktop.Service.APIClient
{
    public class ApplicationVersionClientStub : IApplicationVersionClient
    {
        public async Task<ApplicationVersion> GetApplicationVersionsAsync()
        {
            var applicationVersions = new ApplicationVersion
            {
                LastUpdated = new DateTime(2019, 11, 10),
                Url = "https://submitlearnerdatabeta.fasst.org.uk/public-downloads/Desktop",
                Versions = new List<Version>
                {
                    new Version
                    {
                        ApplicationVersion = "0.0.0.1",
                        Major = 2,
                        Minor = 0,
                        Increment = 1,
                        ReleaseDateTime = new DateTime(2019, 11, 10)
                    }
                }
            };

            return applicationVersions;
        }
    }
}