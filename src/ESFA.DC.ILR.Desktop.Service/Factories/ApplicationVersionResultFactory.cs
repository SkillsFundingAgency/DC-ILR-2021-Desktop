using System;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class ApplicationVersionResultFactory : IApplicationVersionResultFactory
    {
        public ApplicationVersionResult GetApplicationVersionResult(
            string applicationVersion = null,
            DateTime? releaseDateTime = null,
            string url = null)
        {
            return new ApplicationVersionResult
            {
                ApplicationVersion = applicationVersion,
                ReleaseDateTime = releaseDateTime,
                Url = url
            };
        }
    }
}