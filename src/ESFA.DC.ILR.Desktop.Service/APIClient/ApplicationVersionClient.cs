using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.APIClient
{
    public class ApplicationVersionClient : IApplicationVersionClient
    {
        private readonly IAPIClientFactory _clientFactory;

        public ApplicationVersionClient(IAPIClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ApplicationVersion> GetApplicationVersionsAsync()
        {
            try
            {
                var client = _clientFactory.GetAPIClient();
                var request = _clientFactory.GetApplicationVersionRequest();

                return await client.GetAsync<ApplicationVersion>(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}