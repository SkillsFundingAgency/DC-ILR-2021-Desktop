using ESFA.DC.ILR.Desktop.Service.Pipeline.Mutator;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Interface
{
    public interface IIlrDesktopTaskDefinition
    {
        IlrDesktopTaskKeys Key { get; }

        IlrDesktopTaskKeys? FailureKey { get; }

        ContextMutatorKeys? FailureContextMutatorKey { get; }
    }
}
