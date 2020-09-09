using ESFA.DC.ILR.Constants;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Pipeline;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Mutator
{
    public class SchemaErrorContextMutator : IContextMutator
    {
        public IDesktopContext Mutate(IDesktopContext desktopContext)
        {
            desktopContext.KeyValuePairs[ILRContextKeys.ReportTasks] = ReportTaskNameConstants.ValidationSchemaErrorReport;
            return desktopContext;
        }
    }
}
