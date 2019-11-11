using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Mutator;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service
{
    public class IlrPipelineProvider : IIlrPipelineProvider
    {
        private readonly IDesktopServiceSettings _desktopServiceSettings;

        public IlrPipelineProvider(IDesktopServiceSettings desktopServiceSettings)
        {
            _desktopServiceSettings = desktopServiceSettings;
        }

        public IReadOnlyList<IIlrDesktopTaskDefinition> Provide()
        {
            return BuildTaskDefinitionCollectionForSettings(_desktopServiceSettings.ExportToSql, _desktopServiceSettings.ExportToAccessAndCsv);
        }

        public int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey, IReadOnlyList<IIlrDesktopTaskDefinition> pipeline) => pipeline.ToList().FindIndex(i => i.Key == ilrDesktopTaskKey);

        private IReadOnlyList<IIlrDesktopTaskDefinition> BuildTaskDefinitionCollectionForSettings(bool exportToSql, bool exportToAccessAndCsv)
        {
            var taskList = new List<IIlrDesktopTaskDefinition>();

            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PreExecution));

            if (exportToSql)
            {
                taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DatabaseCreate));
            }

            if (exportToAccessAndCsv)
            {
                taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbCreate));
            }

            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FileValidationService, IlrDesktopTaskKeys.ReportService, ContextMutatorKeys.SchemaError));
            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReferenceDataService));
            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ValidationService));
            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.FundingService));

            if (exportToSql)
            {
                taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.DataStore));
            }

            if (exportToAccessAndCsv)
            {
                taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbExport));
            }

            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.ReportService));
            taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.PostExecution));

            if (exportToAccessAndCsv)
            {
                taskList.Add(new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbPublish));
            }

            return taskList;
        }
    }
}
