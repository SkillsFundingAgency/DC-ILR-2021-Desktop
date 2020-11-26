using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public abstract class APIClientFactory
    {
        public IRestClient GetAPIClient(string baseUrl, string headerKey, string apiVersion, string academicYear)
        {
            var url = string.Concat(baseUrl, "/", apiVersion, "/", academicYear);

            IRestClient client = new RestClient(url);

            client.AddDefaultHeader(headerKey, apiVersion);

            return client;
        }
    }
}