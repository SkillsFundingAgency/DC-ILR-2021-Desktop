using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Mutator;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class IlrPipelineProvider : IIlrPipelineProvider
    {
        private readonly IDesktopServiceSettings _desktopServiceSettings;

        private readonly List<IIlrDesktopTaskDefinition> _pipeline = new List<IIlrDesktopTaskDefinition>()
        {
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PreExecution),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DatabaseCreate),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbCreate),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FileValidationService, IlrDesktopTaskKeys.ReportService, ContextMutatorKeys.SchemaError),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReferenceDataService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ValidationService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FundingService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DataStore),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbExport),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReportService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PostExecution),
        };

        private readonly List<IIlrDesktopTaskDefinition> _noSqlPipeline = new List<IIlrDesktopTaskDefinition>()
        {
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PreExecution),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbCreate),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FileValidationService, IlrDesktopTaskKeys.ReportService, ContextMutatorKeys.SchemaError),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReferenceDataService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ValidationService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FundingService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbExport),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReportService),
            new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PostExecution),
        };

        public IlrPipelineProvider(IDesktopServiceSettings desktopServiceSettings)
        {
            _desktopServiceSettings = desktopServiceSettings;
        }

        public IReadOnlyList<IIlrDesktopTaskDefinition> Provide()
        {
            return _desktopServiceSettings.ExportToSql ? _pipeline : _noSqlPipeline;
        }

        public int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey) => _pipeline.FindIndex(i => i.Key == ilrDesktopTaskKey);
    }
}
