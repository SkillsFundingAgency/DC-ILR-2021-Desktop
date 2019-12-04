﻿using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Internal.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Utils.Polly.Interface;
using RestSharp;

namespace ESFA.DC.ILR.Desktop.Service.APIClient
{
    public class ApplicationVersionClient : IApplicationVersionClient
    {
        private readonly IAPIClientFactory _clientFactory;
        private readonly IPollyPolicies _pollyPollicies;

        public ApplicationVersionClient(IAPIClientFactory clientFactory, IPollyPolicies pollyPollicies)
        {
            _clientFactory = clientFactory;
            _pollyPollicies = pollyPollicies;
        }

        public async Task<ApplicationVersion> GetApplicationVersionsAsync()
        {
            try
            {
                var client = _clientFactory.GetAPIClient();
                var request = _clientFactory.GetApplicationVersionRequest();

                return await _pollyPollicies
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