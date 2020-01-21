using RestSharp;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IAPIClientFactory<T>
    {
        IRestClient GetClient();

        IRestRequest GetRequest();

        IRestRequest GetRequestWithParameter(string param);
    }
}