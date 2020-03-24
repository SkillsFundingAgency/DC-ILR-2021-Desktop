using System.IO;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Utils.Polly.Interface;

namespace ESFA.DC.ILR.Desktop.Service.APIClient
{
    public class ReferenceDataClient : IReferenceDataResultClient
    {
        private readonly IAPIClientFactory<Stream> _clientFactory;
        private readonly IPollyPolicies _pollyPolicies;

        public ReferenceDataClient(IAPIClientFactory<Stream> clientFactory, IPollyPolicies pollyPolicies)
        {
            _clientFactory = clientFactory;
            _pollyPolicies = pollyPolicies;
        }

        public async Task<Stream> GetAsync(string fileName, Stream stream)
        {
            var client = _clientFactory.GetClient();
            var request = _clientFactory.GetRequestWithParameter(fileName);

            request.ResponseWriter = (responseStream) => responseStream.CopyTo(stream);
            var response = client.DownloadData(request);

            return stream;
        }
    }
}