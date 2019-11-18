using System;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Interface.Services
{
    public interface IApplicationVersionResultFactory
    {
        ApplicationVersionResult GetApplicationVersionResult(
            string applicationVersion = null,
            DateTime? releaseDateTime = null,
            string url = null);
    }
}