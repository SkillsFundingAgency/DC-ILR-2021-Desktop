using RestSharp;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IAPIClientFactory
    {
        IRestClient GetAPIClient();

        IRestRequest GetApplicationVersionRequest();
    }
}