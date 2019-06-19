using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class IlrPipelineProvider : IIlrPipelineProvider
    {
        private readonly List<IIlrDesktopTaskDefinition> _pipeline = new List<IIlrDesktopTaskDefinition>()
        {
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PreExecution),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DatabaseCreate),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FileValidationService, IlrDesktopTaskKeys.ReportService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReferenceDataService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ValidationService),
            //  new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FundingService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DataStore),
            //  new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReportService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PostExecution),
        };

        public IReadOnlyList<IIlrDesktopTaskDefinition> Provide() => _pipeline;

        public int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey) => _pipeline.FindIndex(i => i.Key == ilrDesktopTaskKey);
    }
}
