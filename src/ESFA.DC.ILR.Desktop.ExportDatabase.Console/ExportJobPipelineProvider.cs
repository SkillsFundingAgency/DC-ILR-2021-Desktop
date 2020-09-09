using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.Desktop.Service.Pipeline;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console
{
    public class ExportJobPipelineProvider : IIlrPipelineProvider
    {
        public IReadOnlyList<IIlrDesktopTaskDefinition> Provide()
        {
            return BuildTaskDefinitionCollectionForSettings();
        }

        public int IndexFor(IlrDesktopTaskKeys ilrDesktopTaskKey, IReadOnlyList<IIlrDesktopTaskDefinition> pipeline) => pipeline.ToList().FindIndex(i => i.Key == ilrDesktopTaskKey);

        private IReadOnlyList<IIlrDesktopTaskDefinition> BuildTaskDefinitionCollectionForSettings()
        {
            var taskList = new List<IIlrDesktopTaskDefinition>
            {
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbCreate),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbExport),
                new IlrDesktopTaskDefinition(IlrDesktopTaskKeys.MdbPublish),
            };

            return taskList;
        }
    }
}
