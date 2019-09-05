﻿using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.ReferenceData;
using IDesktopContextFactory = ESFA.DC.ILR.Desktop.WPF.Service.Interface.IDesktopContextFactory;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDesktopServiceSettings _desktopServiceSettings;

        public DesktopContextFactory(IDateTimeProvider dateTimeProvider, IDesktopServiceSettings desktopServiceSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _desktopServiceSettings = desktopServiceSettings;
        }

        public IDesktopContext Build(string filePath)
        {
            return new DesktopContext(_dateTimeProvider.GetNowUtc(), _desktopServiceSettings.OutputDirectory, filePath, ReferenceDataConstants.FilePath, _desktopServiceSettings.IlrDatabaseConnectionString);
        }
    }
}