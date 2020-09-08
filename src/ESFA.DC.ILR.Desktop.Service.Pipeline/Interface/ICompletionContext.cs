using ESFA.DC.ILR.Desktop.Service.Pipeline.Journey;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Interface
{
    public interface ICompletionContext
    {
        string OutputDirectory { get; }

        ProcessingCompletionStates? ProcessingCompletionState { get; }
    }
}
