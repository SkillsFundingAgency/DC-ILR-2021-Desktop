using ESFA.DC.ILR.Desktop.Service.Journey;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface ICompletionContext
    {
        string OutputDirectory { get; }

        ProcessingCompletionStates ProcessingCompletionState { get; }
    }
}
