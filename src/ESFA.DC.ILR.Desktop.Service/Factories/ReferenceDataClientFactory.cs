﻿using System.IO;
using ESFA.DC.ILR.Desktop.Internal.Interface.Configuration;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.Factories
{
    public class ReferenceDataClientFactory : APIClientFactory, IAPIClientFactory<Stream>
    {
        private readonly IAPIConfiguration _configuration;

        public ReferenceDataClientFactory(IAPIConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IRestClient GetClient()
        {
            return GetAPIClient(
                _configuration.Configuration.APIBaseUrl,
                _configuration.Configuration.APIVersionHeaderKey,
                _configuration.Configuration.APIVersionNumber,
                _configuration.Configuration.AcademicYear);
        }

        public IRestRequest GetRequestWithParameter(string param)
        {
            return new RestRequest(Path.Combine(_configuration.Configuration.ReferenceDataVersionPath, param), Method.GET);
        }

        public IRestRequest GetRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}