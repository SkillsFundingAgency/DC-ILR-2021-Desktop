using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Utils.Polly.Interface;
using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.APIClient
{
    public class ApplicationVersionClient : IApplicationVersionResultClient
    {
        private readonly IAPIClientFactory<ApplicationVersion> _clientFactory;
        private readonly IPollyPolicies _pollyPolicies;

        public ApplicationVersionClient(IAPIClientFactory<ApplicationVersion> clientFactory, IPollyPolicies pollyPolicies)
        {
            _clientFactory = clientFactory;
            _pollyPolicies = pollyPolicies;
        }

        public async Task<ApplicationVersion> GetAsync()
        {
            try
            {
                var client = _clientFactory.GetClient();
                var request = _clientFactory.GetRequest();

                return await _pollyPolicies
                    .RequestTimeoutAsyncRetryPolicy.ExecuteAsync(() =>
                 client.GetAsync<ApplicationVersion>(request));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}