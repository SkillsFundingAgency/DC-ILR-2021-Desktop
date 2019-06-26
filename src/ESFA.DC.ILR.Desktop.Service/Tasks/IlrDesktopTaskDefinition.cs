using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Mutator;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class IlrDesktopTaskDefinition : IIlrDesktopTaskDefinition
    {
        public IlrDesktopTaskDefinition(IlrDesktopTaskKeys key, IlrDesktopTaskKeys? failureKey = null, ContextMutatorKeys? failureContextMutatorKey = null)
        {
            Key = key;
            FailureKey = failureKey;
            FailureContextMutatorKey = failureContextMutatorKey;
        }

        public IlrDesktopTaskKeys Key { get; }

        public IlrDesktopTaskKeys? FailureKey { get; }

        public ContextMutatorKeys? FailureContextMutatorKey { get; }
    }
}
