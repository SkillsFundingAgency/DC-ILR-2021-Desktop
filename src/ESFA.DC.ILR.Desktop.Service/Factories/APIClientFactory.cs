using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public abstract class APIClientFactory
    {
        public IRestClient GetAPIClient(string baseUrl, string headerKey, string apiVersion)
        {
            IRestClient client = new RestClient(baseUrl);

            client.AddDefaultHeader(headerKey, apiVersion);

            return client;
        }
    }
}