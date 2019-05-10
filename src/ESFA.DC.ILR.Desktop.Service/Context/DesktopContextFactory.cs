using System.Collections.Generic;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContextFactory : IDesktopContextFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public DesktopContextFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public IDesktopContext Build(string filePath)
        {
            return new DesktopContext()
            {
                DateTimeUtc = _dateTimeProvider.GetNowUtc(),
                KeyValuePairs = new Dictionary<string, object>()
                {
                    [ILRContextKeys.Container] = "Sandbox",
                    [ILRContextKeys.Filename] = filePath,
                    [ILRContextKeys.OriginalFilename] = filePath,
                    [ILRContextKeys.ValidationErrors] = string.Empty,
                }
            };
        }
    }
}
