using RestSharp;

namespace ESFA.DC.ILR.Desktop.Interface.Services
{
    public interface IAPIClientFactory
    {
        IRestClient GetAPIClient();

        IRestRequest GetApplicationVersionRequest();
    }
}