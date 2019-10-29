﻿using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class ReferenceDataVersionInformationService : IReferenceDataVersionInformationService
    {
        private readonly IServiceConfiguration _serviceConfiguration;

        public ReferenceDataVersionInformationService(IServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public string Date => _serviceConfiguration.Configuration.ReferenceDataDate;

        public string VersionNumber => "123.123.123";
    }
}
