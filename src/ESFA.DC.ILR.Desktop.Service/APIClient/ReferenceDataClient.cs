using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Utils.Polly.Interface;

namespace ESFA.DC.ILR.Desktop.Service.APIClient
{
    public class ReferenceDataClient : IReferenceDataResultClient
    {
        private readonly IAPIClientFactory<byte[]> _clientFactory;
        private readonly IPollyPolicies _pollyPolicies;

        public ReferenceDataClient(IAPIClientFactory<byte[]> clientFactory, IPollyPolicies pollyPolicies)
        {
            _clientFactory = clientFactory;
            _pollyPolicies = pollyPolicies;
        }

        public async Task<byte[]> GetAsync(string fileName)
        {
            var client = _clientFactory.GetClient();
            var request = _clientFactory.GetRequestWithParameter(fileName);

            return client.DownloadData(request);
        }
    }
}