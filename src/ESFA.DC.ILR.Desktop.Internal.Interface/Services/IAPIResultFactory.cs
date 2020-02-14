using System;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IAPIResultFactory<T>
    {
        T GetResult(
            string applicationVersion = null,
            DateTime? releaseDateTime = null,
            string url = null,
            string latestReferemceDataVersion = null,
            string latestReferemceDataFileName = null);
    }
}