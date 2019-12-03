using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class APIClientFactory : IAPIClientFactory
    {
        private readonly IAPIConfiguration _configuration;

        public APIClientFactory(IAPIConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IRestClient GetAPIClient()
        {
            IRestClient client = new RestClient(_configuration.Configuration.APIBaseUrl);

            client.AddDefaultHeader(_configuration.Configuration.APIVersionHeaderKey, _configuration.Configuration.APIVersionNumber);

            return client;
        }

        public IRestRequest GetApplicationVersionRequest()
        {
            var request = new RestRequest(_configuration.Configuration.ApplicationVersionPath, Method.GET);
            return request;
        }
    }
}