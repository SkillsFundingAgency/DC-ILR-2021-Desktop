using System;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class ApplicationVersionResultFactory : IAPIResultFactory<ApplicationVersionResult>
    {
        public ApplicationVersionResult GetResult(
            string applicationVersion = null,
            DateTime? releaseDateTime = null,
            string url = null,
            string latestReferemceDataVersion = null,
            string latestReferemceDataFileName = null)
        {
            return new ApplicationVersionResult
            {
                ApplicationVersion = applicationVersion,
                ReleaseDateTime = releaseDateTime,
                Url = url,
                LatestReferenceDataVersion = latestReferemceDataVersion,
                LatestReferenceDataFileName = latestReferemceDataFileName
            };
        }
    }
}