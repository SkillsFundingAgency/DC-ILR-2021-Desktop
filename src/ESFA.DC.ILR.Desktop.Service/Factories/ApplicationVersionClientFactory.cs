using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class ApplicationVersionClientFactory : APIClientFactory, IAPIClientFactory<ApplicationVersion>
    {
        private readonly IAPIConfiguration _configuration;

        public ApplicationVersionClientFactory(IAPIConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IRestClient GetClient()
        {
            return GetAPIClient(
                _configuration.Configuration.APIBaseUrl,
                _configuration.Configuration.APIVersionHeaderKey,
                _configuration.Configuration.APIVersionNumber);
        }

        public IRestRequest GetRequest()
        {
            var request = new RestRequest(_configuration.Configuration.ApplicationVersionPath, Method.GET);
            return request;
        }

        public IRestRequest GetRequestWithParameter(string param)
        {
            throw new System.NotImplementedException();
        }
    }
}